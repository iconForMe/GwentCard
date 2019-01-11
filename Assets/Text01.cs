using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;
public class Text01 : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    // Use this for initialization
    public Image card;
    public static int count = 1;
    public static int page_count = 0;
    public static int[] my_card = new int[25];
    public static int[] card_count = new int[33];
    private AudioSource _audioSource;
    private float UI_Alpha = 1;             //初始化时让UI显示
    public float alphaSpeed = 2f;          //渐隐渐显的速度
    private CanvasGroup canvasGroup;

    void Start()
    {
        card = GetComponentInChildren<Image>();
        //Debug.Log(card.name);
        Sprite sp = Resources.Load( card.name, typeof(Sprite)) as Sprite;
        card.sprite = sp;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        trance();
    }

    public void test()
    {
        if (count == 25)
        {
            Debug.Log("25");
        }
        else if (count < 25)
        {
            if (card_count[int.Parse(card.sprite.name)] < 2)
            {
                Text tex = GameObject.Find("T" + count.ToString()).GetComponent<Text>();
                my_card[count] = int.Parse(card.sprite.name);
                card_count[int.Parse(card.sprite.name)]++;
                tex.text = card.sprite.name;
                count++;

            }


            else
            {
                Sprite sp = Resources.Load("mycard", typeof(Sprite)) as Sprite;
                //Debug.Log(card.name);


                card_count[int.Parse(card.sprite.name)]++;
                card.sprite = sp;
            }
            if (card_count[int.Parse(card.sprite.name)] >= 2)
            {
                Sprite sp = Resources.Load("1", typeof(Sprite)) as Sprite;
                //Debug.Log(card.name);


                card_count[int.Parse(card.sprite.name)]++;
                card.sprite = sp;

            }
            //Debug.Log(count);
        }


    }

    public void add_card()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        test();
        Debug.Log(eventData.used.ToString());
        //StartCoroutine(MoveToPosition());
        //this.UI_FadeOut_Event();
        //Debug.Log("here");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Image image = GameObject.Find("Image").GetComponent<Image>();
        Debug.Log(eventData.used.ToString());
        image.sprite = card.sprite;

        //Debug.Log("here");
    }
    public void text2()
    {
        int i = 1;
        page_count++;
        if (page_count < 3)
        {


            while (i < 16)
            {

                card = GameObject.Find(i.ToString()).GetComponent<Image>();
                if (((IList)my_card).Contains(int.Parse(card.name) + 15 * page_count))
                {
                    Sprite sp = Resources.Load("mycard", typeof(Sprite)) as Sprite;
                    card.sprite = sp;
                    i++;
                }
                else
                {
                    Sprite sp = Resources.Load(((int.Parse(card.name) + 15 * page_count).ToString()), typeof(Sprite)) as Sprite;
                    if (sp == null)
                    {
                        sp = Resources.Load("mycard", typeof(Sprite)) as Sprite;
                        card.sprite = sp;
                    }
                    else
                    {
                        card.sprite = sp;
                    }

                    i++;
                }


                Debug.Log(page_count);
            }
        }
        else
        {
            page_count--;
        }

    }
    public void text3()
    {
        int i = 1;
        page_count--;
        if (page_count >= 0)
        {
            while (i < 16)
            {
                card = GameObject.Find(i.ToString()).GetComponent<Image>();
                if (((IList)my_card).Contains(int.Parse(card.name) + 15 * page_count))
                {
                    Sprite sp = Resources.Load("mycard", typeof(Sprite)) as Sprite;
                    card.sprite = sp;
                    i++;
                }
                else
                {
                    Sprite sp = Resources.Load(((int.Parse(card.name) + 15 * (page_count)).ToString()), typeof(Sprite)) as Sprite;
                    card.sprite = sp;
                    i++;
                }



                Debug.Log(page_count);
            }

        }
        else
        {
            page_count++;
        }
    }

    public void Write(string path)
    {
        int i = 0;
        FileStream fs = new FileStream(path, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        while (i < 25)
        {
            sw.WriteLine(my_card[i]);
            i++;
        }


        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();
    }

    public void write_card()
    {

        Write("card");

    }

    public void voice_card()
    {
        //添加 Audio Source 组件
        _audioSource = this.gameObject.AddComponent<AudioSource>();

        //加载 Audio Clip 对象
        AudioClip audioClip = Resources.Load<AudioClip>("bgm");

        //播放
        _audioSource.loop = true;
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    IEnumerator MoveToPosition()
    {
        while (card.transform.localPosition != new Vector3(-5, -3, 50))
        {
            card.transform.localPosition = Vector3.MoveTowards(card.transform.localPosition, new Vector3(-20, -3, 50), 50 * Time.deltaTime);
            yield return 0;
        }
    }

    public void trance()
    {
        if (canvasGroup == null)
        {
            return;
        }

        if (UI_Alpha != canvasGroup.alpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, UI_Alpha, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(UI_Alpha - canvasGroup.alpha) <= 0.01f)
            {
                canvasGroup.alpha = UI_Alpha;
            }
        }
    }

    public void UI_FadeIn_Event()
    {
        UI_Alpha = 1;
        canvasGroup.blocksRaycasts = true;      //可以和该对象交互
    }
    public void UI_FadeOut_Event()
    {
        UI_Alpha = 0;
        canvasGroup.blocksRaycasts = false;     //不可以和该对象交互
    }
}

