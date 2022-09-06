using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private AudioSource hitAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITakenDamage enemy = collision.GetComponentInParent<ITakenDamage>();
        if (enemy != null&&collision.CompareTag("Enemy"))
        {
            if (!enemy.isAttack)
            {
                hitAudio.Play();
                int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                cameraController.StartCameraShake((float)damage / 100);
                enemy.TakeDamage(damage);
                Transform _enemy = collision.gameObject.transform;
                Vector2 difference = _enemy.position - playerBody.position;
                difference.Normalize();
                _enemy.position = new Vector2(_enemy.position.x + difference.x,
                                                       _enemy.position.y + difference.y);
            }
        }
    }
}
