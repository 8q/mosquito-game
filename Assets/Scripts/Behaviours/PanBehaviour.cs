using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PanBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // １秒で殺す
        Observable.Timer(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            })
            .AddTo(gameObject);
    }

}
