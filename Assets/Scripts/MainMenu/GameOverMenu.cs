using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text now, best;

    private void Start()
    {
        now.text = PlayerPrefs.GetString("currentScore");
        best.text = ""+PlayerPrefs.GetInt("personalBest");
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game....");
        Application.Quit();
    }
}
