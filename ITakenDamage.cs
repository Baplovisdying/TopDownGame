using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakenDamage
{
    bool isAttack { get; set; }

    void TakeDamage(int _amount);
}
