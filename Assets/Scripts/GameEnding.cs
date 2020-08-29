using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    private bool hasEntered = false;

    public bool isFinal = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasEntered)
        {
            if (other.gameObject.tag == "Player")
            {
                if (isFinal)
                {
                    GameObject.Find("GameManager").GetComponent<UIManager>().ShowFinal();
                    GameObject.FindWithTag("Player").GetComponent<PlayerInput>().SetInput(false);
                }
                else
                {
                    hasEntered = true;
                    GameData.hasData = true;
                    GameData.roomBounds = Camera.main.GetComponent<CameraFollow>().GetBounds();
                    GameData.cameraPosition = Camera.main.transform.position;
                    GameData.playerPosition = GameObject.FindWithTag("Player").transform.position;
                    GameData.playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>().GetHealth();
                    GameData.keyList = GameObject.Find("GameManager").GetComponent<KeyManager>().GetList();
                    GameData.playerVelocity = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity;
                    StartCoroutine(LoadScene());
                }
            }
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (loadingScene.progress < 1)
        {
            yield return null;
        }
    }
}
