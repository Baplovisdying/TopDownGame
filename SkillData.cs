using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data/Skill",menuName ="SkillData")]
public class SkillData : ScriptableObject
{
    public int skillID;
    public Sprite skillIcon;
    public string skillName;
    [TextArea(1,5)]
    public string skillDes;
    public int skillPointNeeded;

    public bool isUnlocked;
    public SkillData[] preSkillNeeded;
}
