using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasGroupExtend
{
    public static void CanvasGroupFade(this CanvasGroup canvasGroup, float _alpha)
    {
        canvasGroup.alpha = _alpha;
    }

    public static void CanvasGroupInteractable(this CanvasGroup canvasGroup, bool canInteract)
    {
        canvasGroup.interactable = canInteract;
        canvasGroup.blocksRaycasts = canInteract;
    }
}
