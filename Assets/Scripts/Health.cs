using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public Stats statsData;

    private float maxHealth;

    private float defence;

    [SerializeField]
    private float currentHealth;

    public TextMeshProUGUI healthUI = null;

    [SerializeField]
    private bool toWait = false;

    private float timer = 0;

    private float immunityPeriod;

    private float shakeFactor;

    [SerializeField]
    private ParticleSystem hurtParticle;

    [SerializeField]
    private GameObject sprite;

    void Awake()
    {
        SetStats();
        sprite = transform.Find("Sprite").gameObject;
        hurtParticle = transform.Find("Particle").gameObject.GetComponent<ParticleSystem>();
        currentHealth = maxHealth;
        if (GameData.hasData && this.gameObject.tag == "Player")
        {
            currentHealth = GameData.playerHealth;
        }
    }

    void Start()
    {
        SetUI();
    }

    void SetStats()
    {
        maxHealth = statsData.health;
        defence = statsData.defence;
        immunityPeriod = statsData.immunityPeriod;
        shakeFactor = statsData.hurtShakeFactor;
    }

    void Update()
    {
        if (toWait)
        {
            timer += Time.deltaTime;
            if (timer >= immunityPeriod)
            {
                toWait = false;
                timer = 0f;
            }
        }
    }

    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            amount *= (1 - (defence / 100));
            if (toWait)
            {
                return;
            }
            else
            {
                toWait = true;
                timer = 0f;
                hurtParticle.Play();
                Camera.main.GetComponent<ScreenShake>().Shake(shakeFactor);
            }
        }
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SetUI();
        if (currentHealth == 0)
        {
            OnDeath();
        }
    }

    void SetUI()
    {
        if (healthUI != null)
        {
            healthUI.text = ((int)(currentHealth)).ToString();
        }
    }

    void OnDeath()
    {
        if (this.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<UIManager>().ShowDeath();
        }
        else if (this.gameObject.tag == "Enemy")
        {
            GameObject.Find("GameManager").GetComponent<EnergyManager>().GenerateEnergy(transform.position);
        }
        GetComponent<BoxCollider2D>().enabled = false;
        sprite.SetActive(false);
        Invoke("End", 0.25f);
    }

    void End()
    {
        this.gameObject.SetActive(false);
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        sprite.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = true;
        SetUI();
    }

    public bool GetStatus()
    {
        return toWait;
    }

    public bool CheckStatus()
    {
        bool status = true;
        if (currentHealth == maxHealth)
        {
            status = false;
        }
        return status;
    }
}
