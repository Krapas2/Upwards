using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


	public static bool gameIsPaused = false;
	public GameObject pausedUI;
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SwitchState ();
		}
	}

	public void Resume ()
	{
		gameIsPaused = false;
		pausedUI.SetActive (false);
		Time.timeScale = 1f;
	}

	public void Pause ()
	{
		gameIsPaused = true;
		pausedUI.SetActive (true);
		Time.timeScale = 0f;
	}

	public void SwitchState ()
	{
		if (gameIsPaused) {
			Resume ();
			Debug.Log ("resume");
		} else if (!gameIsPaused) {
			Pause ();
			Debug.Log ("pause");
		}
	}

}
