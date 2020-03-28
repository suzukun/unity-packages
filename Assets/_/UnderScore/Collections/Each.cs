namespace UnderScore
{
    using System.Collections.Generic;

    public static partial class _
    {
        public static void Each <T> (T[] array, ArrayDelegate<T> callback)
        {
            for (int i = 0, size = array.Length; i < size; ++i)
            {
                callback(array[i], i, array);
            }
        }

        public static void Each <T> (List<T> list, ListDelegate<T> callback)
        {
            for (int i = 0, size = list.Count; i < size; ++i)
            {
                callback(list[i], i, list);
            }
        }

        public static void Each <T, U> (
            Dictionary<T, U> dictionary,
            DictionaryDelegate<T, U> callback
        )
        {
            foreach (KeyValuePair<T, U> pair in dictionary)
            {
                callback(pair.Value, pair.Key, dictionary);
            }
        }
    }
}
