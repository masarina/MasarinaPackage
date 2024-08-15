using UdonSharp;
using UnityEngine;
using UnityEngine.UI; // UI要素を扱うために必要
using VRC.SDKBase;
using VRC.Udon;

public class InputToQRCode : UdonSharpBehaviour
{
    public InputField inputField; // ユーザー入力を受け取るInputField
    // public YourQRCodeGenerator qrCodeGenerator; // QRコードを生成するクラス

    public void ConvertInputToQRCode()
    {
        string userInput = inputField.text; // InputFieldからテキストを取得
        // qrCodeGenerator.GenerateQRCode(userInput); // QRコード生成メソッドを呼び出す
    }
}
