using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{

	public void ChangeScene (int buildIndex)
	{
		SceneManager.LoadScene (buildIndex);
		Time.timeScale = 1f;
	}

	public void QuitGame ()
	{
		Debug.Log ("quitGame");
		Application.Quit ();
	}
}
