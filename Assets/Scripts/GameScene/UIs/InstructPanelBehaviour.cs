using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace MosquitoGame.GameScene
{
    public class InstructPanelBehaviour : MonoBehaviour
    {

        [SerializeField]
        private Button playButton;

        // Use this for initialization
        void Start()
        {
            var eventManager = EventManager.Instance;

            playButton.onClick.AsObservable()
                .Subscribe(_ =>
                {
                    eventManager.PlayStart.OnNext(Unit.Default);
                })
                .AddTo(gameObject);

        }

    }
}


