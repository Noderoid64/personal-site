using FullIndexSearch;
using Microsoft.EntityFrameworkCore;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Models.Entities.Enums;
using PersonalSite.Infrastructure.EF;

namespace PersonalSite.Services.FullTextSearch;

public class TextSearchIndex
{
    #region Singleton

    private static TextSearchIndex _instance;

    private TextSearchIndex()
    {
    }

    public static TextSearchIndex GetInstance()
    {
        if (_instance != null)
            return _instance;
        var temp = new TextSearchIndex();
        Interlocked.CompareExchange(ref _instance, temp, null);
        return _instance;
    }

    #endregion

    private IndexService _indexService = new IndexService();
    private AutoResetEvent _autoResetEvent = new AutoResetEvent(true);
    private IConfiguration _configuration;

    public async Task FillNewIndexAsync(IConfiguration configuration)
    {
        _autoResetEvent.WaitOne();
        _configuration = configuration;

        using (var context = new ApplicationContext(configuration))
        {
            foreach (var post in await context.Posts.Where(x => x.FileObjectType == FileObjectType.Post).ToListAsync())
            {
                _indexService.AddText(post.Content, post.Id);
                _indexService.AddText(post.Title, post.Id);
            }
        }

        await _indexService.SaveAsync();
        _indexService.SwapIndexes();
        _autoResetEvent.Set();
    }

    public async Task FillIndexAsync()
    {
        var now = DateTime.Now;
        _autoResetEvent.WaitOne();
        using (var context = new ApplicationContext(_configuration))
        {
            var postChanges = await context.PostChanges
                .Where(x => x.DateTime.CompareTo(now) >= 0)
                .ToListAsync();
            foreach (var postChange in postChanges)
            {
                var post = await context.Posts.FirstAsync(x => x.Id == postChange.Id);
                if (postChange.ChangeType == ChangeType.Add)
                {
                    _indexService.AddText(post.Content, post.Id);
                    _indexService.AddText(post.Title, post.Id);
                }
                else if (postChange.ChangeType == ChangeType.Change)
                {
                    _indexService.RemoveText(post.Id);
                    _indexService.AddText(post.Content, post.Id);
                    _indexService.AddText(post.Title, post.Id);
                }
                else if (postChange.ChangeType == ChangeType.Delete)
                {
                    _indexService.RemoveText(post.Id);
                }
            }
        }

        _indexService.SwapIndexes();
        _autoResetEvent.Set();

        await _indexService.SaveAsync();
    }

    public async Task<IEnumerable<PostPreviewDto>> FindPosts(string searchString)
    {
        var words = searchString.Split(' ');

        _autoResetEvent.WaitOne();
        var index = _indexService.GetIndex();

        Dictionary<int, HashSet<int>> result = GetWord(index, words[0]);
        for (int i = 1; i < words.Length; i++)
        {
            var intersect = result.Keys.Intersect(GetWord(index, words[i]).Keys);
            result = result.Where(x => intersect.Contains(x.Key)).ToDictionary(x => x.Key, y => y.Value);
        }

        _autoResetEvent.Set();

        List<PostPreviewDto> postPreviewDtos = new List<PostPreviewDto>();

        foreach (var row in result)
        {
            using (var context = new ApplicationContext(_configuration))
            {
                var temp = await context.Posts
                    .Where(x => x.Id == row.Key)
                    .Include(x => x.Profile)
                    .FirstAsync();

                var pos = row.Value.FirstOrDefault();
                int from, length;
                if (pos < 50)
                    from = 0;
                else
                    from = pos - 50;

                if (from + 100 > temp.Content.Length)
                    length = temp.Content.Length - from;
                else
                    length = 100;
                
                postPreviewDtos.Add(new PostPreviewDto()
                {
                    Id = temp.Id,
                    Content = temp.Content.Substring(from, length),
                    Title = temp.Title,
                    Created = temp.CreatedAt,
                    Author = temp.Profile.Nickname
                });
            }
        }

        return postPreviewDtos;
    }

    private Dictionary<int, HashSet<int>> GetWord(Dictionary<string, Dictionary<int, HashSet<int>>> index, string word)
    {
        if (index.TryGetValue(word.ToLower(), out var token))
        {
            return token;
        }

        return new Dictionary<int, HashSet<int>>();
    }
}