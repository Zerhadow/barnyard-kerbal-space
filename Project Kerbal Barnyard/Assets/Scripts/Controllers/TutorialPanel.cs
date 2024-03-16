using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private bool firstStart = true;

    private void Start()
    {
        panel.SetActive(false);
    }
    public void TryTurorial()
    {
        if(firstStart)
        {
            ResetTutorial();
        }/*
        else
        {
            DisableTutorial();
        }*/
    }
    private void ResetTutorial()
    {
        if(panel != null)
        {
            panel.SetActive(true);

            foreach (Transform child in panel.transform)
            {
                child.gameObject.SetActive(false);
            }

            panel.transform.GetChild(0).gameObject.SetActive(true);
            panel.transform.GetChild(1).gameObject.SetActive(true);
        }

        firstStart = false;
    }
    private void DisableTutorial()
    {
        panel?.SetActive(false);
    }
}
