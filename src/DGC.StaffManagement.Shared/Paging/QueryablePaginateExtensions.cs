using Microsoft.EntityFrameworkCore;

namespace DGC.StaffManagement.Shared.Paging
{
    public static class QueryablePaginateExtensions
    {
        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size,
            int from = 0, CancellationToken cancellationToken = default)
        {
            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            List<T> items;
            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

            if (size >= 0)
            {
                items = await source
                    .Skip((index - from) * size)
                    .Take(size)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            else
            {
                items = await source.ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            var list = new Paginate<T>
            {
                PageIndex = index,
                PageSize = size,
                From = from,
                TotalItem = count,
                Items = items,
                TotalPages = (int) Math.Ceiling(count / (double) size)
            };
            list.TotalPages = list.TotalPages < 0 ? 0 : list.TotalPages;

            return list;
        }
    }
}