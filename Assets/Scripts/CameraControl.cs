using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Life gameManager;
    private Vector3 offset;

    private void Awake()
    {
        gameManager = GetComponent<Life>();
        offset = Camera.main.transform.position - gameManager.getCenter();
    }
  
}
