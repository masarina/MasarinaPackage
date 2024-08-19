using UdonSharp;
using UnityEngine;

public class World : UdonSharpBehaviour
{

    // 【README】
    // 下記のように追加してください
    // ============================================================
    //     public NewPlayerName newPlayerName;
    // ============================================================
    public BallObject ball;
    public FirstPlayer firstPlayer;
    public FinalPlayer finalPlayer;
    public DebugPlayer debugPlayer;
    public InputAndPrintPlayerDir inputAndPrintPlayerDir;

    // 【README】
    // 下記のように追加してください
    // ============================================================
    //     private bool NewPlayerNameResetResult;
    // ============================================================
    private bool BallResetResult;
    private bool FirstPlayerResetResult;
    private bool FinalPlayerResetResult;
    private bool DebugPlayerResetResult;
    private bool ScheduleModeSettingsResult;
    private bool ResetScheduleStatusResult;
    private bool CatchBallResult;
    private bool InputAndPrintPlayerDirResult;



    public bool WorldReset()
    {
        // 【README】
        // 下記のように追加してください
        // ========================================================
        // NewPlayerNameResetResult = newPlayerNameReset.NewPlayerNameReset();
        // ========================================================
        // 各Layerのリセット
        BallResetResult = ball.BallReset();
        FinalPlayerResetResult = firstPlayer.FirstPlayerReset();
        FinalPlayerResetResult = finalPlayer.FinalPlayerReset();
        DebugPlayerResetResult = debugPlayer.DebugPlayerReset();
        InputAndPrintPlayerDirResult = inputAndPrintPlayerDir.InputAndPrintPlayerDirResult();

        return true;
    }

    public string GetBeforePlayerStatus(World world, string myName)
    {
        string[] miniScheduleStatus = world.ball.nowScheduleStatus[world.ball.indexOfSchedule];
        string beforePlayerStatus = null;

        // もし、ミニスケの先頭だった場合（でかつ、FirstPlayerを除く。）
        if (world.ball.indexOfScheduleOfSchedule == 0 && myName != "FirstPlayer")
        {
            // ひとつ前ミニスケの一番後ろのplayerのステータスを取得。
            string[] beforeMiniScheduleStatus = world.ball.nowScheduleStatus[world.ball.indexOfSchedule - 1];
            beforePlayerStatus = beforeMiniScheduleStatus[beforeMiniScheduleStatus.Length - 1];
        }
        // もし、FirstPlayerだった場合
        else if (myName == "FirstPlayer")
        {
            beforePlayerStatus = "Completed";  // 一つ前のプレイヤーはいないから強制
        }
        // もし、ミニスケの先頭でもなく、FirstPlayerでもなかった場合
        else
        {
            // 普通にひとつ前のplayerのステータスを取得。
            beforePlayerStatus = miniScheduleStatus[world.ball.indexOfScheduleOfSchedule - 1];
        }

        return beforePlayerStatus;
    }

    public bool UpdateNowScheduleStatus(World world, string playerStatus)
    {
        int axis0 = world.ball.indexOfSchedule;
        int axis1 = world.ball.indexOfScheduleOfSchedule;
        world.ball.nowScheduleStatus[axis0][axis1] = playerStatus;

        return true;
    }

    public string GoToNextIndexOfScheduleOfSchedule(World world)
    {
        int axis0 = world.ball.indexOfSchedule;
        int axis1 = world.ball.indexOfScheduleOfSchedule;

        if (axis1 + 1 >= world.ball.nowSchedule[axis0].Length)
        {
            world.ball.indexOfScheduleOfSchedule = 0;
            return "over!";
        }
        else
        {
            world.ball.indexOfScheduleOfSchedule++;
            return "True";
        }
    }

