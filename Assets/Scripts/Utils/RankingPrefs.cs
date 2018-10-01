using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MosquitoGame.Utils
{
    public class RankingPrefs
    {
        private const string KEY_PREFIX = "RANK";

        public const int RANK_COUNT = 5;

        private static string GetKey(int rank)
        {
            return KEY_PREFIX + rank;
        }

        public static ScoreData GetScoreData(int rank)
        {
            return PlayerPrefsUtils.HasKey(GetKey(rank)) ? PlayerPrefsUtils.GetObject<ScoreData>(GetKey(rank)) : null;
        }

        public static void SetScoreData(int rank, ScoreData scoreData)
        {
            PlayerPrefsUtils.SetObject<ScoreData>(GetKey(rank), scoreData);
        }

        public static void InsertScoreData(ScoreData scoreData)
        {
            int rank = 1;
            while(rank <= RANK_COUNT)
            {
                var tmp = GetScoreData(rank);
                if(tmp == null)
                {
                    SetScoreData(rank, scoreData);
                    return;
                }
                if(tmp.Score < scoreData.Score) break;
                rank++;
            }
            if (rank <= RANK_COUNT)
            {
                for (int i = RANK_COUNT; i > rank; i--)
                {
                    SetScoreData(i, GetScoreData(i - 1));
                }
                SetScoreData(rank, scoreData);
            }
        }

    }

}

