using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public static EndMenu _instance;
    public bool endAnimation = false;

    void Awake()
    {
        _instance = this;
    }
}
