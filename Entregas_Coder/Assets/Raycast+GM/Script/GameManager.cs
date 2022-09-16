using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{        
    public static GameManager instance;
    public bool dontDestroyOnLoad;
    private void Awake()
    {
        if (instance == null)
        {
                Application.targetFrameRate = 60;
            instance = this;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
