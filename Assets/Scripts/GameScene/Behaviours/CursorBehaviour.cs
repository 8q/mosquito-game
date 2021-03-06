﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


namespace MosquitoGame.GameScene
{
    public class CursorBehaviour : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            // StageManagerのインスタンス取得
            var stageManager = StageManager.Instance;
            var eventManager = EventManager.Instance;

            // 疑似カーソルをマウスの位置に追従させる
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    targetPosition.z = 0f;
                    transform.position = targetPosition;
                })
                .AddTo(gameObject);

            // プレー中に画面内を左クリックしたときにMouseClickイベントを流す
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ => stageManager.StageState.Value == StageState.Playing)
                .Select(_ => transform.position)
                .Where(p =>
                {
                    var t = Camera.main.WorldToViewportPoint(p);
                    return t.x >= 0.0f && t.x <= 1.0f && t.y >= 0.0f && t.y <= 1.0f;
                })
                .Subscribe(p => eventManager.MouseClick.OnNext(p))
                .AddTo(gameObject);

            // クリックイベントが流れてきたときにカーソルの下に蚊がいたら蚊を消す
            // 蚊を叩いたときにMosquitoHitイベントを流す
            eventManager.MouseClick
                .Select(p => Physics2D.OverlapPoint(p))
                .Where(collider => collider != null && collider.gameObject.tag == "Mosquito")
                .Subscribe(collider =>
                {
                    eventManager.MosquitoHit.OnNext(Unit.Default);
                    Destroy(collider.gameObject);
                })
                .AddTo(gameObject);

            // インゲームのときだけカーソルを見えなくする
            stageManager.StageState
                .Subscribe(s => Cursor.visible = s != StageState.Playing)
                .AddTo(gameObject);
        }
    }

}
