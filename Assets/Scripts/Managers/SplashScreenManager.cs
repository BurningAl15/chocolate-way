using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : Menu_Manager_Parent
{
    [SerializeField] Animator animator;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        InitSplash();
    }

    void InitSplash()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(InitGame());
    }

    IEnumerator InitGame()
    {
        yield return StartCoroutine(TransitionManager._instance.FadeOut(1f));
        animator.enabled = true;
        currentCoroutine = null;
    }
}
