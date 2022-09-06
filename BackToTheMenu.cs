using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackToTheMenu : MonoBehaviour, IPointerClickHandler
{
    [System.Obsolete]
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("StartScenes");
        SceneManager.UnloadScene("SampleScene");
    }
}
