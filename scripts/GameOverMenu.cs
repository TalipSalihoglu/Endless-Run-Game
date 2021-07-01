using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject p;
    private UIManager uiManager;
    public static bool isDeadGM;

    // Start is called before the first frame update
    void Start()
    {
        p= GameObject.FindGameObjectWithTag("Player");
        uiManager = FindObjectOfType<UIManager>();
        isDeadGM = false;
        heartCount = uiManager.GetAsiCounter();
       
    }

    // Update is called once per frame
    void Update()
    {
        heartCount = uiManager.GetAsiCounter();
        uiManager.UpdateHG();
    }

    public void StartRun()
    {
        CallMenu();
    }

    private int heartCount;
    private int _countinueValue= 30;

    public void GameCountinue()
    {
        if (heartCount >= _countinueValue)
        {
            
            isDeadGM = true;
            Player.currentLife = 1;
            uiManager.gameOverPanel.SetActive(false);
            uiManager.UpdateLives(1);
            Debug.Log("Can satýn alýndý!");

        }
        else
        {
            Debug.Log("Yeterli Sayýda Aþý yok");
            Invoke("CallMenu", 10f);       
        }

    }
    void CallMenu()
    {
        GameManager.gm.EndRun();
        Debug.Log("Menüye dönüldü!");
    }

}
