using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using AldhaDev.ExtensionMethods;

public class Countdown : MonoSingleton<Countdown>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private const int countTime = 3;
    [SerializeField] private const float timeBetweenCount = 0.35f;
    [SerializeField] AnimationCurve animationCurve;
    [FormerlySerializedAs("duration")] [SerializeField] private float fadeDuration;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        canvasGroup.CanvasGroupInteractable(false);
        canvasGroup.CanvasGroupFade(0);
    }

    public void InitWithFade()
    {
        canvasGroup.CanvasGroupInteractable(true);
        canvasGroup.CanvasGroupFade(1);
    }
    

    public IEnumerator CountDown()
    {
        int value = countTime;
        countdownText.text = $"{value}";
        yield return null;
        
        while (value >= 0)
        {
            countdownText.text = value > 0 ? $"<Bounce> {value} </Bounce>" : "<Bounce> Go! </Bounce>";
            yield return new WaitForSeconds(timeBetweenCount);
            value--;
        }
        yield return new WaitForSeconds(timeBetweenCount);

        for (float i = fadeDuration; i > 0; i -= Time.deltaTime)
        {
            canvasGroup.CanvasGroupFade(animationCurve.Evaluate(i / fadeDuration));
            yield return null;
        }
        canvasGroup.CanvasGroupFade(0);
        canvasGroup.CanvasGroupInteractable(false);
        yield return null;
    }
}
