using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public int tileSelected = -1;

    public void SetTile(int tile)
    {
        tileSelected = tile;
    }
}
