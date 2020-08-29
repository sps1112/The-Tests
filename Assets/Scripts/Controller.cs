using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public PlayerData data;

    private Rigidbody2D rb2D;

    private float jumpForce;

    public Vector2[] offset;

    private float detectRange;

    [SerializeField]
    private bool isInAir = false;

    private float gravityMultiplier;

    private float lag;

    private float lagTimer = 0f;

    [SerializeField]
    private bool canJump = true;

    [SerializeField]
    private bool toCount = false;

    private float dashForce;

    private float dashTimer = 0;

    [SerializeField]
    private bool isDashing = false;

    private float dashPeriod;

    private float wallDetectRange;

    private float rechargeTime;

    private float rechargeTimer;

    [SerializeField]
    private bool isRecharging = false;

    private SpriteRenderer sprite;

    private Color spriteColor;

    private Color dashColor;

    private Vector2 dashDirection;

    private RigidbodyConstraints2D constraints2D;

    [SerializeField]
    private bool isHurt = false;

    private float hurtPeriod;

    private float hurtTimer = 0f;

    private Material trailMaterial;

    void Start()
    {
        SetStats();
        rb2D = GetComponent<Rigidbody2D>();
        sprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        spriteColor = sprite.color;
    }

    void SetStats()
    {
        jumpForce = data.jumpForce;
        gravityMultiplier = data.gravityMultiplier;
        detectRange = data.groundDetectRange;
        lag = data.jumpTimeLag;
        dashForce = data.dashForce;
        dashPeriod = data.dashTimePeriod;
        dashColor = data.dashColor;
        rechargeTime = data.attackRate;
        hurtPeriod = data.hurtTimePeriod;
        wallDetectRange = data.wallDetectRange;
    }

    void Update()
    {
        if (toCount)
        {
            lagTimer += Time.deltaTime;
            if (lagTimer >= lag)
            {
                lagTimer = 0f;
                canJump = false;
                toCount = false;
            }
        }
        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashPeriod)
            {
                dashTimer = 0f;
                isDashing = false;
                ResetRigidbody(false);
            }
        }
        if (isRecharging)
        {
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer >= rechargeTime)
            {
                rechargeTimer = 0f;
                isRecharging = false;
                sprite.color = spriteColor;
                transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().material = trailMaterial;
            }
        }
        if (isHurt)
        {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= hurtPeriod)
            {
                hurtTimer = 0f;
                isHurt = false;
            }
        }
    }

    void FixedUpdate()
    {
        bool isOnGround = false;
        foreach (Vector2 offseti in offset)
        {
            Debug.DrawRay(rb2D.position + offseti, Vector2.down * detectRange, Color.black, 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(rb2D.position + offseti, Vector2.down, detectRange, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                isOnGround = true;
                break;
            }
        }
        if (isOnGround)
        {
            isInAir = false;
            canJump = true;
            toCount = false;
            lagTimer = 0f;
        }
        else
        {
            if (!isInAir)
            {
                toCount = true;
                lagTimer = 0f;
            }
            isInAir = true;
            if (!isDashing)
            {
                rb2D.AddForce(Physics2D.gravity * gravityMultiplier, ForceMode2D.Force);
            }
        }
        if (isDashing)
        {
            Debug.DrawRay(rb2D.position + offset[0], dashDirection * wallDetectRange, Color.white, 0.1f);
            RaycastHit2D hit2 = Physics2D.Raycast(rb2D.position + offset[0], dashDirection, wallDetectRange, LayerMask.GetMask("Ground"));
            if (hit2.collider != null)
            {
                ResetRigidbody(false);
            }
            RaycastHit2D hit3 = Physics2D.Raycast(rb2D.position + offset[0], dashDirection, wallDetectRange, LayerMask.GetMask("Gate"));
            if (hit3.collider != null)
            {
                GameObject gateObject = hit3.collider.gameObject;
                if (gateObject.tag != "Gate")
                {
                    gateObject = gateObject.transform.parent.gameObject;
                }
                if (gateObject.GetComponent<Gate>().gateType != KeyGateType.Normal)
                {
                    if (!gateObject.GetComponent<Gate>().CheckKey())
                    {
                        ResetRigidbody(false);
                    }
                }
            }
        }
    }

    public void ResetRigidbody(bool status)
    {
        rb2D.isKinematic = status;
        GetComponent<BoxCollider2D>().isTrigger = status;
        if (status)
        {
            constraints2D = rb2D.constraints;
            rb2D.constraints = (RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation);
        }
        else
        {
            rb2D.constraints = constraints2D;
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    public void Dash()
    {
        if (data.canDashInAir)
        {
            if (!isRecharging && !isDashing)
            {
                DashExecute();
            }
        }
        else
        {
            if (canJump && !isRecharging && !isDashing)
            {
                DashExecute();
            }
        }
    }

    void DashExecute()
    {
        float lookDirection = GetComponent<Movement>().GetDirection();
        dashDirection = lookDirection * Vector2.right;
        rb2D.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        isDashing = true;
        isRecharging = true;
        dashTimer = 0f;
        rechargeTimer = 0f;
        sprite.color = dashColor;
        trailMaterial = transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().material;
        transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().material = data.dashMat;
        ResetRigidbody(true);
    }

    public bool GetStatus()
    {
        return isDashing;
    }

    public void AddReaction(Vector3 otherPosition, float force)
    {
        ResetRigidbody(false);
        if (!isHurt && !GetComponent<Health>().GetStatus())
        {
            Vector2 directionForce = Vector2.right * (rb2D.position.x - otherPosition.x);
            directionForce.Normalize();
            Debug.DrawRay(rb2D.position, directionForce * 3f, Color.red, 0.2f);
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            rb2D.AddForce(directionForce * force, ForceMode2D.Impulse);
            isHurt = true;
            hurtTimer = 0f;
        }
    }

    public bool GetHurtStatus()
    {
        return isHurt;
    }
}
