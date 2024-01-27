using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AldhaDev.Managers
{
    public class TransitionManager : MonoSingleton<TransitionManager>
    {
        [SerializeField] AnimationCurve animationCurve;

        private bool _animationDone = false;
        [SerializeField] Material material;

        private Coroutine _currentCoroutine = null;
        private static readonly int CutOff = Shader.PropertyToID("_CutOff");
        private static readonly int Cutoff = Shader.PropertyToID("_Cutoff");

        public void CallFade(bool _)
        {
            if (!_)
            {
                NoFadeEffect();
            }
            else
            {
                _currentCoroutine ??= StartCoroutine(FadeOut());
            }
        }

        public void NoFadeEffect()
        {
            material.SetFloat(CutOff, 0);
        }

        /// <summary>
        /// From 1 to 0 (Clears the visibility)
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public IEnumerator FadeOut(float duration = 1f)
        {
            _animationDone = false;
            material.SetFloat(Cutoff, 1);

            for (float i = duration; i > 0; i -= Time.deltaTime)
            {
                material.SetFloat(Cutoff, animationCurve.Evaluate(i / duration));
                yield return null;
            }
            material.SetFloat(Cutoff, 0);
            yield return null;

            _animationDone = true;
            _currentCoroutine = null;
        }

        /// <summary>
        /// From 0 to 1 in the material (block te visibility)
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public IEnumerator FadeIn(float duration = 1f)
        {
            _animationDone = false;
            material.SetFloat(Cutoff, 0);

            for (float i = 0; i < duration; i += Time.deltaTime)
            {
                material.SetFloat(Cutoff, animationCurve.Evaluate(i / duration));
                yield return null;
            }
            material.SetFloat(Cutoff, 1);
            yield return null;

            _animationDone = true;
            _currentCoroutine = null;
        }

        public bool GetAnimationDone()
        {
            return _animationDone;
        }
    }
}
