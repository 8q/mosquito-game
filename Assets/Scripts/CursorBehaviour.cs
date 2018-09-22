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

        // 左クリックしたときにする動作
        var mouseDown = this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0));

        // 「パン」を生成する。
        mouseDown
            .Subscribe(_ =>
            {
                Instantiate(pan, transform.position, pan.transform.rotation);
            })
            .AddTo(gameObject);

        // クリックしたときにカーソルの下に蚊がいたら蚊を消す
        mouseDown
            .Select(_ => Physics2D.OverlapPoint(transform.position))
            .Where(collider => collider != null && collider.gameObject.tag == "Mosquito")
            .Subscribe(collider =>
            {
                Destroy(collider.gameObject);
            })
            .AddTo(gameObject);
    }
}
