using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController _instance;

    [SerializeField] Localization_Scene localization_Scene;

    [SerializeField] LocalizationController localizationController;

    //Save file name
    const string fileName = "SaveFile";
    //save path, it will be constructed in Awake
    string path;

    //Variable to store all your saved values
    [SerializeField] SavedValues savedValues;

    //Construct Path
    void Awake()
    {
        _instance = this;
        path = Application.persistentDataPath + "/" + fileName;
    }

    //Populate UI elements with values from save
    public void Refresh()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (savedValues.localizationLanguage_saved)
        {
            // if(localization_Scene==Localization_Scene.MainScene)
            //     MainSceneController._instance.InitializeLocalization(false);
            localizationController.InitRefreshTexts();
            localizationController.SetFromDictionary(savedValues.localizationLanguage);
        }
        else
        {
            // if(localization_Scene==Localization_Scene.MainScene)
            //     MainSceneController._instance.InitializeLocalization(true);
            localizationController.InitRefreshTexts();
        }

        AudioMixerManager._instance.LoadSoundIndex(savedValues.isSound_saved, savedValues.sfxIndex, savedValues.backgroundIndex);
    }

    public void SaveSound(float _sfx, float _background)
    {
        savedValues.sfxIndex = _sfx;
        savedValues.backgroundIndex = _background;
        savedValues.isSound_saved = true;
        SaveManager.Instance.Save(savedValues, path, SaveComplete, false);
    }

    public void Save(string _)
    {
        savedValues.localizationLanguage = _;
        savedValues.localizationLanguage_saved = true;
        SaveManager.Instance.Save(savedValues, path, SaveComplete, false);
    }

    //This method will be called after save is done
    void SaveComplete(SaveResult result, string message)
    {
        //Check for error
        if (result == SaveResult.Error)
        {
            Debug.LogError("Save error " + message);
        }

        //If no error save was successful
    }

    //Load data from file
    public void Load()
    {
        SaveManager.Instance.Load<SavedValues>(path, LoadComplete, false);
    }

    //This method will be called when load process is done
    void LoadComplete(SavedValues data, SaveResult result, string message)
    {
        //Result is success -> Load your save data into variables
        if (result == SaveResult.Success)
        {
            savedValues = data;
        }

        //If for some reason your load failed, create an empty data to work with inside your game
        //or give a message to the user
        if (result == SaveResult.Error || result == SaveResult.EmptyData)
        {
            savedValues = new SavedValues();
        }

        //After load is done, refresh the UI
        Refresh();
    }
}
