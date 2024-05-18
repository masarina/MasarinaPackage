// このオブジェクトをアタッチすると、そのオブジェクトの中に居るユーザーの音の距離感がなくなります。つまり、このオブジェクト内に居るユーザーの音量が一定の音量としてほかのユーザーに聞こえるようになります。


// 必要なライブラリをインポート
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class VoiceDistanceControl : UdonSharpBehaviour
{
    // ローカルプレイヤーの情報を保持する変数
    public VRCPlayerApi localPlayer;

    // 声が減衰しない最大距離を設定する変数
    public float maxDistance = 25.0f;

    // 初期化処理
    void Start()
    {
        // ネットワーキングのローカルプレイヤーを取得
        localPlayer = Networking.LocalPlayer;
    }

    // フレームごとに呼び出される処理
    void Update()
    {
        // ローカルプレイヤーがnullの場合は何もしない
        if (localPlayer == null) return;

        // 現在の全プレイヤーの数を取得
        VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
        VRCPlayerApi.GetPlayers(players);

        // 全プレイヤーをループ
        foreach (VRCPlayerApi player in players)
        {
            // プレイヤーがnull、またはローカルプレイヤーの場合はスキップ
            if (player == null || player == localPlayer) continue;

            // ローカルプレイヤーと他のプレイヤーの距離を計算
            float distance = Vector3.Distance(localPlayer.GetPosition(), player.GetPosition());

            // 距離がmaxDistance以内の場合、声の減衰を設定
            if (distance < maxDistance)
            {
               
                // 声が聞こえる最大距離を設定（この距離以内なら声が聞こえる）
                player.SetVoiceDistanceFar(maxDistance);
                
                // 声が減衰し始める距離を設定（0に設定しているので、全く減衰しない）
                player.SetVoiceDistanceNear(0);
            }
        }
    }
}
