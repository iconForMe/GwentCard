using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Move : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public Image card;
    public static bool Cardlock;
    // Start is called before the first frame update
    void Start()
    {
        card = GetComponentInChildren<Image>();
        Sprite sp = Resources.Load("1", typeof(Sprite)) as Sprite;
        card.sprite = sp;
        Cardlock = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        StartCoroutine(MoveToPosition());
        //StartCoroutine(Fade());
        Cardlock = true;

    }

    IEnumerator MoveToPosition()
    {
        yield return new WaitUntil(() => Cardlock == true);

        while (card.transform.localPosition != new Vector3(-5, -3, 0))
        {
            card.transform.localPosition = Vector3.MoveTowards(card.transform.localPosition, new Vector3(-20, -3, 0), 1000 * Time.deltaTime);
            Cardlock = false;
            yield return true;
        }
        //Cardlock = false;


        //yield return new WaitUntil(() => card.transform.localPosition == new Vector3(-5, -3, 0));
    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = card.color;
            c.a = f;
            card.color = c;
            yield return null;//下一帧继续执行for循环
            yield return new WaitForSeconds(0.1f);//0.1秒后继续执行for循环
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
