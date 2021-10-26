using UnityEngine.SceneManagement;

public static class SceneUtils
{
    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}