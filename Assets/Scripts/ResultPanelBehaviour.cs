using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultPanelBehaviour : MonoBehaviour
{

    [SerializeField]
    private Text hitCountText, hitCountRemarkText, hitRateText, hitRateRemarkText, scoreText, scoreRemarkText;

    [SerializeField]
    private Button restartButton, titleButton;

    // Use this for initialization
    void Start()
    {
        var stageManager = StageManager.Instance;
        var scoreManager = ScoreManager.Instance;

        stageManager.StageState
            .Where(s => s == StageState.Result)
            .Subscribe(s =>
            {
                var baseScore = scoreManager.GetBaseScore();
                var mag = scoreManager.GetMag();
                var score = scoreManager.GetScore();

                hitCountText.text = string.Format("{0}体", scoreManager.HitCount.Value);
                hitCountRemarkText.text = string.Format("（基本スコア：{0}）", baseScore);
                hitRateText.text = string.Format("{0:0.0}％", scoreManager.HitRate.Value);
                hitRateRemarkText.text = string.Format("（倍率：{0:0.000}）", mag);
                scoreText.text = string.Format("{0}", score);
                scoreRemarkText.text = string.Format("={0}x{1:0.000}", baseScore, mag);
            })
            .AddTo(gameObject);

        restartButton.onClick.AsObservable()
            .Subscribe(_ =>
            {
                gameObject.SetActive(false);
                stageManager.StageState.Value = StageState.Initializing;
            })
            .AddTo(gameObject);

        titleButton.onClick.AsObservable()
            .Subscribe(_ =>
            {
                SceneManager.LoadScene("Title");
            })
            .AddTo(gameObject);
    }

}
