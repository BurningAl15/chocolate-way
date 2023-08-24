using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class MessageAnimation : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Base Message Animation")]
    public static void AddBaseContainer()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Base Message Animation"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
}