    public string GoToNextIndexOfSchedule(World world)
    {
        bool miniScheduleIsAllCompleted = true;
        int axis0 = world.ball.indexOfSchedule;
        string[] nowMiniScheduleStatus = world.ball.nowScheduleStatus[axis0];

        foreach (string status in nowMiniScheduleStatus)
        {
            if (status != "Completed")
            {
                miniScheduleIsAllCompleted = false;
                break;
            }
        }

        if (miniScheduleIsAllCompleted)
        {
            world.ball.indexOfSchedule++;
            if (world.ball.indexOfSchedule >= world.ball.nowSchedule.Length)
            {
                world.ball.indexOfSchedule = 0;
                world.ball.indexOfScheduleOfSchedule = 0;
                ResetScheduleStatusResult = world.ball.ResetScheduleStatus();
                return "range over!";
            }
        }

        if (!miniScheduleIsAllCompleted)
        {
            return "Not this mini schedule is all completed yet";
        }

        return null;
    }

    public bool GoToNextScheduleMode(World world)
    {
        ScheduleModeSettingsResult = world.ball.ScheduleModeSettings();

        return true;
    }





    public string DoingNowPlayersMainAndUpdateNowPlayersStatus(
        World world,
        string beforePlayerStatus, // ひとつ前のプレイヤーステータス（自信を実行するべきかの判断に使用する変数）
        string playerName // 
    )

    {
        string result = "0";

        if (beforePlayerStatus == "Completed")
        {
            if (world.ball.nowScheduleStatus[world.ball.indexOfSchedule][world.ball.indexOfScheduleOfSchedule] == "Not completed yet")
            {
                // Pass if already executed
            }
            else
            {

                // 【README】
                // 下記のように書き込み追加してください。
                // (!大文字小文字注意!)
                // ====================================
                // else if (playerName == "NewPlayerName")
                // {
                //     result = newPlayerName.ExecuteMain();
                // }
                // ====================================
                if (playerName == "FirstPlayer")
                {
                    result = firstPlayer.ExecuteMain();
                }
                else if (playerName == "DebugPlayer")
                {
                    result = debugPlayer.ExecuteMain();
                }
                else if (playerName == "FinalPlayer")
                {
                    result = finalPlayer.ExecuteMain();
                }
                else if (playerName == "InputAndPrintPlayerDir")
                {
                    result = inputAndPrintPlayerDir.ExecuteMain();
                }

                // 他のプレイヤーもここに追加

                else
                {
                    Debug.LogError($"「{playerName}」というプレイヤーは存在しません。");
                }
            }
        }

        if (result == "0")
        {
            // Pass
        }
        else if (result == "Completed")
        {
            UpdateNowScheduleStatus(world, "Completed");
        }
        else
        {
            UpdateNowScheduleStatus(world, "Not completed yet");
        }

        return result;
    }



    public bool DoingNextPlayersCatchBallMethodByPlayerName(string playerName, World world)
    // 引数:playerName
    // 処理:playerNameのCatchBallを実行
    // 戻値:なし

    // 【README】
    // 下記のように追加してください。
    // (!大文字小文字注意!)
    // ================================================================
    // else if (playerName == "newPlayerName")
    // // newPlayerNameのCatchBallを実行
    // {
    //     CatchBallResult = newPlayerName.CatchBall(world);
    // }
    // ================================================================


    {

        if (playerName == "FirstPlayer")
        // FirstPlayerのCatchBallを実行
        {
            CatchBallResult = firstPlayer.CatchBall(world);
        }

        else if (playerName == "DebugPlayer")
        // DebugPlayerのCatchBallを実行
        {
            CatchBallResult = debugPlayer.CatchBall(world);
        }

        else if (playerName == "FinalPlayer")
        // FinalPlayerのCatchBallを実行
        {
            CatchBallResult = finalPlayer.CatchBall(world);
        }

        else if (playerName == "InputAndPrintPlayerDir")
        // InputAndPrintPlayerDirのCatchBallを実行
        {
            CatchBallResult = inputAndPrintPlayerDir.CatchBall(world);
        }


        // 他のプレイヤーもここに追加

        return true;
    }




}











