using UdonSharp;
using UnityEngine;

public class DebugPlayer : SuperPlayer
{
    public bool DebugPlayerReset()
    {
        myName = "DebugPlayer";

        return true;
    }

    public override string ReturnMyName()
    {
        return "DebugPlayer";
    }


    public override string ExecuteMain()
    {
        Debug.Log("\n==== Debug ExecuteMain ==============================");
        
        // BallObjectのすべてのメンバ変数を出力
        Debug.Log($"nowSchedule: {typeConverterToolkit.Format2DArray(world.ball.nowSchedule)}"); // ちょっとまってね。これが一応今null参照してるってエラーが出てるの。でも、いったんコメアウトで、全体のエラーを確認しよう。
        Debug.Log($"nowScheduleStatus: {typeConverterToolkit.Format2DArray(world.ball.nowScheduleStatus)}");
        Debug.Log($"scheduleMode: {world.ball.scheduleMode}");
        Debug.Log($"indexOfSchedule: {world.ball.indexOfSchedule}");
        Debug.Log($"indexOfScheduleOfSchedule: {world.ball.indexOfScheduleOfSchedule}");
        Debug.Log($"nowCatchBallingPlayer: {world.ball.nowCatchBallingPlayer}");
        Debug.Log($"nextPlayerName: {world.ball.nextPlayerName}");

        Debug.Log("\n==== Debug ExecuteMain (END) ==============================\n");

        return "Completed";
    }

}
