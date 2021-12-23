using System;
using System.Collections.Generic;

namespace AC2RE.Utils;

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

    public static void ProcessChunks<T>(this List<T> list, int chunkSize, Action<List<T>> chunkProcessor) {
        if (list.Count > chunkSize) {
            for (int i = 0; i < list.Count; i += chunkSize) {
                chunkProcessor.Invoke(list.GetRange(i, Math.Min(chunkSize, list.Count - i)));
            }
        } else {
            chunkProcessor.Invoke(list);
        }
    }
}
