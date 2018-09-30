using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using MosquitoGame.Utils;

namespace MosquitoGame.GameScene
{
    public class MosquitoBehavoiur : MonoBehaviour
    {

        [SerializeField]
        private float speed = 0.2f;

        void Start()
        {
            // ベジェ曲線用の点配列を用意
            var points = GetRandomBezierCurveControlPoints();

            var spriteRenderer = GetComponent<SpriteRenderer>();

            // ベジェ曲線に沿って蚊を移動させる
            this.UpdateAsObservable()
                .Select(_ => speed * Time.deltaTime) // フレームあたりのtの変化量
                .Scan(0.0f, (acc, cur) => acc + cur)
                .TakeWhile(t => t <= 1.0f) // tが1を超えた時点てonCompletedを流す
                .Subscribe(t =>
                {
                    Vector2 targetPoint = Camera.main.ViewportToWorldPoint(BezierCurve.GetPoint4(points[0], points[1], points[2], points[3], points[4], t));
                    // 進行方向によってテクスチャを反転させる
                    spriteRenderer.flipX = targetPoint.x > transform.position.x;
                    transform.position = targetPoint;
                }, () =>
                {
                    Destroy(gameObject); // onCompletedで蚊を消す
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


        // ベジェ曲線用のランダムな制御点の用意
        private Vector2[] GetRandomBezierCurveControlPoints()
        {
            var points = new Vector2[5];
            points[0] = new Vector2(-0.1f, Random.Range(-0.1f, 1.1f)); // 左
            points[1] = new Vector2(Random.Range(-0.1f, 1.1f), 1.1f); // 上
            points[2] = new Vector2(1.1f, Random.Range(-0.1f, 1.1f)); // 右
            points[3] = new Vector2(Random.Range(-0.1f, 1.1f), -0.1f); // 下

            // 5つ目の点は上下左右ランダム
            switch (Random.Range(0, 4))
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

    }

}

