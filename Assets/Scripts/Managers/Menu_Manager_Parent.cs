using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Manager_Parent : MonoBehaviour
{
    protected Coroutine currentCoroutine = null;

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Init(){}
}
