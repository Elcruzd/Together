using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager loadInstance;
    public GameObject LoadingScreen;
    public string sceneToLoad;

    public float loadTime = 1f;
    public Animator transition;

    private void Awake()
    {
        // pause in loading screen while change scene
        if (loadInstance == null) {
            loadInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        // set loading screen disable
        LoadingScreen.SetActive(false);
    }

    public void SwitchScene(string sceneToLoad) {
        // load scene
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene() {
        LoadingScreen.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneToLoad);
        // Play Animation
        transition.SetTrigger("Start");
        // set wait time
        yield return new WaitForSeconds(loadTime);

        LoadingScreen.SetActive(false);
    }
}
