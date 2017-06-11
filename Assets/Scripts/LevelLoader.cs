using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    public GameObject loadingScreenObject;

    private AsyncOperation async = null;
    private bool isLoading = false;
    // Use this for initialization
    void Awake()
    {
        loadingScreenObject.SetActive(false);
        isLoading = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += this.LevelLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= this.LevelLoaded;
    }

    void Update()
    {
        if (isLoading == false) return;

        if (async != null)
        {
            //loadBar.value = async.progress;
        }
    }

    public bool LoadLevel(string name)
    {
        if (isLoading == true) return false;
        loadingScreenObject.SetActive(true);
        isLoading = true;
        StartCoroutine(name);
        return true;
    }

    IEnumerator LoadScene(string name)
    {
        async = SceneManager.LoadSceneAsync(name);

        yield return async;

        loadingScreenObject.SetActive(false);
        isLoading = false;
        async = null;

        //LevelLoader[] potLevelLoaders = FindObjectsOfType(typeof(LevelLoader)) as LevelLoader[]; //se till så att det bara finns en levelloader i scenen
        //if (potLevelLoaders.Length > 1) Destroy(this.gameObject);
        Debug.Log("Loading complete");
    }

    void LevelLoaded(Scene scene, LoadSceneMode sceneMode) //istället för OnLevelWasLoaded
    {

        Debug.Log("Ny scene");

    }
}
