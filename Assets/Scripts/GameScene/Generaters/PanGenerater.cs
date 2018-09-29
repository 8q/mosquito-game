using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace MosquitoGame.GameScene
{

    public class PanGenerater : MonoBehaviour
    {
        [SerializeField]
        private GameObject pan;

        // Use this for initialization
        void Start()
        {
            var eventManager = EventManager.Instance;

            // 「パン」を生成する。
            eventManager.MouseClick
                .Subscribe(pos =>
                {
                    Instantiate(pan, pos, pan.transform.rotation);
                })
                .AddTo(gameObject);
        }

    }

}