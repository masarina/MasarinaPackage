using UdonSharp;
using UnityEngine;








public class FinalPlayer : SuperPlayer
{
    public bool FinalPlayerReset()
    {
        myName = "FinalPlayer";

        return true;
    }

    public override string ReturnMyName()
    {
        return "FinalPlayer";
    }

    public override string ExecuteMain()
    {
        return "Completed";
    }
}








