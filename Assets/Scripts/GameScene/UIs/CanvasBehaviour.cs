using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace MosquitoGame.GameScene
{
    public class CanvasBehaviour : MonoBehaviour
    {

        [SerializeField]
        private GameObject instructPanel, scorePanel, resultPanel;

        // Use this for initialization
        void Start()
        {
            var stageManager = StageManager.Instance;

            stageManager.StageState
                .Subscribe(s =>
                {
                    instructPanel.SetActive(s == StageState.Initializing);
                    scorePanel.SetActive(s == StageState.Playing);
                    resultPanel.SetActive(s == StageState.Result);
                })
                .AddTo(gameObject);

        }

    }
}


