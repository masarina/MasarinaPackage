
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ArrayToolkit : UdonSharpBehaviour
{

    // 二次元配列の全ての要素を"None"に初期化するメソッド
    public static string[][] ToAllNone_Array2d(string[][] inputArray)
    {
        // 入力配列と同じ行数の新しい配列を作成
        string[][] outputArray = new string[inputArray.Length][];

        // 各行を"None"で初期化
        for (int i = 0; i < inputArray.Length; i++)
        {
            // 各行の長さを取得して新しい配列を作成
            outputArray[i] = new string[inputArray[i].Length];
            for (int j = 0; j < inputArray[i].Length; j++)
            {
                outputArray[i][j] = "None"; // 要素を"None"で初期化
            }
        }

        return outputArray; // 初期化した配列を返す
    }
}
