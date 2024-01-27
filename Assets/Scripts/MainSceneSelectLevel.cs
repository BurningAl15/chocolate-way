using System.Collections;
using UnityEngine;
using AldhaDev.Managers;

public class MainSceneSelectLevel : MonoBehaviour
{
    private Coroutine _coroutine;
    
    public void ToNextScene(){
        SceneUtils.ToLevelSelection();
    }

    public void ToNextScene_Coroutine()
    {
        _coroutine ??= StartCoroutine(CallTransition());
    }

    private IEnumerator CallTransition()
    {
        yield return StartCoroutine(TransitionManager.Current.FadeIn());
        yield return null;
        SceneUtils.ToLevelSelection();
    }
}
