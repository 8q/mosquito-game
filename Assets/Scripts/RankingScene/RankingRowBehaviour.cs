using MosquitoGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MosquitoGame.RankingScene
{
    public class RankingRowBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int rank = 1;

        [SerializeField]
        private Text scoreText, detailText;

        // Use this for initialization
        void Start()
        {
            var scoreData = RankingPrefs.GetScoreData(rank);

            if(scoreData == null)
            {
                scoreData = new ScoreData { Score = 0, HitCount = 0, HitRate = 0 };
            }

            scoreText.text = string.Format("{0}", scoreData.Score);
            detailText.text = string.Format("たおした数：{0}体\nめいちゅう率：{1:0.0}％", scoreData.HitCount, scoreData.HitRate);
        }

    }

}

