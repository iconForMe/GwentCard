using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardClassfy : MonoBehaviour {
    public Image card;
    public Sprite sp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CardClassfied(int cardId)
    {
 
            switch (cardId/50)
            {
                case 0://TODO 放置在近战
                card = GameObject.Find("near").GetComponent<Image>();
                Sprite sp = Resources.Load(cardId.ToString(), typeof(Sprite)) as Sprite;
                card.sprite = sp;

                break;
                case 1://TODO 放置在远程
                card = GameObject.Find("far").GetComponent<Image>();
                Sprite sp1 = Resources.Load(cardId.ToString(), typeof(Sprite)) as Sprite;
                card.sprite = sp1;
                break;
                case 2://TODO 放置在攻城
                card = GameObject.Find("ToWall").GetComponent<Image>();
                Sprite sp2 = Resources.Load(cardId.ToString(), typeof(Sprite)) as Sprite;
                card.sprite = sp2;
                break;
                case 3://放置在天气
                card = GameObject.Find("Weather").GetComponent<Image>();
                Sprite sp3 = Resources.Load(cardId.ToString(), typeof(Sprite)) as Sprite;
                card.sprite = sp3;
                break;
                case 4://放置在号角
                card = GameObject.Find("horn").GetComponent<Image>();
                Sprite sp4 = Resources.Load(cardId.ToString(), typeof(Sprite)) as Sprite;
                card.sprite = sp4;
                break;
                default://TODO 英雄卡 放置在近战 不受天气和效果效果影响
                Image card5 = GameObject.Find("near").GetComponent<Image>();
                Sprite sp5 = Resources.Load(cardId.ToString(), typeof(Sprite)) as Sprite;
                card.sprite = sp5;
                break;
            }
    }
        
    
}
