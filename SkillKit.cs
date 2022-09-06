using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillKit : MonoBehaviour
{
    private static SkillKit instance;

    [SerializeField] private Image skillIcon;
    [SerializeField] private Text skillPoint;
    [SerializeField] private Text skillName;
    [SerializeField] private Text skillDes;

    [SerializeField] private GameObject kit;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SkillKit Instance
    {
        get{ return instance; }
    }

    public void SetSkillData(SkillData skill)
    {
        skillIcon.sprite = skill.skillIcon;
        skillName.text = skill.skillName;
        skillPoint.text = skill.skillPointNeeded.ToString();
        skillDes.text = skill.skillDes;

        transform.position = Input.mousePosition + Vector3.right * 400 + Vector3.down * 100;
        kit.SetActive(true);
    }

    public void CloseDownSkillKit()
    {
        kit.SetActive(false);
    }
}
