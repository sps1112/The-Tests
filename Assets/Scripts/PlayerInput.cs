using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isTesting;

    private bool canInput;

    void Start()
    {
        if (!isTesting && !GameData.hasData)
        {
            canInput = false;
        }
        else
        {
            canInput = true;
        }
    }

    public void SetInput(bool status)
    {
        canInput = status;
    }

    void Update()
    {
        if (canInput)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Controller>().Jump();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.Return))
            {
                GetComponent<Controller>().Dash();
            }
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("GameManager").GetComponent<UIManager>().Pause();
        }
    }

    void FixedUpdate()
    {
        if (canInput)
        {
            float horizontal = Input.GetAxis("Horizontal");
            GetComponent<Movement>().Move(horizontal);
        }
        else
        {
            GetComponent<Movement>().Move(0f);
        }
    }
}
