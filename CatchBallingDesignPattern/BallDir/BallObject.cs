using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BallObject : UdonSharpBehaviour
{
    public string[][] nowSchedule;
    public string[][] nowScheduleStatus;
    public string scheduleMode;
    public int indexOfSchedule;
    public int indexOfScheduleOfSchedule;
    public string inputText; // 入力される文字列の箱(inputPlayerで使用)
    
    public RinaNumpy rinaNumpy;

    // デバッグ用
    public string nowCatchBallingPlayer;
    public string nextPlayerName;
    public TypeConverterToolkit typeConverterToolkit;
    public bool ResetScheduleStatusResult;
    private bool ScheduleDictResult;

    public bool BallReset()
    {
        this.scheduleMode = "sample_mode"; 
        ScheduleDictResult = this.ScheduleDict(scheduleMode); 
        ResetScheduleStatusResult = this.ResetScheduleStatus(); 

        return true;

    }

    public bool ScheduleModeSettings()
    {
        // 【README】
        // スケジュールの切り替え条件です。
        //
        // このメソッドはスケジュールが一つ終わった時点で呼び出されますので。
        // その時点での好ましいコーディングをしてください。
        //

        if (scheduleMode == "sample_mode")
        {
            scheduleMode = "sample2_mode";
        }
        else if (scheduleMode == "sample2_mode")
        {
            scheduleMode = "sample3_mode";
        }
        else if (scheduleMode == "sample3_mode")
        {
            scheduleMode = "sample_mode";
        }

        ScheduleDictResult = ScheduleDict(scheduleMode);

        return true;
    }

    public bool ResetScheduleStatus()
    {
        string[][] keep = rinaNumpy.Copy_Array2d(nowSchedule);
        this.nowScheduleStatus = rinaNumpy.InitializeJagged_StrArray(nowSchedule);
        nowSchedule = keep;

        return true;
    }

    public bool ScheduleDict(string modeName)
    {
        // 【README】
        // スケジュールは複数作成することができます。
        // どのPlayerの次に何のPlayerを実行するかという概念の元、実装してください。

        string[][] array2d = null;

        if (modeName == "sample_mode")
        {
            array2d = new string[][]
            {
                new string[] { "FirstPlayer" },
                new string[] { "InputAndPrintPlayerDir" },
                new string[] { "FinalPlayer" }
            };
        }
        else if (modeName == "sample2_mode")
        {
            array2d = new string[][]
            {
                new string[] { "FirstPlayer", "DebugPlayer" },
                new string[] { "DebugPlayer", "DebugPlayer", "FinalPlayer" }
            };
        }
        else if (modeName == "sample3_mode")
        {
            array2d = new string[][]
            {
                new string[] { "FirstPlayer" },
                new string[] { "DebugPlayer" },
                new string[] { "DebugPlayer", "DebugPlayer", "DebugPlayer", "FinalPlayer" }
            };
        }
        else
        {
            Debug.LogError("モードの選択が出来ませんでした。ScheduleModeSettingsメソッドでモード名を確認してください。");
            return false;
        }

        this.nowSchedule = array2d;

        return true;
    }
}
