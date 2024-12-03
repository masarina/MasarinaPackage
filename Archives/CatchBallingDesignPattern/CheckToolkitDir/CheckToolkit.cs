
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CheckToolkit : UdonSharpBehaviour
{
    public bool Completed_Array_2d(string[][] array)
    {
        if (array == null)
        {
            // 各要素をチェック
            for (int i = 0; i < array. Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] != "Completed")
                    {
                        return false; // "Completed"ではない要素が見つかった場合、falseを返す
                    }
                }
            }
            return true; // すべての要素が"Completed"ならtrueを返す
        }

        else
        {
            Debug.Log("arrayがnullです。");
            return false;
        }
    }
}
