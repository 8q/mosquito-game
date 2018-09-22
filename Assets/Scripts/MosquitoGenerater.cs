using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class MosquitoGenerater : MonoBehaviour
{
    [SerializeField]
    private GameObject mosquito;

    // Use this for initialization
    void Start()
    {
        Observable.Interval(TimeSpan.FromMilliseconds(1000))
            .Subscribe(_ =>
            {
                Instantiate(mosquito);
            });
    }
}
