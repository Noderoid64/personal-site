import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-personal-site-project',
  templateUrl: './personal-site-project.component.html',
  styleUrls: ['./personal-site-project.component.scss']
})
export class PersonalSiteProjectComponent implements OnInit {

  public content = '# :bulb: The idea\n' +
    'I was wondering if maybe I should make my own site. The idea was to do all project development stages by myself: creating the concept, trying to design mockups, coding everything, and finally deploying the app on an ubuntu server with Nginx.\n' +
    '<br>\n' +
    'At that point in time I had the next aims:\n' +
    '- Trying to perform all stages by myself to feel common pitfalls and struggle a little bit.\n' +
    '- Experiment a little with architecture. Simply apply some weird experience into real peace of web app.\n' +
    '- Do and separate some common solutions into my own libraries.\n' +
    '\n' +
    '# :triangular_ruler: The structure\n' +
    '```mermaid\n' +
    'graph TD;\n' +
    'A-->C[Projects];\n' +
    'A[MainPage]-->B[About];  \n' +
    '  C-->D[Projects];\n' +
    'C-->E[Blog];\n' +
    'C-->F[Interview questions];\n' +
    '```\n' +
    'Lest make an observation of each node:\n' +
    '- **MainPage** - The first thing that you can see)\n' +
    '- **About** - Short info about me and contacts\n' +
    '- **Projects** - The page where I put all the big things like programming my personal projects. Usually, It will contain a link to a GitHub repository.\n' +
    '- **Blog** - Aimed to create posts and show them to my friends. Also, you are able to do it too. Go ahead and try!\n' +
    '- **Interview questions** - The page contains all questions that I and my colleagues (and friends) faced during interviews.\n' +
    '\n' +
    '# :notebook: Notes\n' +
    '- The current web app contains bugs and that\'s normal. I will fix them in the near future. Buy the way, if you find something, please contact me by [Telegram](https://t.me/noderoid64)\n' +
    '- The information here actual on 31.08.2022. If you read it one year later, it possible outdated.\n' +
    '\n' +
    '# :computer:Technologies\n' +
    'The app was done in a client-server manner. I tried to use as less libraries as possible (on the client side)<br>\n' +
    '## Client side :a::\n' +
    '### Main technologies\n' +
    '- Angular 14\n' +
    '- Typescript\n' +
    '- RxJs\n' +
    '- Angular Materials\n' +
    '- scss/css\n' +
    '### Used libs\n' +
    '- ngx-markdown\n' +
    '- prismjs\n' +
    '- @ngneat/until-destroy\n' +
    '## Server side:\n' +
    '### Main technologies:\n' +
    '- Asp.net core 3.1 (c#)\n' +
    '- Entity Framework core\n' +
    '- Automapper\n' +
    '- FluentValidator\n' +
    '- Serilog\n' +
    '- SimpleInject\n' +
    '- Swagger\n' +
    '- Postgress\n' +
    '## Deployment\n' +
    '- Digital Ocean hosting\n' +
    '- Ubuntu 18 OS\n' +
    '- Namecheap Dns + SSL\n' +
    '- aspnetcore-runtime 6.0\n' +
    '- Postgress\n' +
    '- Nginx\n' +
    '\n' +
    '# :hammer: How it was made\n' +
    '## Blogs\n' +
    '***\n' +
    '## Index search\n' +
    'When the blog part was almost implemented, I started to think about how actually a user could find an interesting post. I filled that using only DB tools will kill the performance of my app. So I decided to use a method called **Search engine indexing**.<be>\n' +
    'According to the Wikipedia:\n' +
    '> Search engine indexing is the collecting, parsing, and storing of data to facilitate fast and accurate information retrieval<br>\n' +
    '\n' +
    'Hard to understand from that quote? I agree)<br>\n' +
    'Let me explain what is it in the app\n' +
    '### The problem\n' +
    'Regular user is trying to find something by typing "Hybrid Locks". The system should find:<br>\n' +
    '- Posts containing the words "Hybrid" or "Locks" in the title. And better to contain both.\n' +
    '- Posts containing the same words in the content.<br>\n' +
    '\n' +
    'To do it we can use **contains** function of SQL or Entity Framework. But from my tests, it creates huge performance problems.\n' +
    '\n' +
    '### How to solve\n' +
    'A very simple solution came to my mind: to use a structure (file) that knows each word and posts that contain that word. In JSON format we can represent it like:<br>\n' +
    '```json\n' +
    '{\n' +
    '"hybrid": [1, 23, 12]\n' +
    '"locks": [1, 2, 22]\n' +
    '}\n' +
    '```\n' +
    'Where we store a word as a key and the related post\'s ids as an array of numbers. \n' +
    'And when we need to find something by the word "locks" for example, we should only find the line in the structure (*it is represented as c# dictionary, so we will find it very quickly*) and read all the ids. After that, we perform an exact search by ids. So there is no need to look over all posts in the DB.\n' +
    '\n' +
    'What if I tell you that the structure is a little bit more complicated? I decided to show to the user the context of his search. So he will be able to see 50 symbols before and after the searched word. It will help him to decide which post to select. And to do that I changed the structure in the following way:\n' +
    '```json\n' +
    '{\n' +
    '"hybrid": [\n' +
    '\t"1": [23, 37],\n' +
    '\t"23": [10, 56, 82],\n' +
    '\t"12": [0, 44]\n' +
    '],\n' +
    '...\n' +
    '}\n' +
    '```\n' +
    'So we finally have the word "hybrid" associated with three files (1, 23, 12). And each file is associated with an array of positions where the word "hybrid" is located. Sure I restrict the amount of positions in each file to 2. Just to reduce the size of the file.\n' +
    '\n' +
    'There is one additional trick: we need to store two copies of the index at the same time to use the first one as a **workingIndex** and the second one as **backupIndex**.  And when we need to add some record to the index, we use only **backupIndex** and then swap it with **workingIndex**. The search process use only **workingIndex**\n' +
    '\n' +
    '### How the index is populating\n' +
    'When you make changes to a post (or add new, or remove) the system stores the post id, modification type (*change, add ,remove*) and dateTime in the DB. Then, once in 5 minutes, for example, the separate job starts to populate the **backupIndex**. When it is done, we replace **workingIndex** by **backupIndex**.\n' +
    '***\n' +
    '## The Architecture\n' +
    'The logic behind the server structure is simple. <be>\n' +
    'We have next layers:\n' +
    '- **API** - simply controllers that receive HTTP calls. Also here is located mapping and parameter validation. The goal of the layer is to receive -> map -> validate -> delegate to next layer.\n' +
    '- **Background Jobs** - located on the same level as API. The main purpose is to trigger an event and delegate it to the next layer.\n' +
    '- **Services** - Optional layer. Here placed application logic that does not depend on domain logic. It is good place for authorization, Indexes and things like that.\n' +
    '- **Infrastructure** - Layer of tools for others. Here places details of libraries or frameworks. Usualy used for setup the app. Should be simple as possible. There you can find Serilog, SimpleInject, EF, configurations.\n' +
    '- **Core** - Layer of domain logic. Should not depend on anything.\n' +
    '```mermaid\n' +
    'graph TD;\n' +
    'A[Api]-->B[Services];\n' +
    'E[Background  Jobs]-->B\n' +
    'B-->C[Infrastructure]\n' +
    'C-->D[Core]\n' +
    '```\n' +
    '\n' +
    '### API layer\n' +
    '```\n' +
    '- Api \n' +
    '\t- Controllers\n' +
    '\t- Mappings\n' +
    '\t- Validators\n' +
    '\t- Dtos\t\n' +
    '```\n' +
    '\n' +
    '#### Controllers\n' +
    'A good example of controller:\n' +
    '```csharp\n' +
    '[HttpGet("{id}")]\n' +
    'public async Task<IActionResult> GetPost(int id)\n' +
    ' {\n' +
    '       var post = await _postWorkflow.GetPostAsync(id);\n' +
    '       if(post.IsSuccess)\n' +
    '            return Ok(_mapper.Map<PostDto>(post.Value));\n' +
    '       return BadRequest(post.ErrorMessage);\n' +
    ' }\n' +
    '```\n' +
    'I am trying to return **IActionResult** if it is possible. We delegate getting post by id to the Core layer. Then check it on success, map, and return result.\n' +
    '\n' +
    '#### Core\n' +
    'There exists a special kind of interface called Ports. Let\'s look for an example (IPostProvider):\n' +
    '```csharp\n' +
    'public interface IPostProvider\n' +
    '{\n' +
    '    public Task<FileObjectEntity> \t\tGetFileObjectAsync(int postId);\n' +
    '    public Task<FileObjectEntity> \t\tGetFileObjectRootAsync(int profileId);\n' +
    '    public Task<List<FileObjectEntity>> GetPostsByProfileIdAsync(int profileId);\n' +
    '    public Task<List<FileObjectEntity>> GetRecentPosts();\n' +
    '    public Task<FileObjectEntity> \t\tGetPostWithCommentsAsync(int postId);\n' +
    '    public void \t\t\t\t\t\tSaveFileObject(FileObjectEntity fileObject);\n' +
    '    public Task \t\t\t\t\t\tDeleteFileObjectAsync(FileObjectEntity fileObjectEntity);\n' +
    '    public Task \t\t\t\t\t\tSaveAsync();\n' +
    '}\n' +
    '```\n' +
    'It declare a contract which EF should implement. As you can remember, EF placed in **interfaces** layer. The trick called **Dependency Inversion**\n' +
    '\n' +
    '# To be continue...';

  constructor() { }

  ngOnInit(): void {
  }

}
