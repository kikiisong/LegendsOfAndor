using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Collections.Generic;

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

    public class CoroutineQueue
    {
        MonoBehaviour owner;
        Coroutine internalCoroutine;

        Queue<IEnumerator> enumerators = new Queue<IEnumerator>();
        
        public CoroutineQueue(MonoBehaviour monoBehaviour)
        {
            owner = monoBehaviour;
        }

        public void StartLoop()
        {
            internalCoroutine = owner.StartCoroutine(Process());
        }

        public void StopLoop()
        {
            owner.StopCoroutine(internalCoroutine);
            internalCoroutine = null;
        }

        public void Enqueue(IEnumerator enumerator)
        {
            enumerators.Enqueue(enumerator);
        }

        private IEnumerator Process()
        {
            while (true)
            {
                if(enumerators.Count > 0)
                {
                    yield return owner.StartCoroutine(enumerators.Dequeue());
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}