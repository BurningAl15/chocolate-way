using System.Collections;
using UnityEngine;
using AldhaDev.Managers;

public class SelectLevelManager : MonoBehaviour
{
    private Coroutine _coroutine;

    public void Select_Level(int index)
    {
        PlayerPrefs.SetInt("level_index",index);
        SceneUtils.GameLevel();
    }
    
    public void Select_Level_Coroutine(int index)
    {
        PlayerPrefs.SetInt("level_index",index);
        _coroutine ??= StartCoroutine(CallTransition());
    }

    private IEnumerator CallTransition()
    {
        yield return StartCoroutine(TransitionManager.Current.FadeIn());
        yield return null;
        SceneUtils.GameLevel();
    }
}
