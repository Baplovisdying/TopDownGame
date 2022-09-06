using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour,IPointerClickHandler
{
    [System.Obsolete]
    public void OnPointerClick(PointerEventData eventData)
    {
        StartTheGame();
        _ = SceneManager.UnloadScene("StartGame");
    }

    public void StartTheGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
