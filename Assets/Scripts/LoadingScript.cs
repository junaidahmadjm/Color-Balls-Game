using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingScript : MonoBehaviour
{
    public string SceneName;
    private void OnEnable()
    {
        StartCoroutine(LoadSceneAsync(SceneName));
    }
    private IEnumerator LoadSceneAsync(string Scene)
    {
        yield return new WaitForSeconds(1.2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }           
            yield return null;
        }
    }
}
