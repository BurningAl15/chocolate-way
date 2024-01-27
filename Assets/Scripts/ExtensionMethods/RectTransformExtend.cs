using UnityEngine;

namespace AldhaDev.ExtensionMethods
{
    public static class RectTransformExtend
    {
        public static void TranslateToPosition(this RectTransform rectTransform, Vector3 target)
        {
            rectTransform.localPosition = target;
        }
    
        public static void TranslateLerp_Vector3(this RectTransform rectTransform, Vector3 initialPos, Vector3 endPos,
            float time, AnimationCurve animationCurve)
        {
            rectTransform.localPosition=Vector3.Lerp(initialPos, endPos, animationCurve.Evaluate(time));
        }
    
        public static void TranslateXY(this RectTransform rectTransform,Vector2 xy){
            rectTransform.localPosition=xy;
        }

        public static void TranslateY(this RectTransform rectTransform,float y){
            rectTransform.localPosition=new Vector2(0,y);
        }

        public static void TranslateX(this RectTransform rectTransform,float x){
            rectTransform.localPosition=new Vector2(x,0);
        }
    }
}
