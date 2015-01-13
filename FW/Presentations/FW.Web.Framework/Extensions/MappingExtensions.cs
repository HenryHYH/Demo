using AutoMapper;
using FW.Core;
using FW.Web.Framework.Datasource;
using FW.Web.Framework.UI.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FW.Web.Framework.Extensions
{
    public static class MappingExtensions
    {
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
    }
}