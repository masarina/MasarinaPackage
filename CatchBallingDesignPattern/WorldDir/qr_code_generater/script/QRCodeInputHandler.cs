using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class QRCodeInputHandler : UdonSharpBehaviour
{
    // 仮想キーボードコンポーネントへの参照
    public UdonBehaviour virtualKeyboard;

    // QRコード生成スクリプトへの参照
    public UdonBehaviour qrCodeGenerator;

    void Start()
    {
        // 初期化処理が必要であればここに記述
    }

    public void OnEndEdit()
    {
        // キーボードからテキストを取得
        string inputText = (string)virtualKeyboard.GetProgramVariable("text");

        // QRコード生成スクリプトにテキストを渡して、QRコードを生成
        qrCodeGenerator.SetProgramVariable("inputText", inputText);
        qrCodeGenerator.SendCustomEvent("GenerateQRCode");
    }
}
