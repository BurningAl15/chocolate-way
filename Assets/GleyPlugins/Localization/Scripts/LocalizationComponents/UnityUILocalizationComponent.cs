namespace GleyLocalization
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class UnityUILocalizationComponent : MonoBehaviour, ILocalizationComponent
    {

        public WordIDs wordID;

        /// <summary>
        /// Used for automatically refresh
        /// </summary>
        public void Refresh()
        {
            TextMeshAnimator temp = GetComponent<TextMeshAnimator>();
            if (temp == null)
                GetComponent<TextMeshProUGUI>().text = LocalizationManager.Instance.GetText(wordID);
            else
            {
                temp.Init(LocalizationManager.Instance.GetText(wordID));
            }
        }

        void Start()
        {
            Refresh();
        }
    }
}
