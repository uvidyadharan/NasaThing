using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        float loadTime = Time.time;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");
        asyncLoad.allowSceneActivation = false;
        while(asyncLoad.progress <= 0.89f){
               yield return null;
        }

        loadTime = Time.time - loadTime;
        loadTime %= 6;

        yield return new WaitForSeconds(6.2f-loadTime);

        asyncLoad.allowSceneActivation = true;
    }

}
