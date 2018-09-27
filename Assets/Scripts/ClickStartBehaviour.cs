using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class ClickStartBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                var color = spriteRenderer.color;
                color.a = Mathf.PingPong(Time.time, 1.0f);
                spriteRenderer.color = color;
            })
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Select(_ => Camera.main.ScreenToViewportPoint(Input.mousePosition))
            .Where(p => p.x >= 0.0f && p.x <= 1.0f && p.y >= 0.0f && p.y <= 1.0f)
            .Take(1)
            .Subscribe(p =>
            {
                SceneManager.LoadScene("Game");
            })
            .AddTo(gameObject);
    }

}
