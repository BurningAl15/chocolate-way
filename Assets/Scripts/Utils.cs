using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ContainerLimits
{
    [SerializeField] GameObject container;
    [SerializeField] RectTransform containerTransform;
    [SerializeField] RectTransform initialPoint, midPoint, endPoint;

    [SerializeField] AnimationCurve animationCurve;

    public void MoveToMid(float _)
    {
        containerTransform.position = Vector3.Lerp(initialPoint.position, midPoint.position, animationCurve.Evaluate(_));
    }

    public void MoveToEnd(float _)
    {
        containerTransform.position = Vector3.Lerp(midPoint.position, endPoint.position, animationCurve.Evaluate(_));
    }

    public void Init()
    {
        containerTransform.position = initialPoint.position;
    }
}

