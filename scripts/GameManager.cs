using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    // Start is called before the first frame update

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartRun()
    {
        SceneManager.LoadScene("Game");
    }

  
    public void EndRun()
    {
        SceneManager.LoadScene("Menu");
    }
}