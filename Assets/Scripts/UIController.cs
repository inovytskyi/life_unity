using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button PauseButton;
    [SerializeField] private Image panel;
    private Life gameManager;

    void Awake()
    {
        gameManager = GetComponent("Life") as Life;
        PauseButton.onClick.AddListener(PauseClickHandler);
        gameManager.pauseEvent.AddListener(PauseEventHandler);
        PauseEventHandler(gameManager.Pause);
        
    }

    // Update is called once per frame
    void PauseClickHandler()
    {
        gameManager.TogglePause();
    }

    void PauseEventHandler(bool pause)
    {
        if (pause)
        {
            PauseButton.GetComponentInChildren<Text>().text = "Go";
            panel.color = Color.white;
            
        } else
        {
            PauseButton.GetComponentInChildren<Text>().text = "Pause";
            panel.color = Color.red;
        }
    }
}
