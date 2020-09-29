using System.Collections.Generic;

namespace AC2E.Utils {

    public static class ListExtensions {

        public static int InsertSafe<T>(this IList<T> list, int index, T item) {
            if (index < 0) {
                list.Insert(0, item);
                return 0;
            } else if (index >= list.Count) {
                list.Add(item);
                return list.Count - 1;
            } else {
                list.Insert(index, item);
                return index;
            }
        }
    }
}
