using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : Button_Parent
{
    protected override void DelayedAction()
    {
        base.DelayedAction();
        SceneUtils.ResetLevel();
    }
}
