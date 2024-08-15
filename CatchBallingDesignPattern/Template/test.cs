
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


public class InputPlayer : SuperPlayer
{

    // 初期化
    public string playerNane = "InputPlayer";


    public bool InputPlayerReset()
    {
        myName = playerNane;

        return true;
    }


    public override string ReturnMyName()
    {
        return playerNane;
    }


    public override string ExecuteMain()
    {
        Debug.Log($"\n==== {playerNane} ExecuteMain ==============================");

        Debug.Log($"\n==== {playerNane} ExecuteMain (END) ==============================\n");

        return "Completed";
    }

}
