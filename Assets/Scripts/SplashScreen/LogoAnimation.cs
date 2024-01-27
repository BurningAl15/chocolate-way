using System.Collections;
using UnityEngine;
using TMPro;

namespace AldhaDev.SplashScreen
{
    using Managers;
    
    public class LogoAnimation : MonoBehaviour
    {
        [SerializeField] private string titleContent;
        [SerializeField] private string studioContent;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI studioText;


        private Coroutine _currentCoroutine = null;

        void Awake()
        {
            titleText.text = studioText.text = "";
        }

        public void CallCoroutine()
        {
            _currentCoroutine ??= StartCoroutine(TextCoroutine());
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
            yield return StartCoroutine(TransitionManager.Current.FadeIn(.5f));
            SceneUtils.ToNextLevel();

            _currentCoroutine = null;
        }
    }
}