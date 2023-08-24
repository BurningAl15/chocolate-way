using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LogoAnimation : MonoBehaviour
{
    [SerializeField] string titleContent;
    [SerializeField] string studioContent;

    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI studioText;


    Coroutine currentCoroutine = null;

    void Awake()
    {
        titleText.text = studioText.text = "";
    }

    public void CallCoroutine()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        string tempTitle = "";
        for (int i = 0; i < titleContent.Length; i++)
        {
            tempTitle += titleContent[i];
            titleText.text = tempTitle;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.5f);

        tempTitle = "";

        for (int i = 0; i < studioContent.Length; i++)
        {
            tempTitle += studioContent[i];
            studioText.text = tempTitle;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(TransitionManager._instance.FadeIn(.5f));
        SceneUtils.ToNextLevel();

        currentCoroutine = null;
    }

}
