using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CanvasBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject instructPanel, scorePanel, resultPanel;

    // Use this for initialization
    void Start()
    {
        var stageManager = StageManager.Instance;

        stageManager.StageState
            .Where(s => s == StageState.Initializing)
            .Subscribe(_ =>
            {
                instructPanel.SetActive(true);
            })
            .AddTo(gameObject);

        stageManager.StageState
            .Where(s => s == StageState.Playing)
            .Subscribe(_ =>
            {
                scorePanel.SetActive(true);
            })
            .AddTo(gameObject);

        stageManager.StageState
            .Where(s => s == StageState.Result)
            .Subscribe(_ =>
            {
                resultPanel.SetActive(true);
            })
            .AddTo(gameObject);

    }

}
