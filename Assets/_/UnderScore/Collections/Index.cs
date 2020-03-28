namespace UnderScore
{
    using System.Collections.Generic;

    /// <deprecated>
    /// ArrayList, Hashtable
    /// </deprecated>
    public static partial class _
    {
        /// Array
        public delegate void ArrayDelegate <T> (T v, int i, T[] a);
        public delegate T ArrayValueReturnDelegate <T> (T v, int i, T[] a);
        public delegate string ArrayKeyReturnDelegate <T> (T v, int i, T[] a);

        /// List
        public delegate void ListDelegate <T> (T v, int i, List<T> a);
        public delegate T ListValueReturnDelegate <T> (T v, int i, List<T> a);
        public delegate string ListKeyReturnDelegate <T> (T v, int i, List<T> a);

        /// Dictionary
        public delegate void DictionaryDelegate <T, U> (U v, T k, Dictionary<T, U> a);
        public delegate U DictionaryValueReturnDelegate <T, U> (U v, T k, Dictionary<T, U> a);
        public delegate T DictionaryKeyReturnDelegate <T, U> (U v, T k, Dictionary<T, U> a);
    }
}
