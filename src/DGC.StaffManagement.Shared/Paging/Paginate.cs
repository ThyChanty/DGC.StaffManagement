using Newtonsoft.Json;

namespace DGC.StaffManagement.Shared.Paging
{
    public class Paginate<T> : IPaginate<T>
    {
        public Paginate(IEnumerable<T> source, int index, int size, int from)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var enumerable = source as T[] ?? source.ToArray();

            // Handle default/zero values
            PageIndex = Math.Max(index, 1); // Ensure at least page 1
            PageSize = size > 0 ? size : enumerable.Length; // If size=0, return all
            From = Math.Min(from, PageIndex); // Ensure From doesn't exceed PageIndex

            TotalItem = enumerable.Count();

            // Fix total pages calculation to prevent division by zero
            TotalPages = PageSize > 0 ? (int)Math.Ceiling(TotalItem / (double)PageSize) : 1;

            // Fix skip calculation
            var skipCount = (PageIndex - From) * PageSize;
            if (skipCount < 0) skipCount = 0;

            Items = enumerable.Skip(skipCount).Take(PageSize).ToList();
        }

        public Paginate()
        {
            Items = Array.Empty<T>();
        }

        [JsonIgnore]
        public int From { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPages { get; set; }
        public IList<T> Items { get; set; }
        public bool HasPreviousPage => PageIndex > 1; // Simplified
        public bool HasNextPage => PageIndex < TotalPages; // Simplified
    }

    internal class Paginate<TSource, TResult> : IPaginate<TResult>
    {
        public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
            int pageIndex, int pageSize, int from)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            var enumerable = source as TSource[] ?? source.ToArray();

            // Handle default/zero values
            PageIndex = Math.Max(pageIndex, 1);
            PageSize = pageSize > 0 ? pageSize : enumerable.Length;
            From = Math.Min(from, PageIndex);

            TotalItem = enumerable.Count();
            TotalPages = PageSize > 0 ? (int)Math.Ceiling(TotalItem / (double)PageSize) : 1;

            // Fix skip calculation
            var skipCount = (PageIndex - From) * PageSize;
            if (skipCount < 0) skipCount = 0;

            var items = enumerable.Skip(skipCount).Take(PageSize).ToArray();
            Items = new List<TResult>(converter(items));
        }

        public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            PageIndex = source.PageIndex;
            PageSize = source.PageSize;
            From = source.From;
            TotalItem = source.TotalItem;
            TotalPages = source.TotalPages;
            Items = new List<TResult>(converter(source.Items));
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalItem { get; }
        public int TotalPages { get; }
        [JsonIgnore]
        public int From { get; }
        public IList<TResult> Items { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }

    public static class Paginate
    {
        public static IPaginate<T> Empty<T>()
        {
            return new Paginate<T>();
        }

        public static IPaginate<TResult> From<TResult, TSource>(IPaginate<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            return new Paginate<TSource, TResult>(source, converter);
        }
    }
}