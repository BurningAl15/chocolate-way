using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public enum SFXType
{
    Click,
    Exit,
    PopUp
}

[Serializable]
public class SFXElement
{
    public AudioClip clip;
    public AudioSource source;

    public void CallSFX()
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void SetPitch(float pitch)
    {
        source.pitch = pitch;
    }
}
public class AudioMixerManager : MonoSingleton<AudioMixerManager>
{
    [SerializeField] private AudioMixer mixer;

    private bool isMusicOn = true;
    private bool isSFXOn = true;


    [Header("SFX")]
    [SerializeField] private SFXElement click_Clip;
    [SerializeField] private SFXElement exit_Clip;
    [SerializeField] private SFXElement popUp_Clip;

    private Coroutine currentCoroutine = null;

    [SerializeField] TextMeshProUGUI sfxText;
    [SerializeField] TextMeshProUGUI backgroundText;

    float sfxIndex = 10f;
    float backgroundIndex = 10f;


    public void CallSFX(SFXType sfxType)
    {
        AudioClip clip = null;
        switch (sfxType)
        {
            case SFXType.Click:
                click_Clip.CallSFX();
                click_Clip.SetPitch(1);
                break;
            case SFXType.Exit:
                exit_Clip.CallSFX();
                exit_Clip.SetPitch(1);
                break;
            case SFXType.PopUp:
                popUp_Clip.CallSFX();
                popUp_Clip.SetPitch(1);
                break;
        }
    }

    public void Click_SFX()
    {
        CallSFX(SFXType.Click);
    }
    public void Exit_SFX()
    {
        CallSFX(SFXType.Exit);
    }
    public void PopUp_SFX()
    {
        CallSFX(SFXType.PopUp);
    }

    public void LoadSoundIndex(bool _, float sfx, float background)
    {
        if (_)
        {
            backgroundIndex = background;
            sfxIndex = sfx;
        }

        SetBackgroundMusicVolume();
        SetBSFXMusicVolume();
    }

    public void AddBackgroundVolume()
    {
        backgroundIndex = Mathf.Clamp(backgroundIndex + 1, 0, 10);
        SetBackgroundMusicVolume();
    }
    public void TakeBackgroundVolume()
    {
        backgroundIndex = Mathf.Clamp(backgroundIndex - 1, 0, 10);
        SetBackgroundMusicVolume();
    }

    public void SetBackgroundMusicVolume()
    {
        // SaveController._instance.SaveBackground(backgroundIndex);

        var _soundLevel = backgroundIndex > 0 ? (backgroundIndex / 10f) : 0.0001f;
        mixer.SetFloat("MusicVol", Mathf.Log10(_soundLevel) * 20);
        backgroundText.text = "" + backgroundIndex;

        // SaveDataManager._instance.gameData.musicLevel = isMusicOn;
        // SaveDataManager._instance.SaveData();
        // var value = isMusicOn ? 1 : 0;
        // PlayerPrefs.SetInt("MusicLevel", value);
    }

    public void SaveMusicIndex()
    {
        // SaveController._instance.SaveSound(sfxIndex, backgroundIndex);
    }

    public void AddSFXVolume()
    {
        sfxIndex = Mathf.Clamp(sfxIndex + 1, 0, 10);
        SetBSFXMusicVolume();
    }
    public void TakeSFXVolume()
    {
        sfxIndex = Mathf.Clamp(sfxIndex - 1, 0, 10);
        SetBSFXMusicVolume();
    }

    public void SetBSFXMusicVolume()
    {
        // SaveController._instance.SaveSFX(sfxIndex);

        var _soundLevel = sfxIndex > 0 ? (sfxIndex / 10f) : 0.0001f;
        mixer.SetFloat("SFXVol", Mathf.Log10(_soundLevel) * 20);
        sfxText.text = "" + sfxIndex;

        // SaveDataManager._instance.gameData.sfxLevel = isSFXOn;
        // SaveDataManager._instance.SaveData();
        // var value = isSFXOn ? 1 : 0;
        // PlayerPrefs.SetInt("SFXLevel", value);
    }

    // private void CheckSFXValue()
    // {
    //     var _soundLevel = 0.0001f;
    //     // if (isSFXOn)
    //     //     _soundLevel = 1;
    //     mixer.SetFloat("SFXVol", Mathf.Log10(_soundLevel) * 20);
    // }

    private void CheckBackgroundValue()
    {
        var _soundLevel = 0.0001f;
        if (isMusicOn)
            _soundLevel = 1;
        mixer.SetFloat("MusicVol", Mathf.Log10(_soundLevel) * 20);
    }

    #region Not Used

    public void SetBackgroundMusicVolume(float _soundLevel)
    {
        print(_soundLevel);
        mixer.SetFloat("MusicVol", Mathf.Log10(_soundLevel) * 20);
        // CheckButton_Music();
    }

    public void SetBSFXMusicVolume(float _soundLevel)
    {
        print(_soundLevel);
        mixer.SetFloat("SFXVol", Mathf.Log10(_soundLevel) * 20);
        // CheckButton_SFX();
    }

    #endregion
}