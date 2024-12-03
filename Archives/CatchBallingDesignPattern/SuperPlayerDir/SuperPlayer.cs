using UdonSharp;
using UnityEngine;

public class SuperPlayer : UdonSharpBehaviour
{
    public string myName;
    public TypeConverterToolkit typeConverterToolkit;
    public World world;
    private bool ResetScheduleStatusResult;


    public virtual string ExecuteMain() // これは呼ばれないはず。呼ばれてしまったら間違い。オーバーライドのし忘れ。
    {
        string e = "！！！！！！！！！！！！！！！！SuperPlayerのMain をオーバライドしていないプレイヤーがいます！！！！！！！！！！！！！！！！";
        Debug.LogError(e);

        Debug.Log("\n==== 現時点のメンバ変数の一覧を示します。 ==============================");
        
        // BallObjectのすべてのメンバ変数を出力
        Debug.Log($"nowSchedule: {typeConverterToolkit.Format2DArray(world.ball.nowSchedule)}"); // ちょっとまってね。これが一応今null参照してるってエラーが出てるの。でも、いったんコメアウトで、全体のエラーを確認しよう。
        Debug.Log($"nowScheduleStatus: {typeConverterToolkit.Format2DArray(world.ball.nowScheduleStatus)}");
        Debug.Log($"scheduleMode: {world.ball.scheduleMode}");
        Debug.Log($"indexOfSchedule: {world.ball.indexOfSchedule}");
        Debug.Log($"indexOfScheduleOfSchedule: {world.ball.indexOfScheduleOfSchedule}");
        Debug.Log($"nowCatchBallingPlayer: {world.ball.nowCatchBallingPlayer}");
        Debug.Log($"nextPlayerName: {world.ball.nextPlayerName}");

        Debug.Log("\n==== 現時点のメンバ変数の一覧を示します。 (END) ==============================\n");

        return e;
    }

    public virtual string ReturnMyName()
    {
        string e = "ReturnMyName をオーバライドしていないプレイヤーがいます！";
        Debug.LogError(e);
        return e;
    }

    public bool CatchBall(World world)
    {

        // 初期化
        bool _result = true;

        if (world == null)
        {
            Debug.LogError("World object is null!");
            return false;
        }

        if (world.ball == null)
        {
            Debug.LogError("Ball object is null!");
            return false;
        }

        // 変数の準備
        world.ball.nextPlayerName = null;

        // CatchBallが渡されたplayerの名前を代入
        myName = ReturnMyName();

        // 一つ前のプレイヤーのステータスを参考にして
        // self.Main の実行の有無を決めて、self.Main の処理。
        // プレイヤーself自身のステータスも更新する。

        world.ball.nowCatchBallingPlayer = myName; // ここでself.my_nameを確認


        // ひとつ前のプレイヤーのステータスを取得
        string beforePlayerStatus = world.GetBeforePlayerStatus(world, myName); // An exception occurred during Udon execution, this UdonBehaviour will be halted.VRC.Udon.VM.UdonVMException: The VM encountered an error!


        string result = world.DoingNowPlayersMainAndUpdateNowPlayersStatus(
            world, beforePlayerStatus, myName
        );

        // of_schedule_of_schedule を 一つ進める
        result = world.GoToNextIndexOfScheduleOfSchedule(world);

        // ミニスケジュール内のplayerがすべて"Completed"だった場合、
        // 次のミニスケジュールに遷移
        if (result == "over!")
        {
            result = world.GoToNextIndexOfSchedule(world);
        }

        // 今回実行しているスケジュールモードの全ステータスが
        // "Completed"の場合、次のschedule_modeに遷移、
        // 次のモードのスケジュールとステータスの準備
        // ↪　now_cheduleの更新、now_chedule_statusの初期化
        if (result == "range over!")
        {
            world.GoToNextScheduleMode(world);
            ResetScheduleStatusResult = world.ball.ResetScheduleStatus();
        }

        // 次のプレイヤーのcatch_ballを実行する
        int axis_0 = world.ball.indexOfSchedule;
        int axis_1 = world.ball.indexOfScheduleOfSchedule;

        // 次のプレイヤーの名前を取得
        world.ball.nextPlayerName = world.ball.nowSchedule[axis_0][axis_1];


        // Debug.Log($"\n==== {this.myName}のCatchBallが実行されました ==============================");
        // // BallObjectのすべてのメンバ変数を出力
        // Debug.Log($"nowSchedule: {typeConverterToolkit.Format2DArray(world.ball.nowSchedule)}"); // ちょっとまってね。これが一応今null参照してるってエラーが出てるの。でも、いったんコメアウトで、全体のエラーを確認しよう。
        // Debug.Log($"nowScheduleStatus: {typeConverterToolkit.Format2DArray(world.ball.nowScheduleStatus)}");
        // Debug.Log($"scheduleMode: {world.ball.scheduleMode}");
        // Debug.Log($"indexOfSchedule: {world.ball.indexOfSchedule}");
        // Debug.Log($"indexOfScheduleOfSchedule: {world.ball.indexOfScheduleOfSchedule}");
        // Debug.Log($"nowCatchBallingPlayer: {world.ball.nowCatchBallingPlayer}");
        // Debug.Log($"nextPlayerName: {world.ball.nextPlayerName}");
        // Debug.Log($"\n==== {this.myName}のCatchBallが実行されました (END) ==============================\n\n\n\n\n\n\n\n");




        // ミニスケジュールが終わったら、これを実行するべきではないでしょうか。
        // ここで、FinalPlayerの時、次のスケジュールのFirstPlayerの実行がなされている。（2024年8月15日）
        if (this.myName != "FinalPlayer")
        {
            _result = world.DoingNextPlayersCatchBallMethodByPlayerName(
                world.ball.nextPlayerName, world
            );
        }

        else if (this.myName == "FinalPlayer")
        {
            _result = true;
        }

        return _result; 

    }
}
