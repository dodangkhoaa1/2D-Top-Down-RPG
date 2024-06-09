using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead {  get; private set; }
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    //support for health sliders
    private Slider healthSlider;

    private float currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    const string HEALTH_SLIDER_TEXT = "Health Slider";

    //support for death of player
    //const string TOWN_TEXT = "CharacterSelectScene";
    const string TOWN_TEXT = "SelectionMenu";

    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        isDead = false;
        maxHealth = PlayerController.Instance.playerStats.healthPoint;
        currentHealth = maxHealth;
        knockBackThrustAmount = PlayerController.Instance.playerStats.knockbackForce;
        damageRecoveryTime = PlayerController.Instance.playerStats.invincibleTime;
        UpdateHeartSlider();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHeartSlider();
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());

        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());

        UpdateHeartSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead =true;
            Destroy(ActiveWeapon.Instance.gameObject);

            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(AnimationConsts.PLAYER_DEATH_PARAM);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

        GameObject[] games =  GameObject.FindGameObjectsWithTag(TagConsts.UIPLAYING_TAG);
        foreach (GameObject game in games)
        {
            Destroy(game.gameObject);
        }
        SceneManager.LoadScene(TOWN_TEXT);
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHeartSlider()
    {
        if(healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>(); 
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
