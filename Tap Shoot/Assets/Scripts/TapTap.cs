using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTap : MonoBehaviour
{    
    [SerializeField] private GameObject exit;
    
    public void OnTap()
    {
        gameObject.SetActive(false);
        exit.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}