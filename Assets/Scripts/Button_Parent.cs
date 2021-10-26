using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Parent : MonoBehaviour
{
    protected Coroutine currentCoroutine = null;

    protected IEnumerator DelayedCall_Scene()
    {
        yield return StartCoroutine(TransitionManager._instance.FadeIn());
        DelayedAction();
        currentCoroutine = null;
    }

    protected virtual void DelayedAction() { }

    public virtual void ClickCoroutine()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(DelayedCall_Scene());
    }

    public virtual void ClickCoroutine(string _)
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(DelayedCall_Scene());
    }
}
