using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    public static bool onSkill;
    public static bool onDialogue;
    public static Action<SkillData> UpdateSkillUnlocked;
    public static void CellUpdateSkillUnlocked(SkillData skill)
    {
        if (UpdateSkillUnlocked != null)
        {
            UpdateSkillUnlocked.Invoke(skill);
        }
    }


    public static Action<bool> UpdateTalkState;

    public static void  CellUpdateTalkState(bool isTalking)
    {
        if (UpdateTalkState != null)
        {
            UpdateTalkState.Invoke(isTalking);
        }
    }

    public static Action<string> UpdateInteractTag;
    public static void CellUpdateInteractTag(string _text)
    {
        if (UpdateInteractTag != null)
        {
            UpdateInteractTag.Invoke(_text);
        }
    }
}
