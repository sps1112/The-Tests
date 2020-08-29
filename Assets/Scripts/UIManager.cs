using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject basicUI;

    public GameObject deathUI;

    public GameObject pauseUI;

    public TextMeshProUGUI[] keyUI;

    private bool isPaused = false;

    public GameObject gameEndUI;

    public void Pause()
    {
        if (isPaused)
        {
            if (pauseUI.activeSelf)
            {
                basicUI.SetActive(true);
                pauseUI.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
        else
        {
            if (basicUI.activeSelf)
            {
                basicUI.SetActive(false);
                pauseUI.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
        }
    }

    public void SetKeyUI(int count1, int count2)
    {
        keyUI[0].text = "X " + count1.ToString();
        keyUI[1].text = "X " + count2.ToString();
    }

    public void ShowDeath()
    {
        basicUI.SetActive(false);
        deathUI.SetActive(true);
    }

    public void ShowFinal()
    {
        gameEndUI.SetActive(true);
        basicUI.SetActive(false);
    }
}
