using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,
    }

    [SerializeField] private PickUpType pickUpType;

    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelerationRate = .2f;
    [SerializeField] private float moveSpeed = 3f;

    //support for pop-up gold coin
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimationCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPosition = PlayerController.Instance.transform.position;

        if(Vector3.Distance(transform.position, playerPosition) < pickUpDistance)
        {
            moveDirection = (playerPosition - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            moveDirection = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }


    private IEnumerator AnimationCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while(timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearTime = timePassed / popDuration;
            float heightTime = animationCurve.Evaluate(linearTime);
            float height = Mathf.Lerp(0f, heightY, heightTime);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearTime) + new Vector2(0f,height);
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickUpType.StaminaGlobe:
                Stamina.Instance.RefreshStamina();
                break;

        }

    }
}
