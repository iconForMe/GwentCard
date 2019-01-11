using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class exit1 : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Image img = GameObject.Find("lose").GetComponent<Image>();
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        GameObject.Find("losemusic").GetComponent<AudioSource>().Stop();
        StartCoroutine(Fade("lose"));
        StartCoroutine(Fade("gameover"));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Fade(string name)
    {
        Image round = GameObject.Find(name).GetComponent<Image>();
        float i = 1f;
        Color c = round.color;

        c = round.color;
        c.a = i;
        round.color = c;
        if (i == 0)
        {

        }
        else
        {
            i -= 0.01f;
        }

        yield return null;
        yield return new WaitForSeconds(0.001f);
    }
}
