using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour,ITakenDamage
{
    private Transform player;
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject damageNumEffect;
    [SerializeField] private GameObject damageNumEffectBig;
    [SerializeField] private Transform impactHolder;
    [SerializeField] private Transform impactPoint;
    [SerializeField] private CameraController cameraController;
    private ScoreKeeper scoreKeeper;
    //private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isAttacked;

    public event Action OnDeath;

    public bool isAttack { get => isAttacked; set => isAttacked=value; }

    private void Start()
    {
        currentHP = maxHP;
        //rb = GetComponent<Rigidbody2D>();
        cameraController = FindObjectOfType<CameraController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int _amount)
    {
        isAttacked = true;
        currentHP -= _amount;
        if (_amount >= 30)
        {
            DamageNum damageNum = Instantiate(damageNumEffectBig, transform.position, Quaternion.identity).GetComponent<DamageNum>();
            damageNum.Init(_amount);
        }
        else
        {
            DamageNum damageNum = Instantiate(damageNumEffect, transform.position, Quaternion.identity).GetComponent<DamageNum>();
            damageNum.Init(_amount);
        }
        
        spriteRenderer.material.SetFloat("_FlashAmount", 1);
        StartCoroutine(AttackCoolDown());
        StartCoroutine(EndFlash());

        Vector2 des = player.transform.position - transform.position;
        float angle = Mathf.Atan2(des.y, des.x) * Mathf.Rad2Deg;
        impactHolder.rotation = Quaternion.Euler(0, 0, angle);

        Instantiate(impactEffect, impactPoint.position, Quaternion.identity);

        if (currentHP <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            cameraController.StartCameraShake(0.3f);
            OnDeath();
            Destroy(gameObject);
        }

        scoreKeeper.Combo();
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.15f);
        isAttacked = false;
    }
    
    IEnumerator EndFlash()
    {
        yield return new WaitForSeconds(0.14f);
        spriteRenderer.material.SetFloat("_FlashAmount", 0);
    }
}
