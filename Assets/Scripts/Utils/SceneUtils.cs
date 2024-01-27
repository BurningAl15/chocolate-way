using System;
using System.Collections;
using UnityEngine.SceneManagement;
using AldhaDev.Managers;

public static class SceneUtils
{
    public static void GameLevel()
    {
        SceneManager.LoadScene(ConstantUtils.mainGame);
    }

    public static void ToLevelSelection()
    {
        SceneManager.LoadScene(ConstantUtils.levelSelector);
    }

    public static void ToMainScreen()
    {
        SceneManager.LoadScene(ConstantUtils.startView);
    }

    public static void ToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}