using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTransitionManager : MonoBehaviour
{
    Coroutine currentCoroutine = null;

    void Start()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return StartCoroutine(TransitionManager._instance.FadeOut());
        yield return null;
        currentCoroutine = null;
    }
}
