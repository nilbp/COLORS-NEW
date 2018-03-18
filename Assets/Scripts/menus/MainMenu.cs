using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	public void newGameBtn(string newlvl){
	
		SceneManager.LoadScene (newlvl);

	}
	public void ExitBtn(){

		Application.Quit();

	}
	public void OptionsBtn(string options){

		SceneManager.LoadScene (options);

	}
	public void lvlSelectBtn(string lvlSelector){

		SceneManager.LoadScene (lvlSelector);

	}
	public void mon1(string Nil_Level){

		SceneManager.LoadScene (Nil_Level);

	}
	public void mon2(string Nil_Level1){

		SceneManager.LoadScene (Nil_Level1);

	}
	public void mon3(string Nil_Level2){

		SceneManager.LoadScene (Nil_Level2);

	}
	public void mainMenu(string MainMenu){

		SceneManager.LoadScene (MainMenu);

	}
}
