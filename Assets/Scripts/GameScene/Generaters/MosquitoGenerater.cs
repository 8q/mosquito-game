using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MosquitoGame.Utils;

namespace MosquitoGame.GameScene
{

    public class MosquitoGenerater : MonoBehaviour
    {
        [SerializeField]
        private GameObject mosquito;

        [SerializeField]
        private FloatRange span = new FloatRange(0.0f, 2.0f);

        // Use this for initialization
        void Start()
        {
            // StageManagerのインスタンス取得
            var stageManager = StageManager.Instance;

            Observable.FromCoroutine<Unit>(observer => RandomIntervalCoroutine(observer))
                .Where(_ => stageManager.StageState.Value == StageState.Playing)
                .Subscribe(_ =>
                {
                    Instantiate(mosquito);
                })
                .AddTo(gameObject);
        }

        private IEnumerator RandomIntervalCoroutine(IObserver<Unit> observer)
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(span.min, span.max));
                observer.OnNext(Unit.Default);
            }
        }
    }

}