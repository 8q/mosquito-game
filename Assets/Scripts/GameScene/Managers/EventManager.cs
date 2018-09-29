using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MosquitoGame.Utils;

namespace MosquitoGame.GameScene
{

    public class EventManager : SingletonMonoBehaviour<EventManager>
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

    }

}