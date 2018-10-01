using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace MosquitoGame.Utils
{
    [RequireComponent(typeof(RawImage), typeof(VideoPlayer))]
    public class VideoPlayerOnUGUI : MonoBehaviour
    {
        RawImage image;
        VideoPlayer player;

        void Awake()
        {
            image = GetComponent<RawImage>();
            player = GetComponent<VideoPlayer>();
        }

        void Update()
        {
            if (player.isPrepared)
            {
                image.texture = player.texture;
            }
        }
    }
}
