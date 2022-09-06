using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashIcon : MonoBehaviour
{
    [SerializeField] private Image[] dashIcons;
    private int index;

    private void Start()
    {
        index = 0;
    }

    public void RecoverDashIcon()
    {
        if (index < 0) return;
        index--;
        dashIcons[index].enabled = true;
    }

    public void DashIconUsed()
    {
        if (index > 2) return;
        dashIcons[index].enabled = false;
        index++;
    }
}
