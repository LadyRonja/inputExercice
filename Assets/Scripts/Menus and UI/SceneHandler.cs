using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string sceneToLoadFromStart = "SampleScene";

    public void LaunchGame()
    {
        SceneManager.LoadScene(sceneToLoadFromStart);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
