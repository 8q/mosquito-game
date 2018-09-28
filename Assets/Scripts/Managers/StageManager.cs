using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    // CursorBehaviourで発行
    private Subject<Vector3> _mouseClick = new Subject<Vector3>();
    public Subject<Vector3> MouseClick { get { return _mouseClick; } }

    // CursorBehaviourで発行
    private Subject<Unit> _mosquitoHit = new Subject<Unit>();
    public Subject<Unit> MosquitoHit { get { return _mosquitoHit; } }

    // InstructPanelで発行
    private Subject<Unit> _playStart = new Subject<Unit>();
    public Subject<Unit> PlayStart { get { return _playStart; } }

    // TimerMangerで発行
    private Subject<Unit> _timeUp = new Subject<Unit>();
    public Subject<Unit> TimeUp { get { return _timeUp; } }

    // ResultPanelで発行
    private Subject<Unit> _reStart = new Subject<Unit>();
    public Subject<Unit> ReStart { get { return _reStart; } }

    // ゲームの現在のステート
    private ReactiveProperty<StageState> _stageState = new ReactiveProperty<StageState>(global::StageState.Initializing);
    public ReactiveProperty<StageState> StageState { get { return _stageState; } }

    void Start()
    {
        // ステートの状態遷移
        // プレー開始ボタン
        this.PlayStart
            .Where(_ => this.StageState.Value == global::StageState.Initializing)
            .Subscribe(_ => this.StageState.Value = global::StageState.Playing)
            .AddTo(gameObject);
        // 時間切れ
        this.TimeUp
            .Where(_ => this.StageState.Value == global::StageState.Playing)
            .Subscribe(_ => this.StageState.Value = global::StageState.Result)
            .AddTo(gameObject);
        // もう一度遊ぶ
        this.ReStart
            .Where(_ => this.StageState.Value == global::StageState.Result)
            .Subscribe(_ => this.StageState.Value = global::StageState.Initializing)
            .AddTo(gameObject);
    }

}
