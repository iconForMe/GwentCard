using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RoundAccount(int playerStrength, int enemyStrength, int playerHP, int enemyHP)
    {
        GameObject.Find("lostheart").GetComponent<AudioSource>().Play();
        if (playerStrength > enemyStrength)
        {

            //enemyHP--;
            StartCoroutine(Fadenow("aiheart"));
           
            GameObject.Find("player_life").GetComponent<Text>().text = (int.Parse(GameObject.Find("player_life").GetComponent<Text>().text) - 1).ToString();

        }
        else if (playerStrength < enemyStrength)
        {

            //playerHP--;
            StartCoroutine(Fadenow("playerheart"));
            GameObject.Find("AI_life").GetComponent<Text>().text = (int.Parse(GameObject.Find("AI_life").GetComponent<Text>().text) - 1).ToString();
        }
        else
        {
            StartCoroutine(Fadenow("aiheart"));
            StartCoroutine(Fadenow("playerheart"));
            GameObject.Find("player_life").GetComponent<Text>().text = (int.Parse(GameObject.Find("player_life").GetComponent<Text>().text) - 1).ToString();
            GameObject.Find("AI_life").GetComponent<Text>().text = (int.Parse(GameObject.Find("AI_life").GetComponent<Text>().text) - 1).ToString();
            //playerHP--;
            //enemyHP--;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("test");
        RoundAccount(int.Parse(GameObject.Find("player_strength").GetComponent<Text>().text), int.Parse(GameObject.Find("Ai_strength").GetComponent<Text>().text), int.Parse(GameObject.Find("player_life").GetComponent<Text>().text), int.Parse(GameObject.Find("AI_life").GetComponent<Text>().text));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
    IEnumerator Fadenow(string name)
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
