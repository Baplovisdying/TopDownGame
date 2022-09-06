using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryer : MonoBehaviour
{
    [SerializeField] private float destoryTime;

    private void Start()
    {
        Destroy(gameObject, destoryTime);
    }
}
