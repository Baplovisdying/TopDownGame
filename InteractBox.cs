using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractBox : MonoBehaviour
{
    [SerializeField] private GameObject interactTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (!collision.GetComponent<TalkAble>().hadInteracted)
                interactTag.GetComponent<Text>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactTag.GetComponent<Text>().enabled = false;
    }
}
