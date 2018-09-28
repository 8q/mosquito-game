using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class InstructPanelBehaviour : MonoBehaviour
{

    [SerializeField]
    private Button playButton;

    // Use this for initialization
    void Start()
    {
        var stageManager = StageManager.Instance;

        playButton.onClick.AsObservable()
            .Subscribe(_ =>
            {
                stageManager.PlayStart.OnNext(Unit.Default);
            })
            .AddTo(gameObject);

    }

}
