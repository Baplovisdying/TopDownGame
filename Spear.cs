using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float flyingSpeed;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private float holdTimeNeeded;
    [SerializeField] private GameObject flyingEffect;
    [SerializeField] private GameObject backEffect;
    [SerializeField] private Collider2D hitCollider;
    [SerializeField] private Animator animator;
    private float currentHoldTime;
    private CameraController camera;
    Vector2 des;
    bool onHold;
    bool isStoped;
    bool isComingBack;
    private bool isInteracting;

    private void Start()
    {
        onHold = true;
        isStoped = true;
        camera = FindObjectOfType<CameraController>();
        EventHandler.UpdateTalkState += UpdateIsInteracting;
    }

    private void OnDestroy()
    {
        EventHandler.UpdateTalkState -= UpdateIsInteracting;
    }

    private void Update()
    {
        if (isInteracting) return;
        if (isStoped)
        {
            HandleMouseInput();
        }

        if (onHold && isStoped)
        {
            HandleMoveWhenHolding();
        }
        else if (!onHold && !isStoped && !isComingBack)
        {
            HandleFlying();
        }
        if (isComingBack)
        {
            HandleComeBack();
        }
    }

    void HandleMoveWhenHolding()
    {
        transform.position = holdPoint.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void HandleFlying()
    {
        hitCollider.enabled = true;
        flyingEffect.SetActive(true);
        rb.velocity = des * flyingSpeed;
    }

    void HandleComeBack()
    {
        backEffect.SetActive(true);
        hitCollider.enabled = false;
        //animator.enabled = false;
        transform.position = Vector2.MoveTowards(transform.position, holdPoint.position, flyingSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, holdPoint.position) <= 1f)
        {
            backEffect.SetActive(false);
            onHold = true;
            isStoped = true;
            isComingBack = false;
            flyingEffect.SetActive(false);
            animator.SetTrigger("Out");
            camera.StartCameraShake(0.25f);
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1) && onHold)
        {
            //animator.enabled = true;
            animator.SetTrigger("Out");
            camera.StartCameraShake(0.25f);
            onHold = false;
            isStoped = false;
            des = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            des.Normalize();
            float angle = Mathf.Atan2(des.y, des.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else if (Input.GetKey(KeyCode.Mouse1) && !onHold)
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= holdTimeNeeded)
            {
                isComingBack = true;
                isStoped = false;
            }
        }
        else
        {
            currentHoldTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            onHold = false;
            isStoped = true;
            rb.velocity = Vector2.zero;
        }
        if (collision.CompareTag("Enemy"))
        {
            ITakenDamage takenDamage = collision.GetComponent<ITakenDamage>();

            if (!takenDamage.isAttack)
            {
                int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                takenDamage.TakeDamage(damage);
                camera.StartCameraShake((float)damage / 100);
                Transform _enemy = collision.transform;
                Vector2 difference = _enemy.position - transform.position;
                difference.Normalize();
                _enemy.position = new Vector2(_enemy.position.x + difference.x,
                                                       _enemy.position.y + difference.y);
            }
        }
    }

    void UpdateIsInteracting(bool _isInteracting)
    {
        isInteracting = _isInteracting;
    }
}
