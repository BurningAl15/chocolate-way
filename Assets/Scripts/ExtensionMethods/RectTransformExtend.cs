using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtend
{
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
