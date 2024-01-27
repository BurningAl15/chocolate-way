using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AldhaDev.Managers;

public class SelectLevel : MonoBehaviour
{
    public bool isLevelSelected = false;

    public void Select_Level(int index)
    {
        TilesetManager.Current.SetLevelIndex(index);
        isLevelSelected = true;
    }
}
