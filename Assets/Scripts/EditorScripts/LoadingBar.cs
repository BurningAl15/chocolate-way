using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class LoadingBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    [SerializeField] float maximum;
    [SerializeField] float current;
    [SerializeField] Image mask;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] bool finished = false;
    public bool isFinished { get { return finished; } }

    void Awake()
    {
        current = 0;
        UpdateFillAmount();
    }

    public void UpdateFill(float _current)
    {
        if (!finished)
        {
            print(_current);
            current = _current;
            SetCurrentFill();
        }
    }

    // void FixedUpdate()
    // {
    //     if(!finished){
    //         current++;
    //         GetCurrentFill();
    //     }
    // }

    void SetCurrentFill()
    {
        UpdateFillAmount();
    }

    void UpdateFillAmount(){
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
        textMesh.text = "Loading: " + GetPercentage(fillAmount);

        if (current >= maximum)
        {
            finished = true;
        }
    }

    string GetPercentage(float _)
    {
        float _maximum = (float)maximum;
        return (_ * _maximum) + "%";
    }
}
