namespace Lipstick
{
    using UnityEngine;

    public static partial class Lipstick
    {
        public static string Black(this string str)
        {
            return $"<color=#000000>{str}</color>";
        }

        public static string Bold(this string str)
        {
            return $"<b>{str}</b>";
        }

        public static string Blue(this string str)
        {
            return $"<color=#2196f3>{str}</color>";
        }

        public static string Brown(this string str)
        {
            return $"<color=#795548>{str}</color>";
        }

        public static string Color(this string str, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{str}</color>";
        }

        public static string Green(this string str)
        {
            return $"<color=#4caf50>{str}</color>";
        }

        public static string Gray(this string str)
        {
            return $"<color=#9e9e9e>{str}</color>";
        }

        public static string Indigo(this string str)
        {
            return $"<color=#3f51b5>{str}</color>";
        }

        public static string Italic(this string str)
        {
            return $"<i>{str}</i>";
        }

        public static string Lime(this string str)
        {
            return $"<color=#cddc39>{str}</color>";
        }

        public static string NewLine(this string str)
        {
            return $"{str}\n";
        }

        public static string Orange(this string str)
        {
            return $"<color=#ff9800>{str}</color>";
        }

        public static string Red(this string str)
        {
            return $"<color=#f44336>{str}</color>";
        }

        public static string Pink(this string str)
        {
            return $"<color=#e91e63>{str}</color>";
        }

        public static string Purple(this string str)
        {
            return $"<color=#9c27b0>{str}</color>";
        }

        public static string Size(this string str, int size)
        {
            return $"<size={size}>{str}</size>";
        }

        public static string White(this string str)
        {
            return $"<color=#ffffff>{str}</color>";
        }

        public static string Yellow(this string str)
        {
            return $"<color=#ffeb3b>{str}</color>";
        }
    }
}
