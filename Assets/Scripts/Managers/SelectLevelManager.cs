using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectLevelManager : MonoBehaviour
{
    public void Select_Level(int _index)
    {
        PlayerPrefs.SetInt("level_index",_index);
        SceneUtils.GameLevel();
    }
}
