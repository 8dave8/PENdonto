using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonfunctions : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetCoins()
    {
        PlayerPrefs.SetInt("coins", 100);
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
