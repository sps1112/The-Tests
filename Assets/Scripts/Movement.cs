using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PlayerData data;

    private Rigidbody2D rb2D;

    private float speed;

    [SerializeField]
    private float lookDirection;

    private SpriteRenderer playerSprite;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (GameData.hasData)
        {
            transform.position = GameData.playerPosition;
            rb2D.velocity = GameData.playerVelocity;
        }
    }

    void Start()
    {
        SetStats();
        playerSprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        lookDirection = +1;
    }

    void SetStats()
    {
        speed = data.moveSpeed;
    }

    void Update()
    {
        if (lookDirection > 0)
        {
            playerSprite.flipX = false;
        }
        else if (lookDirection < 0)
        {
            playerSprite.flipX = true;
        }
    }

    public void Move(float input)
    {
        if (input != 0f)
        {
            lookDirection = input / Mathf.Abs(input);
        }
        Vector2 velocity = rb2D.velocity;
        if (!GetComponent<Controller>().GetStatus() && !GetComponent<Controller>().GetHurtStatus())
        {
            velocity.x = speed * input;
        }
        rb2D.velocity = velocity;
    }

    public float GetDirection()
    {
        return lookDirection;
    }
}
