namespace FW.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;

    using FW.Core.Caching;

    using Xunit;
    using Xunit.Extensions;

    public class MemoryCacheManagerTest
    {
        #region Fields

        private readonly ICacheManager cacheManager;

        #endregion Fields

        #region Constructors

        public MemoryCacheManagerTest()
        {
            cacheManager = new MemoryCacheManager();
        }

        #endregion Constructors

        #region Methods

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

        #endregion Methods

        #region Nested Types

        private class Tmp
        {
            #region Properties

            public string Str
            {
                get; set;
            }

            #endregion Properties
        }

        #endregion Nested Types
    }
}