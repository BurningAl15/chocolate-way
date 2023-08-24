using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Flags]
public enum TextAnimatorEffect{
    Wiggle,
    Shake,
    Wave,
    Slide,
    Bounce,
    Rotation,
    Swing,
    Increase,
    // Rainbow,
    // Fade,
    // Dangle,
    Pendulum
}

[Serializable]
public class TextUtils{
    [EnumFlagsAttribute] public TextAnimatorEffect textAnimatorEffect;

    public string GetEffects(string _){
        string temp=_;
        List<int> tempList = ReturnSelectedElements();

        for (int i = 0; i < tempList.Count; i++)
        {
            if (tempList[i] == (int)TextAnimatorEffect.Wiggle)
            {
                temp="<wiggle>" + temp +"</wiggle>";
            }

            if (tempList[i] == (int)TextAnimatorEffect.Shake)
            {
                temp="<shake>" + temp +"</shake>";
            }

            if (tempList[i] == (int)TextAnimatorEffect.Wave)
            {
                temp="<wave>" + temp +"</wave>";
            }

            if (tempList[i] == (int)TextAnimatorEffect.Slide)
            {
                temp="<slide>" + temp +"</slide>";
            }
            
            if (tempList[i] == (int)TextAnimatorEffect.Bounce)
            {
                temp="<bounce>" + temp +"</bounce>";
            }

            if (tempList[i] == (int)TextAnimatorEffect.Rotation)
            {
                temp="<rot>" + temp +"</rot>";
            }

            if (tempList[i] == (int)TextAnimatorEffect.Swing)
            {
                temp="<swing>" + temp +"</swing>";
            }

            if (tempList[i] == (int)TextAnimatorEffect.Increase)
            {
                temp="<incr>" + temp +"</incr>";
            }

            if (tempList[i] == (int)TextAnimatorEffect.Pendulum)
            {
                temp="<pend>" + temp +"</pend>";
            }
        }
        // Debug.Log("Temp: "+temp);
        return temp;
    }

    List<int> ReturnSelectedElements()
    {
        List<int> selectedElements = new List<int>();
        for (int i = 0; i < System.Enum.GetValues(typeof(TextAnimatorEffect)).Length; i++)
        {
            int layer = 1 << i;
            if (((int) textAnimatorEffect & layer) != 0)
                selectedElements.Add(i);
        }

        return selectedElements;
    }
}