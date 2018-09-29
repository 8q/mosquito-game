using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MosquitoGame.Utils;

namespace MosquitoGame.GameScene
{

    public class StageManager : SingletonMonoBehaviour<StageManager>
    {
        // ゲームの現在のステート
        private ReactiveProperty<StageState> _stageState = new ReactiveProperty<StageState>(GameScene.StageState.Initializing);
        public ReactiveProperty<StageState> StageState { get { return _stageState; } }

        void Start()
        {
            var eventManager = EventManager.Instance;

            // ステートの状態遷移
            // プレー開始ボタン
            eventManager.PlayStart
                .Where(_ => this.StageState.Value == GameScene.StageState.Initializing)
                .Subscribe(_ => this.StageState.Value = GameScene.StageState.Playing)
                .AddTo(gameObject);
            // 時間切れ
            eventManager.TimeUp
                .Where(_ => this.StageState.Value == GameScene.StageState.Playing)
                .Subscribe(_ => this.StageState.Value = GameScene.StageState.Result)
                .AddTo(gameObject);
            // もう一度遊ぶ
            eventManager.ReStart
                .Where(_ => this.StageState.Value == GameScene.StageState.Result)
                .Subscribe(_ => this.StageState.Value = GameScene.StageState.Initializing)
                .AddTo(gameObject);
        }

    }

}
