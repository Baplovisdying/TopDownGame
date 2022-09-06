using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ITakenDamage takenDamage = collision.GetComponent<ITakenDamage>();
            if (!takenDamage.isAttack)
            {
                takenDamage.TakeDamage(1);

                Vector2 des = collision.transform.position - transform.position;
                des.Normalize();
                collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x + des.x,
                                                                        collision.gameObject.transform.position.y + des.y);
            }
        }
    }
}
