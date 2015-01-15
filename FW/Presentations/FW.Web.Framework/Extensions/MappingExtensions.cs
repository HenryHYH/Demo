namespace FW.Web.Framework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using FW.Core;
    using FW.Web.Framework.Datasource;
    using FW.Web.Framework.UI.Pagination;

    public static class MappingExtensions
    {
        #region Methods

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public static DatasourceResult<TDestination> ToModel<TSource, TDestination>(this IPagedList<TSource> data)
        {
            return new DatasourceResult<TDestination>()
            {
                Data = data.Select(x => x.MapTo<TSource, TDestination>()).ToList(),
                Pager = new Pager(data.PageIndex, data.PageSize) { TotalRecords = data.TotalRecords }
            };
        }

        #endregion Methods
    }
}