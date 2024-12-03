using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MainScript : UdonSharpBehaviour
{
    // 初期化
    public World world;
    public TypeConverterToolkit typeConverterToolkit;
    private bool result_flag;
    private bool WorldResetResut;
    private bool test = true;

    void Start()
    {
        WorldResetResut = world.WorldReset();
    }


    void Update()
    {
        // 初期化
        string stringNowSchedule = "";
        string stringNowSatus = "";


        if (test == true)
        {

            // firstPlayerのCatchBallメソッドを呼び出し
            result_flag = world.firstPlayer.CatchBall(world);

            if (result_flag == true)
            {
                stringNowSchedule = typeConverterToolkit.Format2DArray(world.ball.nowSchedule);
                stringNowSatus = typeConverterToolkit.Format2DArray(world.ball.nowScheduleStatus);
                // Debug.Log("\n=========================================\n一つスケジュールが正常に完了しました\n" + stringNowSchedule + "\n" + stringNowSatus);
                // Debug.Log("\n=========================================");
            }


            else
            {
            }

        }

    }




}





















