namespace DGC.StaffManagement.Shared.Paging
{
    public interface IPaginate<T>
    {
        int From { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalItem { get; }

        int TotalPages { get; }

        IList<T> Items { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}