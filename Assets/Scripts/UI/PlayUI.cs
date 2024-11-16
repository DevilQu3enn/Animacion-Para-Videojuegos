using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayUI : MonoBehaviour
{
    public static PlayUI Instance;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        SceneManager.LoadScene("EntregaFinal");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuFinal");
        Time.timeScale = 1;
    }
}
