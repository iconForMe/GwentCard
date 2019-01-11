using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Script.common;
using UnityEngine.EventSystems;
using System.Threading;


public class MainGame : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{

    Gamer player = new Gamer();
    Gamer enemy = new Gamer();
    
    public Image card;
    public Image local;

    public static string[] playercard_near = new string[10];
    public static string[] AIcard_near = new string[10];
    public static string[] playercard_arch = new string[10];
    public static string[] AIcard_arch = new string[10];
    public static string[] playercard_wall = new string[10];
    public static string[] AIcard_wall = new string[10];

    public static int[] Player_card_battle = new int[10];
    public static int[] Ai_card_battle = new int[10];

    public static int Player_card_count = 0;
    public static int AI_card_count = 0;

    public static int playercardcount_near = 0;
    public static int AIcardcount_near = 0;
    public static int playercardcount_arch = 0;
    public static int AIcardcount_arch = 0;
    public static int playercardcount_wall = 0;
    public static int AIcardcount_wall = 0;
    public Image ailocal;

    public  Image [] AI_card = new Image[10];
    public Image [] AI_battle_card = new Image[10];
    

    public static string[] playerCardBox = new string[25];
    public static string[] enemyCardBox = new string[25];

    public static int[] playerCardBox2 = new int[25];
    public static int[] enemyCardBox2 = new int[25];

    public static int[] playerCardBox3 = new int[25];
    public static int[] enemyCardBox3 = new int[25];

    public static int[] playerHandCard = new int[10];
    public static int[] enemyHandCard = new int[10];

    System.Random ra = new System.Random();

    public bool Cardlock = true;
    public bool isYourRound = false;
    public bool isEnemyRound = false;
    public bool isGiveUp = false;
    public bool isPutCardInRound = false;

    CardClassfy CC = new CardClassfy();

    GameAccount_ GA = new GameAccount_();
    public Text playerstrength;
    public Text AIstrength;
    public Text playerhp;
    public Text AIhp;

    public int[] player_all_card = new int[25];
    public int[] AI_all_card = new int[25];

    public AudioSource play;
    public Image round;

    public bool fade = true;
    // AutoResetEvent detailCollectedEvent = new AutoResetEvent(false);

    // Use this for initialization
    void Start()
    {

        
        card = GetComponentInChildren<Image>();
        //初始化双方血量
        player.HP = 2;
        enemy.HP = 2;

        //初始化双方力量
        enemy.strengthNum = 0;
        player.strengthNum = 0;

        GA.ReadCard(player_all_card);
        GA.ReadCard(AI_all_card);
        //读取配置文件 生成双方牌组
        //playerCardBox = File.ReadAllLines(@"testPlayCardBox.txt", Encoding.UTF8);
        //enemyCardBox = File.ReadAllLines(@"enemyCardBox.txt", Encoding.UTF8);

        //将牌组乱序 洗牌
        player_all_card = ShufflePlayer(player_all_card);
        AI_all_card = ShuffleAI(AI_all_card);

        //抽取手牌
        for(int i = 0; i < 10; i++)
        {
           playerHandCard[i] = player_all_card[i];
           enemyHandCard[i] = AI_all_card[i];
        }
        for (int i = 0; i < 10; i++)
        {
            CardImage(playerHandCard[i], i + 1);
        }

        for (int i = 0; i < 10; i++)
        {
            AI_card[i] = GameObject.Find("Image" + i.ToString()).GetComponent<Image>();
            Sprite sp = Resources.Load(enemyHandCard[i].ToString(), typeof(Sprite)) as Sprite;
            AI_card[i].sprite = sp;

        }

        //渲染手牌
        /*for (int i = 0; i < 3; i++)
        {
            CardImage(1, i + 1);
        }
        for (int i = 3; i < 7; i++)
        {
            CardImage(51, i + 1);
        }
        for (int i = 7; i < 10; i++)
        {
            CardImage(101, i + 1);
        }

        for (int i = 0; i < 3; i++)
        {
            AI_card[i] = GameObject.Find("Image" + i.ToString()).GetComponent<Image>();
            Sprite sp = Resources.Load("1", typeof(Sprite)) as Sprite;
            AI_card[i].sprite = sp;
        }
        for (int i = 3; i < 8; i++)
        {
            AI_card[i] = GameObject.Find("Image" + i.ToString()).GetComponent<Image>();
            Sprite sp = Resources.Load("51", typeof(Sprite)) as Sprite;
            AI_card[i].sprite = sp;
        }
        for (int i = 8; i < 10; i++)
        {
            AI_card[i] = GameObject.Find("Image" + i.ToString()).GetComponent<Image>();
            Sprite sp = Resources.Load("101", typeof(Sprite)) as Sprite;
            AI_card[i].sprite = sp;
        }*/
        //决定出牌顺序
        //if ((ra.Next(0, 2)) == 1)
        //{
        //    isYourRound = true;
        //}
        //else
        //{
        //    isEnemyRound = true;
        //}


        isYourRound = true;
        //开始对战
        //StartCoroutine(StartBattle());
        playerstrength = GameObject.Find("player_strength").GetComponent<Text>();
        AIstrength = GameObject.Find("Ai_strength").GetComponent<Text>();
        playerhp = GameObject.Find("player_life").GetComponent<Text>();
        AIhp = GameObject.Find("AI_life").GetComponent<Text>();
        play = GameObject.Find("playwhenpointeon").GetComponent<AudioSource>();
        round = GameObject.Find("round1").GetComponent<Image>();
        AIstrength.text = "2";
        AIstrength.text = "2";
    }

