using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private int currentCombo;
    [SerializeField] public int currentScore;
    [SerializeField] private float comboTime;
    [SerializeField] private float lastComboTime;
    [SerializeField] private float animationTime;
    [SerializeField] private Text comboText;
    [SerializeField] private Text scoreText;

    private float shakePower;
    private Vector3 shakeActive;

    private void Start()
    {

    }

    private void Update()
    {
        if (Time.time > lastComboTime + comboTime)
        {
            comboText.enabled = false;
        }
    }

    public void Combo()
    {
        comboText.enabled = true;
        if(Time.time <= lastComboTime + comboTime)
        {
            currentCombo++;
            comboText.text = "连击 X <color=orange>" + currentCombo.ToString() + "</color>";
            comboText.gameObject.GetComponent<Animator>().SetBool("Combo", true);
            shakePower = 1f;
        }
        else
        {
            currentCombo = 1;
            comboText.text = "连击 X <color=orange>" + currentCombo.ToString() + "</color>";
        }
        lastComboTime = Time.time;

        RollingScore();
        comboText.gameObject.GetComponent<Animator>().SetBool("Combo", false);
    }

    void RollingScore()
    {
        int fromScore = currentScore;
        int toScore = fromScore + currentCombo * 5;

        LeanTween.value(fromScore, toScore, animationTime).setEase(LeanTweenType.easeOutQuart)
            .setOnUpdate((float _obj) =>
            {
                fromScore = (int)_obj;
                scoreText.text = "得分：<color=orange>" + _obj.ToString("00000") + "</color>";
            });

        currentScore = (int)toScore;
    }

    public void UseScore(int point)
    {
        int fromScore = currentScore;
        int toScore = point;

        LeanTween.value(fromScore, toScore, animationTime).setEase(LeanTweenType.easeOutQuart)
            .setOnUpdate((float _obj) =>
            {
                fromScore = (int)_obj;
                scoreText.text = "得分：<color=orange>" + _obj.ToString("00000") + "</color>";
            });

        currentScore = (int)toScore;
    }

}
