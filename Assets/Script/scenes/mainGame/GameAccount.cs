using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text;
// 游戏结算
public class GameAccount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //力量结算
    public int StrengthAcount(int []card)
    {
        int strengthNum=0;
        for(int i = 0; i < card.Length; i++)
        {
            if (card[i] == 0)
            {
                strengthNum = strengthNum + 0;
            }
            else
            {
                strengthNum=strengthNum+ReadCardStrength(card[i]);
            }
        }
        return strengthNum;
    }
    //回合结算
    public void RoundAccount(int playerStrength, int enemyStrength,int playerHP,int enemyHP)
    {
        if (playerStrength > enemyStrength)
        {
            
            enemyHP--;

        }
        else if(playerStrength < enemyStrength)
        {
            
            playerHP--;
        }
        else
        {
            
            playerHP--;
            enemyHP--;
        }
    }
    //最终结算
    public void FinalAccount(int playerHp, int enemyHP)
    {
        if (playerHp==0)
        {
            //TODO 显示失败LOGO
            Debug.Log("fail");
        }   
        else if(enemyHP==0)
        {
            Debug.Log("win");
            //TODO 显示胜利LOGO
        }
    }



    public int ReadCardStrength(int cardLine) {

        int strengthNum=0;
        StreamReader sr = new StreamReader("cardStrength.txt", Encoding.Default);
        String line=sr.ReadLine();
        int current_line = 0;

        while (line != null && current_line++ < (cardLine-1))
        {
            
            strengthNum = int.Parse(sr.ReadLine());
   
        }
        
        return strengthNum;
    }




}


