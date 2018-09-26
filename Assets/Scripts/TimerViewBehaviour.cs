using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class TimerViewBehaviour : MonoBehaviour
{
    [SerializeField]
    private Text timerText;

    // Use this for initialization
    void Start()
    {
        var timerManager = TimerManager.Instance;

        timerManager.Time
            .Subscribe(t => timerText.text = string.Format("のこり時間：{0}", t))
            .AddTo(gameObject);
    }

}
