using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Localization_Scene
{
    MainScene,
    Game
}

[Serializable]
public class LanguageIndex
{
    public SupportedLanguages languageSupported;
    public string languageString;
    public Sprite languageSprite;
}

public class LocalizationController : MonoBehaviour
{
    [SerializeField] Localization_Scene localization_Scene;
    [SerializeField] List<LanguageIndex> spriteList = new List<LanguageIndex>();
    [SerializeField] Image languageImg, leftImg, rightImg;

    int index = 0;
    [SerializeField] TextMeshProUGUI languageText;
    [SerializeField] TextMeshProUGUI selectText;

    [Header("Main Scene Texts")]
    [SerializeField] TextMeshProUGUI playText;
    [SerializeField] TextMeshProUGUI musicText;
    [SerializeField] TextMeshProUGUI exitText;

    [Header("Level Texts")]
    [SerializeField] TextMeshProUGUI matchRuneText;
    [SerializeField] TextMeshProUGUI goodMessageAnimationText;
    [SerializeField] TextMeshProUGUI wrongMessageAnimationText;
    [SerializeField] TextMeshProUGUI endMessageAnimationText;
    [SerializeField] TextMeshProUGUI gloriousText;



    [Header("Music Settings Texts")]
    [SerializeField] TextMeshProUGUI musicSettingsText;
    [SerializeField] TextMeshProUGUI backgroundText;
    [SerializeField] TextMeshProUGUI sfxText;
    [SerializeField] TextMeshProUGUI closeText;

