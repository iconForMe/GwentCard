using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;

public class Text02 : MonoBehaviour, IPointerDownHandler
{
    // Use this for initialization
    public Image card;
    public static int count = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void test()
    {
        Sprite sp = Resources.Load("1", typeof(Sprite)) as Sprite;
        card.sprite = sp;

    }

    public void add_card()
    {
        Text tex = GameObject.Find("T" + count.ToString()).GetComponent<Text>();
        tex.text = (int.Parse(card.name) + 15).ToString();
    }

    public void text2()
    {
        int i = 1;
        while(i < 16)
        {
            card = GameObject.Find(i.ToString()).GetComponent<Image>();
            Sprite sp = Resources.Load(((int.Parse(card.name) + 15).ToString()), typeof(Sprite)) as Sprite;
            card.sprite = sp;
            i++;
        }
        
    }

    


    public void OnPointerDown(PointerEventData eventData)
    {
        test();
        add_card();
    }
}

