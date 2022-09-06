using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interact
{
    public string[] interactTexts;
    public int currentIndex;
    public bool onlyInteractOnce;
    public KeyCode[] interactKey;
}
