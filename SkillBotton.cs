using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SkillBotton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private SkillData skill;
    [SerializeField] private float timePointerNeededToStay = 0.55f;
    [SerializeField] private float currentPointerStayTime;
    [SerializeField] private AudioSource activeAudio;
    private ScoreKeeper score;
    private Image image;
    bool startCountingTime;
    bool showSkillkit;

    void Start()
    {
        image = GetComponent<Image>();
        score = FindObjectOfType<ScoreKeeper>();
        skill.isUnlocked = false;
        activeAudio = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showSkillkit = true;
        SkillKit.Instance.SetSkillData(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startCountingTime = false;
        showSkillkit = false;
        SkillKit.Instance.CloseDownSkillKit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skill.isUnlocked) return;
        
        if(score.currentScore < skill.skillPointNeeded)
        {
            return;
        }
        else
        {
            score.UseScore(score.currentScore - skill.skillPointNeeded);
        }

        if(skill.preSkillNeeded.Length == 0)
        {
            activeAudio.Play();
            EventHandler.CellUpdateSkillUnlocked(skill);
            skill.isUnlocked = true;
            image.color = Color.white;
        }
        else
        {
            foreach(var s in skill.preSkillNeeded)
            {
                if (s.isUnlocked)
                {
                    activeAudio.Play();
                    EventHandler.CellUpdateSkillUnlocked(skill);
                    skill.isUnlocked = true;
                    image.color = Color.white;
                }
            }
        }
    }
}
