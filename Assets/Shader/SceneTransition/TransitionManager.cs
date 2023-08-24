using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager _instance;

    [SerializeField] AnimationCurve animationCurve;

    bool animationDone = false;
    [SerializeField] Material material;

    Coroutine currentCoroutine = null;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    public void CallFade(bool _)
    {
        if (!_)
        {
            NoFadeEffect();
        }
        else
        {
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(FadeOut());
            }
        }
    }

    public void NoFadeEffect()
    {
        material.SetFloat("_CutOff", 0);
    }


    /// <summary>
    /// From 1 to 0 (Clears the visibility)
    /// </summary>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public IEnumerator FadeOut(float _duration = 1f)
    {
        animationDone = false;
        material.SetFloat("_Cutoff", 1);

        for (float i = _duration; i > 0; i -= Time.deltaTime)
        {
            material.SetFloat("_Cutoff", animationCurve.Evaluate(i / _duration));
            yield return null;
        }
        material.SetFloat("_Cutoff", 0);
        yield return null;

        animationDone = true;
        currentCoroutine = null;
    }

    /// <summary>
    /// From 0 to 1 in the material (block te visibility)
    /// </summary>
    /// <param name="_duration"></param>
    /// <returns></returns>
    public IEnumerator FadeIn(float _duration = 1f)
    {
        animationDone = false;
        material.SetFloat("_Cutoff", 0);

        for (float i = 0; i < _duration; i += Time.deltaTime)
        {
            material.SetFloat("_Cutoff", animationCurve.Evaluate(i / _duration));
            yield return null;
        }
        material.SetFloat("_Cutoff", 1);
        yield return null;

        animationDone = true;
        currentCoroutine = null;
    }

    public bool GetAnimationDone()
    {
        return animationDone;
    }
}