    // Update is called once per frame
    void Update()
    {
        
        if(AI_card_count == 10 && Player_card_count == 10)
        {
            if(int.Parse(GameObject.Find("player_life").GetComponent<Text>().text) > int.Parse(GameObject.Find("AI_life").GetComponent<Text>().text))
            {
                StartCoroutine(Win());
            }
            else if(int.Parse(GameObject.Find("player_life").GetComponent<Text>().text) < int.Parse(GameObject.Find("AI_life").GetComponent<Text>().text))
            {
                StartCoroutine(lose());
            }
        }
        //Debug.Log(GA.StrengthAcount(Player_card_battle).ToString());
        //Debug.Log(GA.StrengthAcount(Ai_card_battle).ToString());
        playerstrength.text = GA.StrengthAcount(Player_card_battle).ToString();
        AIstrength.text = GA.StrengthAcount(Ai_card_battle).ToString();
        //playerstrength.text = "98";
        //AIstrength.text = "66";

        //if (isEnemyRound)
        //{

        //}
    }


    void PutCard(GameObject cardObj, int mode = 0)
    {


    }

    //洗牌
    public int[] ShuffleAI(int[] arr)
    {
        int[] arr2 = new int[arr.Length];
        int k = arr.Length;
        for (int i = 0; i < arr.Length; i++)
        {
            int temp = new System.Random(5).Next(0, k);
            arr2[i] = arr[temp];
            //arr[temp]后面的数向前移一位
            for (int j = temp; j < arr.Length - 1; j++)
            {
                arr[j] = arr[j + 1];
            }
            k--;
        }
        return arr2;
    }
    public int[] ShufflePlayer(int[] arr)
    {
        int[] arr2 = new int[arr.Length];
        int k = arr.Length;
        for (int i = 0; i < arr.Length; i++)
        {
            int temp = new System.Random(10).Next(0, k);
            arr2[i] = arr[temp];
            //arr[temp]后面的数向前移一位
            for (int j = temp; j < arr.Length - 1; j++)
            {
                arr[j] = arr[j + 1];
            }
            k--;
        }
        return arr2;
    }

    //绘制卡牌
    public void CardImage(int cardId, int panelId)
    {
        Image card = GameObject.Find("Panel" + panelId.ToString()).GetComponent<Image>();
        Sprite sp = Resources.Load(cardId.ToString(), typeof(Sprite)) as Sprite;
        card.sprite = sp;

    }

