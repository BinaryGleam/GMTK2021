using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneClass : MonoBehaviour
{
    public string sceneToLoad = "SceneName";
	public LoadSceneMode loadMode = LoadSceneMode.Single;

    public void ChangeScene()
	{
		SceneManager.LoadScene(sceneToLoad, loadMode);
	}
}
