
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using ureishi.UKeyboard.Udon; // 名前空間をインポート （うれいしが名前空間使ってるから引き継がなきゃいけない、くそっ。）


// 【README】
// 新しいPlayerを作成する場合、次の手順が必要です。（指定先に行き、そこの【README】に従ってください。）
// ============================================================================================
// World.csのインスタンスの変数を更新して下さい。
// World.csのbool変数を更新してください。
// World.csのWorldResetを更新してください。
// World.csのDoingNowPlayersMainAndUpdateNowPlayersStatusを更新してください。
// World.csのDoingNextPlayersCatchBallMethodByPlayerNameを更新してください。
// このファイルのInputAndPrintPlayerDirResultを更新してください。
// このファイルのExecuteMainを更新してください。
// ============================================================================================


// RKeyboardに打ち込まれた文字列をframe_textでprintします。
// printする文字数の設定等はframe_textで設定をお願いします。


public class TemplatePlayer : SuperPlayer
{

    // 初期化
    public string playerName = "TemplatePlayer"; // このプレイヤーの名前
    public Frame_text frame_Text; // テキストメッシュプロに書き込むオブジェクト
    public RKeyboard rKeyboard; // キーボードに打ち込まれた情報を持ってるオブジェクト
    public string inputText = ""; // 入力される文字列の箱



    //【README】
    // メソッド名を次のように変更して下さい
    // ============================================
    // public bool NewPlayerNameReset()    
    // ============================================
    public bool InputAndPrintPlayerDirResult()
    {
        myName = playerName;
        return true;
    }



    public override string ReturnMyName()
    {
        return playerName;
    }


    public override string ExecuteMain()
    // 【README】
    // ExecuteMainないに、任意の機能を実装してください。
    //
    //
    // (このメソッドの戻り値について)
    // [同期関数の場合]
    // ・戻り値は、"Completed"としてください。
    //
    // [非同期関数の場合]
    // ・目的が達成していない時の戻り値は、"None"としてください。
    // ・目的が達成した場合の戻り値は、"Completed"としてください。
    // (尚、"Completed"を返さない限り、次のPlayerは実行されないようにデザインされています。)
    // (スケジュール自体もここでストップし、常にこの非同期関数に注目された状態になります。)


    {
        // 入力された文字列をballに渡します。

        Debug.Log($"\n==== {playerName} ExecuteMain ==============================");

        // キーボードからテキストを取得
        if (inputText == null) // 特に文字列が指定されていなければ
        {
            if (rKeyboard.text_push_flag == true) // 新しい入力のみtrueになるので。
            {
                // テキストを抽出(デバッグ重視です。一応ballに変数を持たせます。)
                world.ball.inputText = rKeyboard.previousStringHolderText;

                // テキストを出力
                frame_Text.WriteStr(world.ball.inputText);
            }

        }

        Debug.Log($"\n==== {playerName} ExecuteMain (END) ==============================\n");

        return "Completed"; 
        // 処理が完了した場合、"Completed"を返してください。
        // 実装したものが非同期関数であり、非同期関数の目的を果たすまで待機する場合は、
        // その戻り値が目的を果たすまでは"None"を返してください。
        // （"Completed"を返さない限り、次のPlayerは実行されないようにデザインしてあります。）
    }

}





