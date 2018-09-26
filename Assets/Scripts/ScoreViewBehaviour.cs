using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ScoreViewBehaviour : MonoBehaviour
{
    [SerializeField]
    private Text hitCountText;

    [SerializeField]
    private Text hitRateText;

    // Use this for initialization
    void Start()
    {
        var scoreManager = ScoreManager.Instance;

        scoreManager.HitCount
            .Subscribe(cnt => hitCountText.text = string.Format("たおした数：{0}", cnt))
            .AddTo(gameObject);

        scoreManager.HitRate
            .Subscribe(rate => hitRateText.text = string.Format("めいちゅう率：{0:0.0}％", rate))
            .AddTo(gameObject);
    }
}
