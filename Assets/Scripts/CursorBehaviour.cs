using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CursorBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject pan;

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

        // 左クリックしたときにMouseClickイベントを流す
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                stageManager.MouseClick.OnNext(transform.position);
            });

        // 「パン」を生成する。
        stageManager.MouseClick
            .Subscribe(pos =>
            {
                Instantiate(pan, pos, pan.transform.rotation);
            })
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