    //正式战斗 玩家
    public void PutPlayerCard(bool isYourRound)
    {
        //TODO 
        if (isYourRound)
        {

            if (!isGiveUp)
            {   //TODO 出牌之后 手牌减少
                //如果不点击 则等待

                // yield return new WaitUntil(() => Input.GetMouseButton(0));
                Image card = GetComponentInChildren<Image>();
                CC.CardClassfied(int.Parse(card.sprite.name));//不同的牌打的位置
                player.handCardNum--;

            }
            else
            {
                isYourRound = false;
            }
            isYourRound = false;
            isEnemyRound = true;

        }
    }
    //AI出牌
    public void AI(bool isEnemyRound)
    {


       

        if (true)
        {
            //Debug.Log(AI_card_count);
            if(int.Parse(AI_card[AI_card_count].sprite.name) < 50)
            {
                ailocal = GameObject.Find("near2").GetComponent<Image>();
                AIcard_near[AIcardcount_near] = AI_card[AI_card_count].name;
                AIcardcount_near++;
               
                if (AIcardcount_near == 1)
                {

                    AI_card[AI_card_count].transform.localPosition = Vector3.MoveTowards(AI_card[AI_card_count].transform.localPosition, ailocal.transform.localPosition, 1000000000 * Time.deltaTime);
                    Ai_card_battle[AI_card_count] = int.Parse(AI_card[AI_card_count].sprite.name);
                    AI_card_count++;
                    isEnemyRound = false;
                    isYourRound = true;
                }
                else
                {
                    for (int j = 0; j < AIcardcount_near; j++)
                    {
                        if (j < AIcardcount_near - 1)
                        {
                            Image imgai = GameObject.Find(AIcard_near[j]).GetComponent<Image>();
                            //Image lastimg = GameObject.Find(playercard_near[j-1]).GetComponent<Image>();
                            float x = imgai.transform.localPosition.x;

                            imgai.transform.localPosition = Vector3.MoveTowards(imgai.transform.localPosition, new Vector3(x - 40, ailocal.transform.localPosition.y, imgai.transform.localPosition.z), 1000000000 * Time.deltaTime);
                            isEnemyRound = false;
                            isYourRound = true;
                        }
                        else
                        {
                            Image imgai = GameObject.Find(AIcard_near[j]).GetComponent<Image>();
                            Image lastimgai = GameObject.Find(AIcard_near[j - 1]).GetComponent<Image>();
                            float x = imgai.transform.localPosition.x;

                            imgai.transform.localPosition = Vector3.MoveTowards(imgai.transform.localPosition, new Vector3(lastimgai.transform.localPosition.x + 85, ailocal.transform.localPosition.y, imgai.transform.localPosition.z), 10000000000 * Time.deltaTime);
                            Ai_card_battle[AI_card_count] = int.Parse(AI_card[AI_card_count].sprite.name);
                            AI_card_count++;
                            isEnemyRound = false;
                            isYourRound = true;


                        }

                    }
                    //AI_card_count++;
                }
            }

            else if (int.Parse(AI_card[AI_card_count].sprite.name) > 50 && int.Parse(AI_card[AI_card_count].sprite.name) < 100)
            {
                ailocal = GameObject.Find("arch2").GetComponent<Image>();
                AIcard_arch[AIcardcount_arch] = AI_card[AI_card_count].name;
                AIcardcount_arch++;
                
                if (AIcardcount_arch == 1)
                {

                    AI_card[AI_card_count].transform.localPosition = Vector3.MoveTowards(AI_card[AI_card_count].transform.localPosition, ailocal.transform.localPosition, 1000000000 * Time.deltaTime);
                    Ai_card_battle[AI_card_count] = int.Parse(AI_card[AI_card_count].sprite.name);
                    AI_card_count++;
                    isEnemyRound = false;
                    isYourRound = true;
                }
                else
                {
                    for (int j = 0; j < AIcardcount_arch; j++)
                    {
                        if (j < AIcardcount_arch - 1)
                        {
                            Image imgai = GameObject.Find(AIcard_arch[j]).GetComponent<Image>();
                            //Image lastimg = GameObject.Find(playercard_near[j-1]).GetComponent<Image>();
                            float x = imgai.transform.localPosition.x;

                            imgai.transform.localPosition = Vector3.MoveTowards(imgai.transform.localPosition, new Vector3(x - 40, ailocal.transform.localPosition.y, imgai.transform.localPosition.z), 1000000000 * Time.deltaTime);
                            isEnemyRound = false;
                            isYourRound = true;
                        }
                        else
                        {
                            Image imgai = GameObject.Find(AIcard_arch[j]).GetComponent<Image>();
                            Image lastimgai = GameObject.Find(AIcard_arch[j - 1]).GetComponent<Image>();
                            float x = imgai.transform.localPosition.x;

                            imgai.transform.localPosition = Vector3.MoveTowards(imgai.transform.localPosition, new Vector3(lastimgai.transform.localPosition.x + 85, ailocal.transform.localPosition.y, imgai.transform.localPosition.z), 10000000000 * Time.deltaTime);
                            Ai_card_battle[AI_card_count] = int.Parse(AI_card[AI_card_count].sprite.name);
                            AI_card_count++;
                            isEnemyRound = false;
                            isYourRound = true;


                        }

                    }
                    //AI_card_count++;
                }
            }
            else if (int.Parse(AI_card[AI_card_count].sprite.name) > 100 && int.Parse(AI_card[AI_card_count].sprite.name) < 150)
            {
                ailocal = GameObject.Find("wall2").GetComponent<Image>();
                AIcard_wall[AIcardcount_wall] = AI_card[AI_card_count].name;
                AIcardcount_wall++;
                
                if (AIcardcount_wall == 1)
                {

                    AI_card[AI_card_count].transform.localPosition = Vector3.MoveTowards(AI_card[AI_card_count].transform.localPosition, ailocal.transform.localPosition, 1000000000 * Time.deltaTime);
                    Ai_card_battle[AI_card_count] = int.Parse(AI_card[AI_card_count].sprite.name);
                    AI_card_count++;
                    isEnemyRound = false;
                    isYourRound = true;
                }
                else
                {
                    for (int j = 0; j < AIcardcount_wall; j++)
                    {
                        if (j < AIcardcount_wall - 1)
                        {
                            Image imgai = GameObject.Find(AIcard_wall[j]).GetComponent<Image>();
                            //Image lastimg = GameObject.Find(playercard_near[j-1]).GetComponent<Image>();
                            float x = imgai.transform.localPosition.x;

                            imgai.transform.localPosition = Vector3.MoveTowards(imgai.transform.localPosition, new Vector3(x - 40, ailocal.transform.localPosition.y, imgai.transform.localPosition.z), 1000000000 * Time.deltaTime);
                            isEnemyRound = false;
                            isYourRound = true;
                        }
                        else
                        {
                            Image imgai = GameObject.Find(AIcard_wall[j]).GetComponent<Image>();
                            Image lastimgai = GameObject.Find(AIcard_wall[j - 1]).GetComponent<Image>();
                            float x = imgai.transform.localPosition.x;

                            imgai.transform.localPosition = Vector3.MoveTowards(imgai.transform.localPosition, new Vector3(lastimgai.transform.localPosition.x + 85, ailocal.transform.localPosition.y, imgai.transform.localPosition.z), 10000000000 * Time.deltaTime);
                            Ai_card_battle[AI_card_count] = int.Parse(AI_card[AI_card_count].sprite.name);
                            AI_card_count++;
                            isEnemyRound = false;
                            isYourRound = true;



                        }

                    }
                    //AI_card_count++;
                }
            }

            
        }
       
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
        Image button = GetComponent<Image>();
        if (button.name == "giveUp")
        {

            RoundAccount(int.Parse(GameObject.Find("player_strength").GetComponent<Text>().text), int.Parse(GameObject.Find("Ai_strength").GetComponent<Text>().text), int.Parse(GameObject.Find("player_life").GetComponent<Text>().text), int.Parse(GameObject.Find("AI_life").GetComponent<Text>().text));
            GameObject.Find("player_strength").GetComponent<Text>().text = "0";
            GameObject.Find("Ai_strength").GetComponent<Text>().text = "0";
            for (int i = 0;i < 10; i++)
            {
                Player_card_battle[i] = 0;
                Ai_card_battle[i] = 0;
            }
            for (int i = 0; i < 10; i++)
            {
                if(playercardcount_arch == 0)
                {

                }
                else
                {
                    Fadecard(playercard_arch[i]);
                    playercard_arch[i] = null;
                    playercardcount_arch--;
                }
               
               


            }
            for (int i = 0; i < 10; i++)
            {
                if (AIcardcount_arch == 0)
                {

                }
                else
                {
                    Fadecard(AIcard_arch[i]);
                    AIcard_arch[i] = null;
                    AIcardcount_arch--;
                }




            }
            for (int i = 0; i < 10; i++)
            {
                if (playercardcount_near == 0)
                {

                }
                else
                {
                    Fadecard(playercard_near[i]);
                    playercard_near[i] = null;
                    playercardcount_near--;
                }




            }
            for (int i = 0; i < 10; i++)
            {
                if (AIcardcount_near == 0)
                {

                }
                else
                {
                    Fadecard(AIcard_near[i]);
                    AIcard_near[i] = null;
                    AIcardcount_near--;
                }




            }
            for (int i = 0; i < 10; i++)
            {
                if (playercardcount_wall == 0)
                {

                }
                else
                {
                    Fadecard(playercard_wall[i]);
                    playercard_wall[i] = null;
                    playercardcount_wall--;
                }




            }
            for (int i = 0; i < 10; i++)
            {
                if (AIcardcount_wall == 0)
                {

                }
                else
                {
                    Fadecard(AIcard_wall[i]);
                    AIcard_wall[i] = null;
                    AIcardcount_wall--;
                }




            }
        }
        else
        {
            GameObject.Find("playcard").GetComponent<AudioSource>().Play();

            StartCoroutine(MoveToPosition());
            StartCoroutine(WaitTowSecond());
            Cardlock = true;
        }
       
        //StartCoroutine(Strength());
        //StartBattle();
       
        //AI(isEnemyRound);
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
    IEnumerator Win()
    {
        Image round = GameObject.Find("win").GetComponent<Image>();
        StartCoroutine(Fade_exit("gameover"));
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        GameObject.Find("winmusic").GetComponent<AudioSource>().Play();

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
    IEnumerator lose()
    {
        Image round = GameObject.Find("lose").GetComponent<Image>();
        StartCoroutine(Fade_exit("gameover"));
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        GameObject.Find("losemusic").GetComponent<AudioSource>().Play();
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
    IEnumerator Fade_exit(string name)
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
    IEnumerator Strength()
    {
        yield return new WaitForSeconds(1);
        //playerstrength.text = GA.StrengthAcount(Player_card_battle).ToString();
        //AIstrength.text = GA.StrengthAcount(Ai_card_battle).ToString();
        playerstrength.text = "playerstrength.text";
        AIstrength.text = "AIstrength.text";
    }
    IEnumerator WaitTowSecond()
    {
        yield return new WaitForSecondsRealtime(1);
        AI(isEnemyRound);
        yield return new WaitForSeconds(0.5F);
        StartCoroutine(Fade());
    }
    IEnumerator Fadeatstart()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = round.color;
            c.a = f;
            round.color = c;
            yield return null;//下一帧继续执行for循环
            //yield return new WaitForSeconds(0.1f);//0.1秒后继续执行for循环
        }
    }
    IEnumerator Fade()
    {
        round = GameObject.Find("round1").GetComponent<Image>();
        float i = 1f;
        for (int f = 0; f < 101; f++)
        {
            Color c = round.color;
            
                c = round.color;
            c.a = i;
                round.color = c;
            if(i == 0)
            {

            }
            else
            {
                i -= 0.01f;
            }
            
            yield return null;
            yield return new WaitForSeconds(0.0001f);
        }
    }
    public void  Fadecard(string name)
    {
        round = GameObject.Find(name).GetComponent<Image>();
        float i = 1f;
        for (int f = 0; f < 101; f++)
        {
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

        }
    }
    IEnumerator Fade_now(string name)
    {
        round = GameObject.Find(name).GetComponent<Image>();
        float i = 1f;
        for (int f = 0; f < 101; f++)
        {
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
    IEnumerator MoveToPosition()
    {
        yield return new WaitUntil(() => Cardlock == true);

        if(int.Parse(card.sprite.name) < 50)
        {
            
            local = GameObject.Find("near").GetComponent<Image>();
            Color c = local.color;
            c.a = 0;
            local.color = c;
            if (true)
            {
                playercard_near[playercardcount_near] = card.name;
                playercardcount_near++;
                Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                Player_card_count++;
                if (playercardcount_near == 1)
                {
                    
                    while (card.transform.localPosition != local.transform.localPosition)
                    {
                        card.transform.localPosition = Vector3.MoveTowards(card.transform.localPosition,local.transform.localPosition, 1000 * Time.deltaTime);
                        Cardlock = false;
                        //Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                       // Player_card_count++;
                        yield return 0;
                    }
                    
                }
                else
                {
                   
                    for (int j = 0; j < playercardcount_near; j++)
                    {
                        if(j < playercardcount_near- 1)
                        {
                            Image img = GameObject.Find(playercard_near[j]).GetComponent<Image>();
                            //Image lastimg = GameObject.Find(playercard_near[j-1]).GetComponent<Image>();
                            float x = img.transform.localPosition.x;
                            
                                img.transform.localPosition = Vector3.MoveTowards(img.transform.localPosition, new Vector3(x - 40, local.transform.localPosition.y, img.transform.localPosition.z), 100000 * Time.deltaTime);
                            
                        }
                        else
                        {
                            Image img = GameObject.Find(playercard_near[j]).GetComponent<Image>();
                            Image lastimg = GameObject.Find(playercard_near[j-1]).GetComponent<Image>();
                            float x = img.transform.localPosition.x;
                           
                            while (img.transform.localPosition != new Vector3(lastimg.transform.localPosition.x + 85, local.transform.localPosition.y, img.transform.localPosition.z))
                            {
                                img.transform.localPosition = Vector3.MoveTowards(img.transform.localPosition, new Vector3(lastimg.transform.localPosition.x + 85, local.transform.localPosition.y, img.transform.localPosition.z), 1000 * Time.deltaTime);
                                Cardlock = false;
                                //Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                                //Player_card_count++;
                                yield return 0;
                            }
                        }
                        
                    }
                }
               
            }
        }
        if(int.Parse(card.sprite.name) > 50  && int.Parse(card.sprite.name) < 100)
        {
            local = GameObject.Find("arch").GetComponent<Image>();
            Color c = local.color;
            c.a = 0;
            local.color = c;
            if (true)
            {
                playercard_arch[playercardcount_arch] = card.name;
                playercardcount_arch++;
                if (playercardcount_arch == 1)
                {
                    Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                    Player_card_count++;
                    while (card.transform.localPosition != local.transform.localPosition)
                    {
                        card.transform.localPosition = Vector3.MoveTowards(card.transform.localPosition, local.transform.localPosition, 1000 * Time.deltaTime);
                        Cardlock = false;
                        //Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                        //Player_card_count++;
                        yield return 0;
                    }
                }
                else
                {
                    Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                    Player_card_count++;
                    for (int j = 0; j < playercardcount_arch; j++)
                    {
                        if (j < playercardcount_arch - 1)
                        {
                            Image img = GameObject.Find(playercard_arch[j]).GetComponent<Image>();
                            //Image lastimg = GameObject.Find(playercard_near[j-1]).GetComponent<Image>();
                            float x = img.transform.localPosition.x;

                            img.transform.localPosition = Vector3.MoveTowards(img.transform.localPosition, new Vector3(x - 40, local.transform.localPosition.y, img.transform.localPosition.z), 100000 * Time.deltaTime);

                        }
                        else
                        {
                            Image img = GameObject.Find(playercard_arch[j]).GetComponent<Image>();
                            Image lastimg = GameObject.Find(playercard_arch[j - 1]).GetComponent<Image>();
                            float x = img.transform.localPosition.x;
                            while (img.transform.localPosition != new Vector3(lastimg.transform.localPosition.x + 85, local.transform.localPosition.y, img.transform.localPosition.z))
                            {
                                img.transform.localPosition = Vector3.MoveTowards(img.transform.localPosition, new Vector3(lastimg.transform.localPosition.x + 85, local.transform.localPosition.y, img.transform.localPosition.z), 1000 * Time.deltaTime);
                                Cardlock = false;
                                //Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                                //Player_card_count++;
                                yield return 0;
                            }
                        }

                    }
                }

            }
        }
        if(int.Parse(card.sprite.name) > 100 && int.Parse(card.sprite.name) < 150)
        {
            local = GameObject.Find("wall").GetComponent<Image>();
            Color c = local.color;
            c.a = 0;
            local.color = c;
            if (true)
            {
                playercard_wall[playercardcount_wall] = card.name;
                playercardcount_wall++;
                if (playercardcount_wall == 1)
                {
                    Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                    Player_card_count++;
                    while (card.transform.localPosition != local.transform.localPosition)
                    {
                        card.transform.localPosition = Vector3.MoveTowards(card.transform.localPosition, local.transform.localPosition, 1000 * Time.deltaTime);
                        Cardlock = false;
                        //Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                        //Player_card_count++;
                        yield return 0;
                    }
                }
                else
                {
                    Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                    Player_card_count++;
                    for (int j = 0; j < playercardcount_wall; j++)
                    {
                        if (j < playercardcount_wall - 1)
                        {
                            Image img = GameObject.Find(playercard_wall[j]).GetComponent<Image>();
                            //Image lastimg = GameObject.Find(playercard_near[j-1]).GetComponent<Image>();
                            float x = img.transform.localPosition.x;

                            img.transform.localPosition = Vector3.MoveTowards(img.transform.localPosition, new Vector3(x - 40, local.transform.localPosition.y, img.transform.localPosition.z), 100000 * Time.deltaTime);

                        }
                        else
                        {
                            Image img = GameObject.Find(playercard_wall[j]).GetComponent<Image>();
                            Image lastimg = GameObject.Find(playercard_wall[j - 1]).GetComponent<Image>();
                            float x = img.transform.localPosition.x;
                            while (img.transform.localPosition != new Vector3(lastimg.transform.localPosition.x + 85, local.transform.localPosition.y, img.transform.localPosition.z))
                            {
                                img.transform.localPosition = Vector3.MoveTowards(img.transform.localPosition, new Vector3(lastimg.transform.localPosition.x + 85, local.transform.localPosition.y, img.transform.localPosition.z), 1000 * Time.deltaTime);
                                Cardlock = false;
                                //Player_card_battle[Player_card_count] = int.Parse(card.sprite.name);
                                //Player_card_count++;
                                yield return 0;
                            }
                        }

                    }
                }

            }
        }
       // 
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        play.Play();
    }
 

    //正式战斗
    public void StartBattle()
    {
        if(player.HP != 0 && enemy.HP != 0)
        {
            if (isYourRound)
            {   //TODO 将点击的卡牌绘制在相应位置
                //点击触发
                //绘制isYourRound panel

                if (!isGiveUp)
                {   //TODO 出牌之后 手牌减少
                    //如果不点击 则等待

                    //yield return new WaitUntil(() => Input.GetMouseButton(0));
                    Image card = GetComponentInChildren<Image>();
                    CC.CardClassfied(int.Parse(card.sprite.name));//不同的牌打的位置
                   // player.handCardNum--;

                }
                else
                {
                    isYourRound = false;
                }
               

            }
            isYourRound = false;
            isEnemyRound = true;
            //else
            //{
            //    //TODO  AI打牌逻辑
            //    Image test = GameObject.Find("near2").GetComponent<Image>();
            //    Sprite sp = Resources.Load("1", typeof(Sprite))as Sprite;
            //    test.sprite = sp;

            //    isEnemyRound = false;
            //    isYourRound = true;
            //   // enemy.handCardNum--;

            //}

            if (isGiveUp || (player.handCardNum == 0 && enemy.handCardNum == 0))
            {
                //TODO 计算双方的力量点数
                GA.RoundAccount(player.strengthNum, enemy.strengthNum, player.HP, enemy.HP);
                isGiveUp = false;
                //TODO 出现回合LOGO
            }

        }
        GA.FinalAccount(player.HP, enemy.HP);
    }


}
