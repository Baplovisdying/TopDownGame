using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    private int index;

    public void DisableHeart()
    {
        if (index > 2) return;
        hearts[index].enabled = false;
        index++;
    }
}
