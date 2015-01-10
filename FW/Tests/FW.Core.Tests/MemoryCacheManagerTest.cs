using FW.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;

namespace FW.Core.Tests
{
    public class MemoryCacheManagerTest
    {
        private readonly ICacheManager cacheManager;

        public MemoryCacheManagerTest()
        {
            cacheManager = new MemoryCacheManager();
        }

        [Fact]
        public void GetNullTest()
        {
            cacheManager.Get<string>("GetNullTest")
                .Should()
                .BeNullOrEmpty();
        }


        [Theory]
        [InlineData(100)]
        [InlineData("Hello world")]
        public void GetTest(object expected)
        {
            cacheManager.Get(expected.ToString(), () => expected)
                .Should()
                .Be(expected);
        }

        [Fact]
        public void GetCacheTest()
        {
            Tmp tmp = new Tmp() { Str = "A" };

            cacheManager.Get("GetCacheTest", () => tmp)
                .Str
                .Should()
                .Be("A");

            tmp.Str = "B";

            cacheManager.Get("GetCacheTest", () => new Tmp() { Str = "C" })
                .Str
                .Should()
                .Be("B");
        }

        private class Tmp
        {
            public string Str { get; set; }
        }
    }
}
