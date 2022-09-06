using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float floatTime;

    private void Update()
    {
        HandleNumFloat();
    }

    public void Init(int _amount)
    {
        text.text = _amount.ToString();
    }

    void HandleNumFloat()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + floatTime * Time.deltaTime, 0f);
    }
}
