using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenüUI : MonoBehaviour
{
    public GameObject PauseMenü;

    private void Awake()
    {
        PauseMenü.SetActive(false);
    }

    private void PauseGame()
    {
        PauseMenü.SetActive(true);

        Time.timeScale = 0;
    }

    
    public void ResumeGame()
    {
        Time.timeScale = 1;

        PauseMenü.SetActive(false);
    }

    public void BackToMainMenü()
    {
        SceneManager.LoadScene("StartMenü");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenü.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
    }
}
