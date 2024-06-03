using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } } 

    [SerializeField] private float moveSpeed = 1f; //move speed of player
    [SerializeField] private float dashSpeed = 4f; //dash speed of player
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private int indexSlot;
    [SerializeField] public PlayerStats playerStats;

    private PlayerControls playerControls; 
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private Knockback knockback;
    private float startingMoveSpeed;

    private bool facingLeft = false; //state of facing
    private bool isDashing = false; //state of dashing

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();

        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash(); //when click "Space" perform Dash()

        //startingMoveSpeed = moveSpeed; //store original move speed
        startingMoveSpeed = playerStats.moveSpeed; //store original move speed

        //ActiveInventory.Instance.EquipStartingWeapon(PlayerPrefs.GetInt(PrefConsts.CHARACTER_SELECTED_OPTION_KEY));
        ActiveWeapon activeWeapon = ActiveWeapon.Instance;
        GameObject newWeapon = Instantiate(activeWeapon.weaponPrefab, activeWeapon.transform);
        activeWeapon.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat(AnimationConsts.PLAYER_RUN_X_PARAM, movement.x);
        myAnimator.SetFloat(AnimationConsts.PLAYER_RUN_Y_PARAM, movement.y);
    }
    private void Move()
    {
        if (knockback.GettingKnockedBack || PlayerHealth.Instance.isDead) return;

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePosition.x < playerScreenPoint.x) //when mouse is left of player
        {
            mySpriteRender.flipX = true; //flip the player
            facingLeft = true; //player is facing left
        }
        else
        {
            mySpriteRender.flipX = false; //don't flip the player
            facingLeft = false; //player is not facing left
        }
    }

    private void Dash()
    {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina();

            isDashing = true;
            moveSpeed *= dashSpeed; //increase move speed by multiple move speed with dash speed
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed; //back to original move speed after dashing 
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
