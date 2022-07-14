using System.Collections.Generic;
using System.Linq;
using PersonalSite.Core.Blog.Services;
using PersonalSite.Core.Models.Entities;
using Xunit;

namespace PersonalSiteTests.Core.Blog.Services;

public class FileObjectBuilderTest
{
    [Fact]
    public void Build_InnerObjects()
    {
        // Arrange
        var builder = new FileObjectBuilder();
        var entities = new List<FileObjectEntity>()
        {
            new FileObjectEntity()
            {
                Id = 1,
            },
            new FileObjectEntity()
            {
                Id = 2,
                ParentId = 1
            },
            new FileObjectEntity()
            {
                Id = 3,
                ParentId = 1
            }
        };
        // Act
        var result = builder.Build(entities);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Children);
        Assert.True(result.Children.Count == 2);
        Assert.True(result.Children.FirstOrDefault().Id != 1);
    }
}