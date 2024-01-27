using System.Collections;
using UnityEngine;
using AldhaDev.Managers;

public class Button_Parent : MonoBehaviour
{
    protected Coroutine currentCoroutine = null;

    protected IEnumerator DelayedCall_Scene()
    {
        yield return StartCoroutine(TransitionManager.Current.FadeIn());
        DelayedAction();
        currentCoroutine = null;
    }

    protected virtual void DelayedAction() { }

    public virtual void ClickCoroutine()
    {
        currentCoroutine ??= StartCoroutine(DelayedCall_Scene());
    }

    public virtual void ClickCoroutine(string _)
    {
        currentCoroutine ??= StartCoroutine(DelayedCall_Scene());
    }
}
