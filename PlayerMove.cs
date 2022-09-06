using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float inputX;
    private float inputY;
    [SerializeField]private float dashSpeed;
    [SerializeField]private float dashCoolTime;
    private float dashTime;
    private bool canDash;
    private int dashCount;
    [SerializeField]private float moveSpeed;
    bool isDashing;
    [SerializeField] private GameObject dashEffect;
    [SerializeField] private DashIcon dashIcon;
    [SerializeField] private Collider2D hurtBox;
    public bool dashUnlocked;
    [SerializeField]private bool isInteracting;

    private void Start()
    {
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        EventHandler.UpdateTalkState += UpdateIsInteracting;
    }

    private void OnDestroy()
    {
        EventHandler.UpdateTalkState -= UpdateIsInteracting;
    }

    private void Update()
    {
        if (isInteracting)
        {
            inputX = 0;
            inputY = 0;
            return;
        }
        else
        {
            HandleMoveInput();
            HandleSpriteFlip();
            HandleDash();
            HandleMovement();
        }
    }

    IEnumerator IDashCoolDown()
    {
        yield return new WaitForSeconds(dashCoolTime);
        dashCount--;
        canDash = true;
        dashIcon.RecoverDashIcon();
    }

    void HandleMoveInput()
    {
        if (isInteracting)
        {
            return;
        }
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");       
    }

    void HandleMovement()
    {
        Vector2 moveDes = new Vector2(inputX, inputY);
        if(moveDes != Vector2.zero)
        {
            animator.SetBool("Runing", true);
        }
        else
        {
            animator.SetBool("Runing", false);
        }
        if (!isDashing)
        {
            hurtBox.enabled = true;
            rb.velocity = moveDes.normalized * moveSpeed;
        }
        else
        {
            rb.velocity = moveDes.normalized * dashSpeed;
            hurtBox.enabled = false;
            dashTime -= Time.deltaTime;
        }
        if (dashTime < 0)
        {
            dashTime = 0;
            isDashing = false;
            dashEffect.SetActive(false);
            StartCoroutine(IDashCoolDown());
        }
    }

    void HandleDash()
    {
        if (!dashUnlocked) return;
        if (rb.velocity == Vector2.zero) return;
        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing)
        {
            dashCount++;
            isDashing = true;
            dashTime = 0.15f;
            dashEffect.SetActive(true);
            dashIcon.DashIconUsed();
            if (dashCount >= 3)
            {
                canDash = false;
            }
        }
    }

    void HandleSpriteFlip()
    {
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    void UpdateIsInteracting(bool _isInteracting)
    {
        isInteracting = _isInteracting;
    }
}
