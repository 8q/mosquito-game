using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{

    private IntReactiveProperty _clickCount = new IntReactiveProperty(0);

    public IntReactiveProperty ClickCount { get { return _clickCount; } }

    private IntReactiveProperty _hitCount = new IntReactiveProperty(0);

    public IntReactiveProperty HitCount { get { return _hitCount; } }

    private FloatReactiveProperty _hitRate = new FloatReactiveProperty(0.0f);

    public FloatReactiveProperty HitRate { get { return _hitRate; } }

    // Use this for initialization
    void Start()
    {
        // StageManagerのインスタンスを取得
        var stageManager = StageManager.Instance;

        stageManager.MouseClick
            .Select(p => Camera.main.WorldToViewportPoint(p))
            .Where(p => p.x >= 0.0f && p.x <= 1.0f && p.y >= 0.0f && p.y <= 1.0f)
            .Subscribe(p => ClickCount.Value++);

        stageManager.MosquitoHit
            .Subscribe(_ => HitCount.Value++);

        Observable.Merge(ClickCount).Merge(HitCount)
            .Where(_ => ClickCount.Value > 0)
            .Subscribe(_ => HitRate.Value = 100.0f * HitCount.Value / ClickCount.Value);
    }

}
