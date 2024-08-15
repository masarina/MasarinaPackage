using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;
using System.Text;

// QRコードのデータモードを定義する列挙型
public enum QRMode { Numeric = 0b0001, Alphanumeric = 0b0010, Byte = 0b0100, Kanji = 0b1000 }

public class QRCodeGenerator2 : UdonSharpBehaviour
{
    public Frame_text frameText; // QRコード生成の進捗や結果を表示するためのフレームテキストのインスタンスをインスペクタからアタッチ

    public QRMode mode = QRMode.Alphanumeric; // デフォルトのQRコードモードをアルファベットモードに設定

    private void Start()
    {
        string sampleData = "ABCDE123"; // サンプルデータ
        Debug.Log(frameText.WriteStr("選択されたモード: " + mode.ToString())); // 選択されたモードを表示
        QRCodeGenerate(sampleData); // QRコードを生成するメソッドを呼び出す
    }

    // データをエンコードしてQRコードを生成するメソッド
    private void QRCodeGenerate(string data)
    {
        Debug.Log(frameText.WriteStr("生成するデータ: " + data)); // 生成するデータを表示
        string encodedBits; // エンコードされたビット列を格納する変数
        switch (mode)
        {
            case QRMode.Numeric:
                encodedBits = EncodeNumericDataToBits(data); // 数字モードのデータエンコード
                break;
            case QRMode.Alphanumeric:
                encodedBits = EncodeAlphanumericDataToBits(data); // 英数字モードのデータエンコード
                break;
            case QRMode.Byte:
                encodedBits = EncodeByteDataToBits(data); // バイトモードのデータエンコード
                break;
            default:
                Debug.Log(frameText.WriteStr("未対応のモードです: " + mode.ToString())); // サポートされていないモードを表示
                return;
        }
        Debug.Log(frameText.WriteStr("エンコードされたデータ: " + encodedBits)); // エンコードされたビット列を表示
    }

    // 数字データをビット列にエンコードするメソッド
    public string EncodeNumericDataToBits(string data)
    {
        string result = ""; // エンコード結果を格納する変数

        for (int i = 0; i < data.Length; i += 3)
        {
            int num = int.Parse(data.Substring(i, Mathf.Min(3, data.Length - i))); // 3桁ずつの数字を整数に変換
            string binaryString = ToBinaryString(num, 10); // 10ビットの2進数に変換
            result += binaryString; // エンコード結果に追加
        }

        return result; // エンコードされたビット列を返す
    }

    // 英数字データをビット列にエンコードするメソッド
    public string EncodeAlphanumericDataToBits(string data)
    {
        string modeIndicator = "0010"; // 英数字モードを示すビット列
        string charCountIndicator = ToBinaryString(data.Length, 9); // データの文字数をビット列に変換

        string encodedBits = modeIndicator + charCountIndicator; // モード指示子と文字数指示子を結合

        for (int i = 0; i < data.Length; i += 2)
        {
            int val1 = GetAlphanumericValue(data[i]); // 1文字目の英数字値を取得
            int val2 = (i + 1 < data.Length) ? GetAlphanumericValue(data[i + 1]) : -1; // 2文字目の英数字値を取得

            int combinedValue;
            if (val2 == -1)
            {
                combinedValue = val1; // 2文字目がない場合は1文字目のみ
            }
            else
            {
                combinedValue = 45 * val1 + val2; // 2文字を45進数として結合
            }

            if (val2 == -1)
            {
                encodedBits += ToBinaryString(combinedValue, 6); // 1文字目のみを6ビットの2進数に変換
            }
            else
            {
                encodedBits += ToBinaryString(combinedValue, 11); // 2文字を11ビットの2進数に変換
            }
        }

        // 終端パターンを追加
        encodedBits += "0000";

        // 8ビット単位にパディングする
        int bitLength = (encodedBits.Length + 7) / 8 * 8;
        encodedBits = encodedBits.PadRight(bitLength, '0');

        return encodedBits; // エンコードされたビット列を返す
    }

    // 英数字モードの各文字に対応する値を取得するメソッド
    private int GetAlphanumericValue(char c)
    {
        string alphanumericChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ $%*+-./:"; // 英数字モードで使用可能な文字
        return alphanumericChars.IndexOf(c); // 文字に対応するインデックスを返す
    }

    // 指定された長さのビット列に整数を変換するメソッド
    private string ToBinaryString(int value, int length)
    {
        char[] binary = new char[length]; // 指定された長さのビット列を保持する配列
        for (int i = length - 1; i >= 0; i--)
        {
            binary[i] = (value & 1) == 1 ? '1' : '0'; // 各ビットを計算して設定
            value >>= 1; // 右にシフトして次のビットを処理
        }
        return new string(binary); // ビット列を文字列として返す
    }

    // バイトデータをビット列にエンコードするメソッド
    public string EncodeByteDataToBits(string data)
    {
        string result = ""; // エンコード結果を格納する変数
        for (int i = 0; i < data.Length; i++)
        {
            result += ToBinaryString(data[i], 8); // 各文字をそのまま8ビットに変換
        }
        return result; // エンコードされたビット列を返す
    }
}
