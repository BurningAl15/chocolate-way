using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace AldhaDev.VFX
{
    public class ScreenShake : MonoSingleton<ScreenShake>
    {
        [FormerlySerializedAs("shakeTimeRamaining")] [SerializeField] private float shakeTimeRemaining;
        [SerializeField] private float shakePower;
        [SerializeField] private float shakeFadeTime;
        [SerializeField] private float shakeRotation;
        [SerializeField] private float rotationMultiplier = 2f;
        private Coroutine _currentCoroutine;
        private Transform _tempTransform;

        protected override void Awake()
        {
            base.Awake();
            _tempTransform = transform;
        }

        public void StartShake(float length, float power)
        {
            shakeTimeRemaining = length;
            shakePower = power;

            shakeFadeTime = power / length;
            shakeRotation = power * rotationMultiplier;

            if (_currentCoroutine == null)
                _currentCoroutine = StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            while (shakeTimeRemaining > 0)
            {
                shakeTimeRemaining -= Time.deltaTime;
                float xAmount = Random.Range(-1f, 1f) * shakePower;
                float yAmount = Random.Range(-1f, 1f) * shakePower;

                _tempTransform.position += new Vector3(xAmount, yAmount, 0f);

                shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

                shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
                yield return null;
                _tempTransform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
                yield return null;
            }

            Vector3 tempPos = _tempTransform.position;
            Vector3 tempRot = _tempTransform.eulerAngles;
            Vector3 cameraEndPos = new Vector3(0, 0, -10);
            float i = 0;
            float endTime = .25f;
            while (i < endTime)
            {
                i += Time.deltaTime;
                _tempTransform.position = Vector3.Lerp(tempPos, cameraEndPos, i / endTime);
                _tempTransform.eulerAngles = Vector3.Lerp(tempRot, Vector3.zero, i / endTime);
                yield return null;
            }
            _tempTransform.position = Vector3.Lerp(tempPos, cameraEndPos, i / endTime);
            _tempTransform.eulerAngles = Vector3.Lerp(tempRot, Vector3.zero, i / endTime);
            yield return null;

            _currentCoroutine = null;
        }
    }
}
