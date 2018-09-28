using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PanGenerater : MonoBehaviour
{
    [SerializeField]
    private GameObject pan;

    // Use this for initialization
    void Start()
    {
        // StageManagerのインスタンス取得
        var stageManager = StageManager.Instance;

        // 「パン」を生成する。
        stageManager.MouseClick
            .Subscribe(pos =>
            {
                Instantiate(pan, pos, pan.transform.rotation);
            })
            .AddTo(gameObject);
    }

}
