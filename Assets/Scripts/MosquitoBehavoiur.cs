using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class MosquitoBehavoiur : MonoBehaviour
{

    [SerializeField]
    private float speed = 0.005f;

    void Start()
    {
        // ベジェ曲線用の点配列を用意
        var points = GetRandomBezierCurveControlPoints4();

        var spriteRenderer = GetComponent<SpriteRenderer>();

        // ベジェ曲線に沿って蚊を移動させる
        this.UpdateAsObservable()
            .Select(_ => speed) // フレームあたりのtの変化量
            .Scan(0.0f, (acc, cur) => acc + cur)
            .TakeWhile(t => t <= 1.0f) // tが1を超えた時点てonCompletedを流す
            .Subscribe(t =>
            {
                var targetPoint = Camera.main.ViewportToWorldPoint(GetBezierCurvePoint4(points[0], points[1], points[2], points[3], points[4], t));
                targetPoint.z = 0.0f;
                // 進行方向によってテクスチャを反転させる
                spriteRenderer.flipX = targetPoint.x > transform.position.x;
                transform.position = targetPoint;
            }, () =>
            {
                // onCompletedで蚊を消す
                Destroy(gameObject);
            })
            .AddTo(gameObject);


        var audioSource = GetComponent<AudioSource>();
        var center = new Vector2(0.5f, 0.5f);

        // 中心に近いほど「プーン」の音量が大きくなる
        this.UpdateAsObservable()
            .Select(_ =>
            {
                var p = Camera.main.WorldToViewportPoint(transform.position);
                return new Vector2(p.x, p.y);
            })
            .Select(p => Mathf.Clamp(1 - 2 * Vector2.Distance(center, p), 0f, 1f))
            .Subscribe(t =>
            {
                audioSource.volume = t;
            })
            .AddTo(gameObject);
    }


    private Vector2[] GetRandomBezierCurveControlPoints3()
    {
        var points = new Vector2[4];
        points[0] = new Vector2(-0.1f, Random.Range(-0.1f, 1.1f)); // 左
        points[1] = new Vector2(Random.Range(-0.1f, 1.1f), 1.1f); // 上
        points[2] = new Vector2(1.1f, Random.Range(-0.1f, 1.1f)); // 右
        points[3] = new Vector2(Random.Range(-0.1f, 1.1f), -0.1f); // 下
        // 配列をランダムソート
        points = points.OrderBy(i => System.Guid.NewGuid()).ToArray();

        return points;
    }

    private Vector2[] GetRandomBezierCurveControlPoints4()
    {
        var points = new Vector2[5];
        points[0] = new Vector2(-0.1f, Random.Range(-0.1f, 1.1f)); // 左
        points[1] = new Vector2(Random.Range(-0.1f, 1.1f), 1.1f); // 上
        points[2] = new Vector2(1.1f, Random.Range(-0.1f, 1.1f)); // 右
        points[3] = new Vector2(Random.Range(-0.1f, 1.1f), -0.1f); // 下
        // 5つ目の点は上下左右ランダム
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                points[4] = new Vector2(-0.1f, Random.Range(-0.1f, 1.1f)); // 左
                break;
            case 1:
                points[4] = new Vector2(Random.Range(-0.1f, 1.1f), 1.1f); // 上
                break;
            case 2:
                points[4] = new Vector2(1.1f, Random.Range(-0.1f, 1.1f)); // 右
                break;
            case 3:
                points[4] = new Vector2(Random.Range(-0.1f, 1.1f), -0.1f); // 下
                break;
        }
        // 配列をランダムソート
        points = points.OrderBy(i => System.Guid.NewGuid()).ToArray();

        return points;
    }


    private Vector2 GetBezierCurvePoint3(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        var oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * oneMinusT * p0 +
               3f * oneMinusT * oneMinusT * t * p1 +
               3f * oneMinusT * t * t * p2 +
               t * t * t * p3;
    }

    private Vector2 GetBezierCurvePoint4(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
    {
        var oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * oneMinusT * oneMinusT * p0 +
               4f * oneMinusT * oneMinusT * oneMinusT * t * p1 +
               6f * oneMinusT * oneMinusT * t * t * p2 +
               4f * oneMinusT * t * t * t * p3 +
               t * t * t * t * p4;
    }
}
