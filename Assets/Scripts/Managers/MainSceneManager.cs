using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager _instance;
    public GameObject loadingScreen;
    public LoadingBar bar;

    void Awake()
    {
        _instance=this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.MainScene,LoadSceneMode.Additive);
        loadingScreen.gameObject.SetActive(false);
    }

    List<AsyncOperation> scenesLoading=new List<AsyncOperation>();
    float totalSceneProgress;

    public void LoadScene(){
        loadingScreen.gameObject.SetActive(true);
        print("AA");

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MainScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Level_0,LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    IEnumerator GetSceneLoadProgress(){
        for(int i=0;i<scenesLoading.Count;i++){
            while(!scenesLoading[i].isDone){
                totalSceneProgress=0;
                foreach(AsyncOperation operation in scenesLoading){
                    totalSceneProgress+=operation.progress;
                }

                totalSceneProgress=(totalSceneProgress/scenesLoading.Count)*100f;
                bar.UpdateFill(totalSceneProgress);

                yield return new WaitForSeconds(2f);
            }
        }
        loadingScreen.gameObject.SetActive(false);
        yield return null;
        // LevelManager._instance.Init();
    }
}
