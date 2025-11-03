/* Copyright (c) threenine.co.uk . All rights reserved.
 
   GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
   This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using DGC.StaffManagement.Shared.Paging;

namespace DGC.StaffManagement.Application.Interfaces
{
    public interface IReadRepository<T> where T : class
    {
        T? FirstOrDefault(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = true);

        IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true);

        IPaginate<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0,
            int size = 20,
            bool enableTracking = true) where TResult : class;


        IEnumerable<T> GetFilterElements(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool ignoreQueryFilters = false,
            bool enableTracking = true);

        IEnumerable<TResult> GetFilterElementsAs<TResult>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool ignoreQueryFilters = false,
            bool enableTracking = true);

        public IQueryable<T> GetDbSetAsIQueryable();
    }
}