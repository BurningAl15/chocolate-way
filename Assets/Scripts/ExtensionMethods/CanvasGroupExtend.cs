using UnityEngine;

namespace AldhaDev.ExtensionMethods
{
    public static class CanvasGroupExtend
    {
        public static void CanvasGroupFade(this CanvasGroup canvasGroup,float alpha)
        {
            canvasGroup.alpha = alpha;
        }

        public static void CanvasGroupInteractable(this CanvasGroup canvasGroup, bool canInteract)
        {
            canvasGroup.interactable=canInteract;
            canvasGroup.blocksRaycasts = canInteract;
        }
    }
}
