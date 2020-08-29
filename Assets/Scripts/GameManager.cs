using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void NextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        index %= SceneManager.sceneCountInBuildSettings;
        LoadScene(index);
    }

    public void Reset()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        LoadScene(index);
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void BackToMenu()
    {
        LoadScene(0);
    }
}
