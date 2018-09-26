using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class StageManager : SingletonMonoBehaviour<StageManager>
{

    private Subject<Vector3> _mouseClick = new Subject<Vector3>();
    public Subject<Vector3> MouseClick { get { return _mouseClick; } }

    private Subject<Unit> _mosquitoHit = new Subject<Unit>();
    public Subject<Unit> MosquitoHit { get { return _mosquitoHit; } }

    private ReactiveProperty<StageState> _stageState = new ReactiveProperty<StageState>(global::StageState.Initializing);
    public ReactiveProperty<StageState> StageState { get { return _stageState; } }

}
