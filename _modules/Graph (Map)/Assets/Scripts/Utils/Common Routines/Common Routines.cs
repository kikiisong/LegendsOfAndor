using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace Routines
{
    public class CommonRoutines
    {
        public static IEnumerator Chain(params IEnumerator[] enumerators)
        {
            foreach (IEnumerator enumarator in enumerators)
            {
                yield return enumarator;
            }
        }

        public static IEnumerator MoveTo(Transform transform, Vector3 target, float timeTaken, Action action = null)
        {
            float t = 0f;
            Vector3 from = transform.position;
            while (t < timeTaken)
            {
                t += Time.deltaTime;
                float ratio = Mathf.SmoothStep(0f, 1f, t / timeTaken);
                transform.position = Vector3.Lerp(from, target, ratio);
                yield return null;
            }
            action?.Invoke();
        }
    }
}