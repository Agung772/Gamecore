using System;
using System.Linq;
using UnityEngine;

public static class EnumExtensions
{
    public static T ToEnum<T>(this string text) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), text, true);
    }
        
    public static int GetLength<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum)).Length;
    }
    
    public static string ToSpace(this Enum text)
    {
        var _result = text.ToString();
        if (string.IsNullOrEmpty(_result)) return _result;

        return string.Concat(_result.Select((ch, i) => 
            i > 0 && char.IsUpper(ch) ? " " + ch : ch.ToString()));
    }
}
