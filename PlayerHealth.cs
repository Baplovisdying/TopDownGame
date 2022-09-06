using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour,ITakenDamage
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHp;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private AudioSource getHitAudio;
    private HealthUIManager uIManager;
    private bool isAttacked;

    void Start()
    {
        currentHp = maxHP;
        uIManager = FindObjectOfType<HealthUIManager>();
    }

    void OnDestroy()
    {
    }

    void Update()
    {

    }

    public bool isAttack { get => isAttacked; set => isAttacked = value; }

    public void TakeDamage(int _amount)
    {
        getHitAudio.Play();
        isAttacked = true;
        spriteRenderer.material.SetFloat("_FlashAmount", 1);
        animator.Play("get_hit");
        StartCoroutine(FlashAmount());
        StartCoroutine(AttackedCoolDown());
        currentHp -= _amount;
        cameraController.StartCameraShake(0.3f);
        uIManager.DisableHeart();
        if (currentHp <= 0)
        {
            Time.timeScale = 0;
            gameOverCanvas.SetActive(true);
        }
    }

    IEnumerator FlashAmount()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetFloat("_FlashAmount", 0);
    }

    IEnumerator AttackedCoolDown()
    {
        yield return new WaitForSeconds(1.5f);
        isAttacked = false;
    }
}
