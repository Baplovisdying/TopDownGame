using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject slashHolder;
    private bool isAttacking;
    [SerializeField]private bool isInteracting;
    [SerializeField] private AudioSource attackAudio;

    private void Start()
    {
        animator.enabled = false;
        EventHandler.UpdateTalkState += UpdateIsInteracting;
    }

    private void OnDestroy()
    {
        EventHandler.UpdateTalkState -= UpdateIsInteracting;
    }

    private void Update()
    {
        if (!isInteracting)
            HandleAttack();
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            attackAudio.Play();
            animator.enabled = true;
            isAttacking = true;
            StartCoroutine(IResetAttack());
            Vector2 des = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(des.y, des.x) * Mathf.Rad2Deg;
            slashHolder.transform.rotation = Quaternion.Euler(0, 0, angle);
            animator.Play("slash");
            //Debug.Log("attacked");
        }
    }

    IEnumerator IResetAttack()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    void UpdateIsInteracting(bool _isInteracting)
    {
        isInteracting = _isInteracting;
    }
}
