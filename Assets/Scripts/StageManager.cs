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

    // InstructPanelでInitializing->Playing
    // TimerでPlaying->Result
    // ResultPanelでResult->Initializing
    private ReactiveProperty<StageState> _stageState = new ReactiveProperty<StageState>(global::StageState.Initializing);
    public ReactiveProperty<StageState> StageState { get { return _stageState; } }

}
