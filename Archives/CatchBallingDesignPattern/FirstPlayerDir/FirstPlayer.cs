using UdonSharp;
using UnityEngine;

public class FirstPlayer : SuperPlayer
{
    public bool FirstPlayerReset()
    {
        myName = "FirstPlayer";

        return true;
    }

    public override string ReturnMyName()
    {
        return "FirstPlayer";
    }

    public override string ExecuteMain()
    {
        return "Completed";
    }
}
