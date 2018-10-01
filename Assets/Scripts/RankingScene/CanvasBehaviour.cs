using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

namespace MosquitoGame.RankingScene
{
    public class CanvasBehaviour : MonoBehaviour
    {

        [SerializeField]
        private Button titleButton;

        // Use this for initialization
        void Start()
        {
            titleButton.onClick.AsObservable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Title");
                });
        }
    }
}


