using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.ExtensionsMethod
{
    public static class QueryExtension
    {
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> list, object para, Func<T, bool> predicate )
        {
            if (para is null) return list;
            return list.Where(predicate);
        }

        public static IEnumerable<T> Paginaze<T>(this IEnumerable<T> list, int page, int size)
        {
            if (list == null) return list;
            return list.Skip(page == 1 ? 0 : (page - 1) * size)
                .Take(size);
        }
    }
}
