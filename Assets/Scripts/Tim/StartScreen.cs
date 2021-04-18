using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    public void OnStartClick()
    {
        Invoke("StartGame", 0.3f);
    }

    private void StartGame()
	{
		SceneManager.LoadScene("FinalScene");
	}

    public void OnExitClick()
	{
		Invoke("ExitGame", 0.3f);
	}

    private void ExitGame()
	{
		Application.Quit();

    #if UNITY_EDITOR
		Debug.LogWarning("Would quit in actual game executable");
    #endif
	}

}
