using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

[RequireComponent(typeof(TextAnimator))]
public class TextMeshAnimator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TextUtils textUtils;
    string content;
    bool endLoad = false;
    bool isSelf = false;
    Coroutine currentCoroutine = null;

    void Awake()
    {
        if(textMeshPro==null)
            textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(Load());
    }

    public void Init(string _)
    {
        content = _;
        if (textMeshPro == null)
            print("Name of failing item: " + transform.name);
        textMeshPro.text = GetEffects();
        endLoad = true;
        isSelf = true;
    }

    public void Init()
    {
        content = textMeshPro.text;
        textMeshPro.text = GetEffects();
    }

    IEnumerator Load()
    {
        float i = 0;
        float loadTime = .2f;
        while (i < loadTime)
        {
            i += Time.deltaTime;
            yield return null;
        }
        i = .5f;
        yield return new WaitUntil(() => textMeshPro != null);

        yield return new WaitUntil(() => endLoad || i >= loadTime);

        if (!isSelf)
            Init();

        currentCoroutine = null;
    }

    string GetEffects()
    {
        return textUtils.GetEffects(content);
    }
}
