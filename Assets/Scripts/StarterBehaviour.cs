using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class StarterBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var stageManager = StageManager.Instance;

        // とりあえずInitializingから即Playingに移す
        stageManager.StageState
            .Where(s => s == StageState.Initializing)
            .Subscribe(s =>
            {
                stageManager.StageState.Value = StageState.Playing;
            })
            .AddTo(gameObject);
    }

}
