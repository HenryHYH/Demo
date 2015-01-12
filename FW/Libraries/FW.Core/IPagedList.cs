namespace FW.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// 分页列表接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IList<T>
    {
        #region Properties

        bool HasNextPage
        {
            get;
        }

        bool HasPreviousPage
        {
            get;
        }

        int PageIndex
        {
            get;
        }

        int PageSize
        {
            get;
        }

        int TotalRecords
        {
            get;
        }

        int TotalPages
        {
            get;
        }

        #endregion Properties
    }
}