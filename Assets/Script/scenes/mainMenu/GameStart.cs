using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToMainGame()
    {
        SceneManager.LoadScene("mainGame");//跳转到mainGame
    }
    public void ToCardManager()
    {
        //TODO  跳转到卡牌管理界面
       SceneManager.LoadScene("cardManager");
    }
    public void Exit()
    {
        //TODO 
        Application.Quit();

    }
    public void ToStartMenu()
    {
        SceneManager.LoadScene("startMenu");
    }
}
