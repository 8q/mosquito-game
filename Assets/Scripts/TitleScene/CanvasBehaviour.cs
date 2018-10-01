using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

namespace MosquitoGame.TitleScene
{
    public class CanvasBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Button rankingButton;


        // Use this for initialization
        void Start()
        {
            rankingButton.onClick.AsObservable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Ranking");
                });
        }
    }
}


