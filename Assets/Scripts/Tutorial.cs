using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public string tutorialText;

    public float offTime;

    private float timer = 0f;

    private bool hasPlayer = false;

    public GameObject tutorialObject;

    public TextMeshProUGUI textObject;

    void Update()
    {
        if (hasPlayer)
        {
            timer += Time.deltaTime;
            if (timer >= offTime)
            {
                timer = 0f;
                tutorialObject.SetActive(false);
                hasPlayer = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hasPlayer)
        {
            tutorialObject.SetActive(true);
            textObject.text = tutorialText;
            hasPlayer = true;
            timer = 0f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hasPlayer)
        {
            tutorialObject.SetActive(true);
            textObject.text = tutorialText;
            hasPlayer = true;
            timer = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialObject.SetActive(false);
            textObject.text = " ";
            hasPlayer = false;
            timer = 0f;
        }
    }
}
