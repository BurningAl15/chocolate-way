using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class BaseContainer : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Base Container")]
    public static void AddBaseContainer()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Base Container"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
}
