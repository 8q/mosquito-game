using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ScorePanelBehaviour : MonoBehaviour
{
    [SerializeField]
    private Text hitCountText;

    [SerializeField]
    private Text hitRateText;

    [SerializeField]
    private Text timerText;

    // Use this for initialization
    void Start()
    {
        var stageManager = StageManager.Instance;
        var scoreManager = ScoreManager.Instance;
        var timerManager = TimerManager.Instance;

        scoreManager.HitCount
            .Subscribe(cnt => hitCountText.text = string.Format("たおした数：{0}体", cnt))
            .AddTo(gameObject);

        scoreManager.HitRate
            .Subscribe(rate => hitRateText.text = string.Format("めいちゅう率：{0:0.0}％", rate))
            .AddTo(gameObject);

        timerManager.Time
            .Subscribe(t => timerText.text = string.Format("のこり時間：{0}", t))
            .AddTo(gameObject);

        stageManager.StageState
            .Where(s => s != StageState.Playing)
            .Subscribe(s => gameObject.SetActive(false))
            .AddTo(gameObject);
    }
}
