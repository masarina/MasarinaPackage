using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;
using System.Text;

public class Frame_text : UdonSharpBehaviour
{
    // TextMeshProのオブジェクトをアタッチする。
    public TextMeshPro debugText;
    
    public string WriteStr(string Text = "test")
    {
        // ターゲットオブジェクトの座標をログに表示する
        debugText.text += Text + "\n";
        return Text;
    }


    public void WriteStrOverwrite(string Text = "test")
    {
        // ターゲットオブジェクトの座標をログに表示する
        debugText.text = Text;
    }

    public int WriteInt(int IntVal= 0)
    {
        // ターゲットオブジェクトの座標をログに表示する
        debugText.text += IntVal.ToString() + "\n";
        return IntVal;
    }
    public void WriteFloat2D(float[][] Float2D = null)
    {
        // ターゲットオブジェクトの座標をログに表示する
        debugText.text += Array2DToString(Float2D) + "\n";
    }

    // public string WriteByteArray(byte[] byteArray = null)
    // {
    //     if (byteArray == null)
    //     {
    //         return "byteArrayはnull\n";
    //     }

    //     debugText.text += "[";
    //     for (int i = 0; i < byteArray.Length; i++)
    //     {
    //         // バイト値を16進数の文字列に変換
    //         debugText.text += byteArray[i].ToString("X2"); // X2フォーマット指定子を使用して2桁の16進数にする
    //         if (i < byteArray.Length - 1)
    //         {
    //             debugText.text += ", ";
    //         }
    //     }
    //     debugText.text += "]\n";
    //     return debugText.text;
    // }

    public string WriteByteArray(byte[] byteArray = null)
    {
        if (byteArray == null)
        {
            return "byteArrayはnull\n";
        }

        string result = "[";
        for (int i = 0; i < byteArray.Length; i++)
        {
            // バイト値を16進数の文字列に変換
            result += byteArray[i].ToString("X2"); // X2フォーマット指定子を使用して2桁の16進数にする
            if (i < byteArray.Length - 1)
            {
                result += ", ";
            }
        }
        result += "]\n";

        debugText.text += result;
        return result;
    }




    public void WriteBool2DArray(bool[][] bool2DArray = null)
    {
        if (bool2DArray == null)
        {
            debugText.text += "[]\n";
            return;
        }

        string result = "[\n";
        for (int i = 0; i < bool2DArray.Length; i++)
        {
            result += "  [";
            for (int j = 0; j < bool2DArray[i].Length; j++)
            {
                result += bool2DArray[i][j] ? "□" : "  ";
                if (j < bool2DArray[i].Length - 1)
                {
                    result += ", ";
                }
            }
            result += "]";
            if (i < bool2DArray.Length - 1)
            {
                result += ",\n";
            }
        }
        result += "\n]";
        debugText.text += result;
    }



    public string Array2DToString(float[][] jaggedArray = null)
    {
        if (jaggedArray == null)
        {
            return "[]";
        }

        string result = "[";
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            result += "[";
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                result += jaggedArray[i][j].ToString();
                if (j < jaggedArray[i].Length - 1)
                {
                    result += ", ";
                }
            }
            result += "]";
            if (i < jaggedArray.Length - 1)
            {
                result += ", ";
            }
        }
        result += "]";
        return result;
    }

    public string ArrayToString(int[] array = null)
    {
        if (array == null)
        {
            return "[]";
        }

        string result = "[";
        for (int i = 0; i < array.Length; i++)
        {
            result += array[i].ToString();
            if (i < array.Length - 1)
            {
                result += ", ";
            }
        }
        result += "]";
        return result;
    }

    public string WriteIntArray(int[] Array = null)
    {
        if (Array == null)
        {
            debugText.text += "[]\n";
            return "入力された引数がnullです。 by Frame_text.cs.WriteIntArray より。";
        }

        string result = "[";
        for (int i = 0; i < Array.Length; i++)
        {
            result += Array[i].ToString();
            if (i < Array.Length - 1)
            {
                result += ", ";
            }
        }
        result += "]\n";
        debugText.text += result;

        return result;
    }


    // 使えるかもしれないオプション
    // float[][]をprintする関数
    public string print_float_2m(float[][] jaggedArray)
    {
        if (jaggedArray != null)
        {
            Debug.Log(jaggedArray);

            string output_ = "[";
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                output_ += "[";
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    output_ += jaggedArray[i][j].ToString();
                    if (j < jaggedArray[i].Length - 1)
                    {
                        output_ += ", ";
                    }
                }
                output_ += "]";
                if (i < jaggedArray.Length - 1)
                {
                    output_ += ", ";
                }
            }
            output_ += "]";
            string text_ = output_;
            return text_;
        }
        else
        {
            string text_ = "";
            return text_;
        }
    }




}
