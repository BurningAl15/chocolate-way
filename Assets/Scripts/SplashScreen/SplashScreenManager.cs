using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AldhaDev.SplashScreen
{
    using Managers;
    
    public class SplashScreenManager : Menu_Manager_Parent
    {
        [SerializeField] Animator animator;

        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            InitSplash();
        }

        private void InitSplash()
        {
            currentCoroutine ??= StartCoroutine(InitGame());
        }

        private IEnumerator InitGame()
        {
            yield return StartCoroutine(TransitionManager.Current.FadeOut(1f));
            animator.enabled = true;
            currentCoroutine = null;
        }
    }
}