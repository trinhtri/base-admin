﻿using System.Linq.Expressions;

namespace Base.Application.Common.Extensions.QueryableExtensions;
public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }
}
