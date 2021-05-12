using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTimer : MonoBehaviour
{
    [SerializeField] public float interval = 2f;

    private float elapsed = 0;

    private Life gameManager;
    
    private void Awake() {
        gameManager = GetComponent<Life>();
    
 
    }

    private void Update()
    {
        if (!gameManager.Pause)
        {
            elapsed += Time.deltaTime;
            if(elapsed >= interval)
            {
                elapsed = 0;
                gameManager.Tick();
            }
        }
        else
        {
            elapsed = 0;
        }
            
    }


}
