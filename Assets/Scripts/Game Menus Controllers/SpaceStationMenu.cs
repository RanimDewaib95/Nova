using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceStationMenu : MonoBehaviour {

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void levelOneClicked()
    {
        //StartCoroutine(LoadNewScene());
        SceneManager.LoadScene("Spacestation-Level1");
        //SceneManager.LoadSceneAsync("Pop up messages");
        //SceneManager.MergeScenes(level1, messages);
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Spacestation-Level1");
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync("Pop up messages");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }
}
