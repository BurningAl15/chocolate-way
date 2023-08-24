using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    public void Load(){
        MainSceneManager._instance.LoadScene();
    }
}
