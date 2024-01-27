using System.Collections;
using UnityEngine;
using AldhaDev.Managers;

public class InitialTransitionManager : MonoBehaviour
{
    private Coroutine _currentCoroutine = null;

    private void Start()
    {
        _currentCoroutine ??= StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return StartCoroutine(TransitionManager.Current.FadeOut());
        yield return null;
        _currentCoroutine = null;
    }
}
