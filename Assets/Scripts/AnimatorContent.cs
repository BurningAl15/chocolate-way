using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorState
{
    Normal,
    Highlighted,
    Pressed,
    Selected,
    Disabled
}

[Serializable]
public class AnimatorContent
{
    public AnimatorState animatorState;
    public string animatorName;
}

