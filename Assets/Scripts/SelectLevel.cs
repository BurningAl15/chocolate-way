using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public bool isLevelSelected = false;

    public void Select_Level(int _index)
    {
        TilesetManager._instance.SetLevelIndex(_index);
        isLevelSelected = true;
    }
}
