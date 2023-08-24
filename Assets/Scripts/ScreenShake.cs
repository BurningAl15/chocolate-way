using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake _instance;
    [SerializeField] private float shakeTimeRamaining, shakePower;
    [SerializeField] private float shakeFadeTime;
    [SerializeField] private float shakeRotation;
    [SerializeField] private float rotationMultiplier = 2f;
    Coroutine currentCoroutine = null;
    Transform tempTransform;

    private void Awake()
    {
        _instance = this;
        tempTransform = transform;
    }

    void Start()
    {

    }

    void LateUpdate()
    {
        // if (shakeTimeRamaining > 0)
        // {
        //     shakeTimeRamaining -= Time.deltaTime;
        //     float xAmount = Random.Range(-1f, 1f) * shakePower;
        //     float yAmount = Random.Range(-1f, 1f) * shakePower;

        //     transform.position += new Vector3(xAmount, yAmount, 0f);

        //     shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime*Time.deltaTime);

        //     shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        // }

        // transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRamaining = length;
        shakePower = power;

        shakeFadeTime = power / length;
        shakeRotation = power * rotationMultiplier;

        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        while (shakeTimeRamaining > 0)
        {
            shakeTimeRamaining -= Time.deltaTime;
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            tempTransform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
            yield return null;
            tempTransform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
            yield return null;
        }

        Vector3 tempPos = tempTransform.position;
        Vector3 tempRot = tempTransform.eulerAngles;
        Vector3 cameraEndPos = new Vector3(0, 0, -10);
        float i = 0;
        float endTime = .25f;
        while (i < endTime)
        {
            i += Time.deltaTime;
            tempTransform.position = Vector3.Lerp(tempPos, cameraEndPos, i / endTime);
            tempTransform.eulerAngles = Vector3.Lerp(tempRot, Vector3.zero, i / endTime);
            yield return null;
        }
        tempTransform.position = Vector3.Lerp(tempPos, cameraEndPos, i / endTime);
        tempTransform.eulerAngles = Vector3.Lerp(tempRot, Vector3.zero, i / endTime);
        yield return null;

        currentCoroutine = null;
    }
}
