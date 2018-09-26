using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class TimerManager : SingletonMonoBehaviour<TimerManager>
{
    [SerializeField]
    private int limitTime = 10;

    private IntReactiveProperty _time = new IntReactiveProperty(0);
    public IntReactiveProperty Time { get { return _time; } }

    // Use this for initialization
    void Start()
    {
        var stageManager = StageManager.Instance;

        Time.Value = limitTime;

        // 初期化中
        stageManager.StageState
            .Where(s => s == StageState.Initializing)
            .Subscribe(s =>
            {
                Time.Value = limitTime;
            })
            .AddTo(gameObject);

        // インゲーム
        stageManager.StageState
            .Where(s => s == StageState.Playing)
            .Subscribe(s =>
            {
                Observable.Interval(TimeSpan.FromSeconds(1))
                    .Select(x => limitTime - (int)x - 1)
                    .TakeWhile(x => x >= 0)
                    .Subscribe(x => Time.Value = x)
                    .AddTo(gameObject);
            })
            .AddTo(gameObject);

        // 時間0でリザルトにステートを変更
        Time.Where(t => t <= 0)
            .Where(_ => stageManager.StageState.Value == StageState.Playing)
            .Subscribe(_ =>
            {
                stageManager.StageState.Value = StageState.Result;
            })
            .AddTo(gameObject);
    }

}
