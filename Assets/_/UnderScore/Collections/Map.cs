namespace UnderScore
{
    using System.Collections.Generic;

    public static partial class _
    {
        public static T[] Map <T> (T[] array, ArrayValueReturnDelegate<T> callback)
        {
            T[] result = new T[array.Length];

            for (int i = 0, size = array.Length; i < size; ++i)
            {
                result[i] = callback(array[i], i, array);
            }

            return result;
        }

        public static List<T> Map <T> (List<T> list, ListValueReturnDelegate<T> callback)
        {
            List<T> result = new List<T>();

            for (int i = 0, size = list.Count; i < size; ++i)
            {
                result.Add(callback(list[i], i, list));
            }

            return result;
        }

        public static List<U> Map <T, U> (
            Dictionary<T, U> dictionary,
            DictionaryValueReturnDelegate<T, U> callback
        )
        {
            List<U> result = new List<U>();

            foreach (KeyValuePair<T, U> pair in dictionary)
            {
                result.Add(callback(pair.Value, pair.Key, dictionary));
            }

            return result;
        }
    }
}
