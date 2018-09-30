using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MosquitoGame.Utils
{
    public class BezierCurve
    {
        public static Vector2 GetPoint3(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            var oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * oneMinusT * p0 +
                   3f * oneMinusT * oneMinusT * t * p1 +
                   3f * oneMinusT * t * t * p2 +
                   t * t * t * p3;
        }

        public static Vector2 GetPoint4(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
        {
            var oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * oneMinusT * oneMinusT * p0 +
                   4f * oneMinusT * oneMinusT * oneMinusT * t * p1 +
                   6f * oneMinusT * oneMinusT * t * t * p2 +
                   4f * oneMinusT * t * t * t * p3 +
                   t * t * t * t * p4;
        }
    }
}



