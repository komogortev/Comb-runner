using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManagerScript : MonoBehaviour {
	public Animator menuWrapper;
	// Use this for initialization

	public void StartGame(string scene) 
	{
		SceneManager.LoadScene(scene);
	}

	// Load Lobby Menu Scene
	public void LoadMenu()  {
		//return to menu
		SceneManager.LoadScene("MenuScene");
 			 
	}

	public void OpenCustomizer(string character) 
	{
		menuWrapper.SetBool("isHidden", true);

	}
	public void HideCustomizer() 
	{
		menuWrapper.SetBool("isHidden", false);
	}


}
