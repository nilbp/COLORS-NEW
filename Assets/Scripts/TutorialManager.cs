﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public static bool gameOver;
    public GameObject gameoverPanel;

    public static bool lastMinion;
    public GameObject[] panel;
    public int index = 0;

    TutorialManager tutorialManager;


    public void LoadNext()
    {
        panel[index].SetActive(false);
        panel[index+1].SetActive(true);
        index ++;
    }
    public void LoadLast()
    {
        panel[index].SetActive(false);
        //Time.timeScale = 1;

    }

    public void LoadFirst(int i)
    {
        Debug.Log("hola");
        panel[index].SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        
		if (lastMinion)
        {
            LoadNext();
            lastMinion = false;
        }

        GameOver();
    }


    void GameOver()
    {
        if (gameOver)
        {
            gameoverPanel.SetActive(true);

            Invoke("ChangeLevel", 4.5f);
        }
    }

    void ChangeLevel()
    {

        SceneManager.LoadScene("lvlSelector");
    }
}
