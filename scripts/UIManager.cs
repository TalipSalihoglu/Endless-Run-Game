using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
	// Start is called before the first frame update
	public Image[] lifeHearts;
	public Text coinText;
	public Text scoreText;
	public Text asiText;
	public Text hscore;
	public GameObject gameOverPanel;							
	public void UpdateLives(int lives)
	{
		for (int i = 0; i < lifeHearts.Length; i++)																																																	
		{
			if (lives > i)
			{
				lifeHearts[i].color = Color.white;
			}
			else
			{
				lifeHearts[i].color = Color.black;
			}
		}
	}
    public void UpdateCoins(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString() + "m";
		float hg = PlayerPrefs.GetFloat("Highscore");
		if (score > hg)
		{
			PlayerPrefs.SetFloat("Highscore", score);
			UpdateHG();
		}
			
    }

    public void UpdateAsi(int asiSayisi)
    {
	    asiText.text  = asiSayisi.ToString(); //asi sayisi
    }
	public int GetAsiCounter()
	{
		return int.Parse(asiText.text);
	}

	public void UpdateHG()
    {
		hscore.text = "High Score :"+PlayerPrefs.GetFloat("Highscore").ToString();		
	}


}
