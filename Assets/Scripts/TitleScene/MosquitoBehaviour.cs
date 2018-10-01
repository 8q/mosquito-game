using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using MosquitoGame.Utils;

namespace MosquitoGame.TitleScene
{
    public class MosquitoBehaviour : MonoBehaviour
    {

        [SerializeField]
        private float speed = 0.2f;

        // Use this for initialization
        void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            Observable.FromCoroutine<Vector2>(observer => MovePointCoroutine(observer))
                .Subscribe(p =>
                {
                    Vector2 targetPoint = Camera.main.ViewportToWorldPoint(p);
                    spriteRenderer.flipX = targetPoint.x > transform.position.x;
                    transform.position = targetPoint;
                })
                .AddTo(gameObject);
        }

        IEnumerator MovePointCoroutine(IObserver<Vector2> observer)
        {
            var t = 0.0f;

            // 初期位置
            var points = GetInitControlPoints();

            while(true)
            {
                observer.OnNext(BezierCurve.GetPoint3(points[0], points[1], points[2], points[3], t));
                
                yield return null;

                if ((t += Time.deltaTime * speed) >= 1.0f)
                {
                    points = GetNextControlPoints(points);
                    t = 0.0f;
                }
            }
        }

        private Vector2[] GetInitControlPoints()
        {
            var points = new Vector2[4];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Vector2(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
            }
            return points;
        }

        public Vector2[] GetNextControlPoints(Vector2[] prevPoints)
        {
            var points = new Vector2[4];
            points[0] = prevPoints[3];
            points[1] = (Vector2)(Quaternion.Euler(0f, 0f, 180f) * (prevPoints[2] - prevPoints[3])) + prevPoints[3];
            points[2] = new Vector2(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
            points[3] = new Vector2(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
            return points;
        }
    }
}


