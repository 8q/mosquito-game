using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CursorBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // StageManagerのインスタンス取得
        var stageManager = StageManager.Instance;

        // デフォルトのカーソルを消す
        Cursor.visible = false;

        // 疑似カーソルをマウスの位置に追従させる
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = 0f;
                transform.position = targetPosition;
            })
            .AddTo(gameObject);

        // 画面内を左クリックしたときにMouseClickイベントを流す
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Select(_ => transform.position)
            .Where(p =>
            {
                var t = Camera.main.WorldToViewportPoint(p);
                return t.x >= 0.0f && t.x <= 1.0f && t.y >= 0.0f && t.y <= 1.0f;
            })
            .Subscribe(p => stageManager.MouseClick.OnNext(p))
            .AddTo(gameObject);

        // クリックしたときにカーソルの下に蚊がいたら蚊を消す
        // 蚊を叩いたときにMosquitoHitイベントを流す
        stageManager.MouseClick
            .Select(pos => Physics2D.OverlapPoint(pos))
            .Where(collider => collider != null && collider.gameObject.tag == "Mosquito")
            .Subscribe(collider =>
            {
                stageManager.MosquitoHit.OnNext(Unit.Default);
                Destroy(collider.gameObject);
            })
            .AddTo(gameObject);
    }
}
