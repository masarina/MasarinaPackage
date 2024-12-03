using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TypeConverterToolkit : UdonSharpBehaviour
{
    public string Format2DArray(string[][] array)
    {
        if (array == null)
        {
            return "null";
        }
        else
        {
            string formattedString = "";

            foreach (var subArray in array)
            {
                formattedString += "[ ";
                foreach (var item in subArray)
                {
                    formattedString += item + " ";
                }
                formattedString += "]\n";
            }

            return formattedString;
        }
    }

    // 新しく1D配列用のフォーマットメソッドを追加するよ！
    public string FormatArray(string[] array)
    {
        if (array == null)
        {
            return "null";
        }
        else
        {
            string formattedString = "[ ";
            foreach (var item in array)
            {
                formattedString += item + " ";
            }
            formattedString += "]";
            return formattedString;
        }
    }
}
