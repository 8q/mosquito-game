using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MosquitoGame.Utils;

namespace MosquitoGame.GameScene
{

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
            var eventManager = EventManager.Instance;

            // 初期化中
            stageManager.StageState
                .Where(s => s == StageState.Initializing)
                .Subscribe(s =>
                {
                    ClickCount.Value = 0;
                    HitCount.Value = 0;
                    HitRate.Value = 0f;
                })
                .AddTo(gameObject);

            // ランキングの更新
            stageManager.StageState
                .Where(s => s == StageState.Result)
                .Subscribe(_ =>
                {
                    RankingPrefs.InsertScoreData(new ScoreData { Score = GetScore(), HitCount = HitCount.Value, HitRate = HitRate.Value });
                })
                .AddTo(gameObject);

            // マウスがクリックされたらクリックカウントを増やす
            eventManager.MouseClick
                .Subscribe(p => ClickCount.Value++)
                .AddTo(gameObject);

            // 蚊に当たったらヒットカウントを増やす
            eventManager.MosquitoHit
                .Subscribe(_ => HitCount.Value++)
                .AddTo(gameObject);

            // クリックカウントとヒットカウントが更新されたら、ヒットレートを更新する
            Observable.Merge(ClickCount).Merge(HitCount)
                .Where(_ => ClickCount.Value > 0)
                .Subscribe(_ => HitRate.Value = 100.0f * HitCount.Value / ClickCount.Value)
                .AddTo(gameObject);
        }

        public float GetBaseScore()
        {
            return this.HitCount.Value * 10000;
        }

        public float GetMag()
        {
            return Mathf.Round(this.HitRate.Value * 10) / 1000.0f + 1.0f;
        }

        public float GetScore()
        {
            return Mathf.Floor(GetBaseScore() * GetMag());
        }

    }

}