    [Header("Paused Settings Texts")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI replayText;
    [SerializeField] TextMeshProUGUI mainMenuText;
    [SerializeField] TextMeshProUGUI closePausedText;


    [SerializeField] List<SupportedLanguages> supportLanguages = new List<SupportedLanguages>();
    Dictionary<SupportedLanguages, LanguageIndex> languages = new Dictionary<SupportedLanguages, LanguageIndex>();
    Dictionary<string, LanguageIndex> languagesString = new Dictionary<string, LanguageIndex>();
    LanguageIndex currentLanguageIndex;

    public string CurrentLanguageString { get { return currentLanguageIndex.languageString; } }

    // void Start()
    // {
    // }

    public void Init()
    {
        supportLanguages = GleyLocalization.Manager.GetSupportedLanguages();
        for (int i = 0; i < supportLanguages.Count; i++)
        {
            if (supportLanguages[i] == spriteList[i].languageSupported)
            {
                languages.Add(supportLanguages[i], spriteList[i]);
                languagesString.Add(spriteList[i].languageString, spriteList[i]);
            }
        }
    }

    public string GetLanguage()
    {
        return currentLanguageIndex.languageString;
    }

    void RefreshTexts_MainScene()
    {
        languageText.text = GleyLocalization.Manager.GetCurrentLanguage().ToString();

        playText.text = GleyLocalization.Manager.GetText(WordIDs.PlayID);
        musicText.text = GleyLocalization.Manager.GetText(WordIDs.MusicID);
        exitText.text = GleyLocalization.Manager.GetText(WordIDs.ExitID);

        MusicTexts();

        selectText.text = GleyLocalization.Manager.GetText(WordIDs.SelectionID);
    }

    void RefreshTexts_Game()
    {
        MusicTexts();

        titleText.text = GleyLocalization.Manager.GetText(WordIDs.PauseID);
        replayText.text = GleyLocalization.Manager.GetText(WordIDs.ReplayID);
        mainMenuText.text = GleyLocalization.Manager.GetText(WordIDs.MainMenuID);
        closePausedText.text = GleyLocalization.Manager.GetText(WordIDs.CloseID);

        matchRuneText.text = GleyLocalization.Manager.GetText(WordIDs.MatchRunesID);
        goodMessageAnimationText.text = GleyLocalization.Manager.GetText(WordIDs.ExcellentID);
        wrongMessageAnimationText.text = GleyLocalization.Manager.GetText(WordIDs.WrongTryID);
        endMessageAnimationText.text = GleyLocalization.Manager.GetText(WordIDs.AmazingJobID);
        gloriousText.text = GleyLocalization.Manager.GetText(WordIDs.GloriousID);
    }

    void MusicTexts()
    {
        musicSettingsText.text = GleyLocalization.Manager.GetText(WordIDs.MusicSettingsID);
        backgroundText.text = GleyLocalization.Manager.GetText(WordIDs.BackgroundID);
        sfxText.text = GleyLocalization.Manager.GetText(WordIDs.SFXID);
        closeText.text = GleyLocalization.Manager.GetText(WordIDs.CloseID);
    }

    #region Dictionary Methods
    Sprite ShowSpriteFromDictionary(SupportedLanguages supportedLanguages)
    {
        LanguageIndex temp = null;

        if (languages.TryGetValue(supportedLanguages, out temp))
        {
            //success!
            currentLanguageIndex = temp;
            return temp.languageSprite;
        }
        else
        {
            //failure!
            print("FAILING!");
            return null;
        }
    }

    LanguageIndex ShowLanguageFromDictionary(SupportedLanguages supportedLanguages)
    {
        LanguageIndex temp = null;

        if (languages.TryGetValue(supportedLanguages, out temp))
        {
            //success!
            currentLanguageIndex = temp;
            return temp;
        }
        else
        {
            //failure!
            print("FAILING!");
            return null;
        }
    }

    public void SetFromDictionary(string supportedLanguages)
    {
        LanguageIndex temp = null;

        if (languagesString.TryGetValue(supportedLanguages, out temp))
        {
            //success!
            currentLanguageIndex = temp;
            if (localization_Scene == Localization_Scene.MainScene)
                RefreshFlag(currentLanguageIndex.languageSupported);
        }
        else
        {
            //failure!
            print("FAILING!");
        }
    }
    #endregion

    public void InitRefreshTexts()
    {
        Init();
        if (localization_Scene == Localization_Scene.MainScene)
        {
            RefreshFlag(GleyLocalization.Manager.GetCurrentLanguage());
            RefreshTexts_MainScene();
        }
        else if (localization_Scene == Localization_Scene.Game)
            RefreshTexts_Game();
    }

    public void RefreshTexts()
    {
        if (localization_Scene == Localization_Scene.MainScene)
        {
            RefreshFlag(GleyLocalization.Manager.GetCurrentLanguage());
            RefreshTexts_MainScene();
        }
        else if (localization_Scene == Localization_Scene.Game)
            RefreshTexts_Game();
    }

    public void ChangeLanguage(int _language)
    {
        if (_language == 0)
            GleyLocalization.Manager.PreviousLanguage();
        else if (_language == 1)
            GleyLocalization.Manager.NextLanguage();

        RefreshTexts();
    }

    void RefreshFlag(bool isAdding)
    {
        if (isAdding)
        {
            index++;
            if (index > spriteList.Count - 1)
                index = 0;
        }
        else if (!isAdding)
        {
            index--;
            if (index < 0)
                index = spriteList.Count - 1;
        }
        languageImg.sprite = spriteList[index].languageSprite;
        leftImg.sprite = spriteList[index].languageSprite;
        rightImg.sprite = spriteList[index].languageSprite;
    }

    void RefreshFlag(SupportedLanguages supportedLanguages)
    {
        Sprite temp = ShowSpriteFromDictionary(supportedLanguages);
        if (languageImg != null)
            languageImg.sprite = temp;
        if (leftImg != null)
            leftImg.sprite = temp;
        if (rightImg != null)
            rightImg.sprite = temp;
    }

    public void SetCurrentLanguage()
    {
        GleyLocalization.Manager.SetCurrentLanguage(GleyLocalization.Manager.GetCurrentLanguage());
        currentLanguageIndex = ShowLanguageFromDictionary(GleyLocalization.Manager.GetCurrentLanguage());
        SaveController._instance.Save(currentLanguageIndex.languageString);
        RefreshTexts();
    }
}
