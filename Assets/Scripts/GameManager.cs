using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject[] NPC;

    public GameObject StartTimer;

    public GameObject pacman;
    public Ghost ghost1;
    public Ghost ghost2;
    public Ghost ghost3;
    public Ghost ghost4;

    public bool isSuperPacman = false;

    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    public List<GameObject> pacdotGos = new List<GameObject>();
    private int pacdotNum = 0;
    private int nowEat = 0;
    public int currentScore = 0;
    public Text remainText;
    public Text nowText;
    public Text scoreText;

    public bool Directionzuo = false;

    public bool Directionyou = false;

    public Transform DirectionzuoPoint;

    public Transform DirectionyouPoint;

    public GameObject Player;

    public GameObject gamestartPanel;
    public GameObject gameoverPrefab;
    public GameObject gamePanel;
    public GameObject winPrefab;
    public AudioClip startClip;

    public int HealthValue = 3;

    public GameObject ReBoomPosition;

    public bool IsOnEatSuperPacdot = false;//是否吃掉超级豆子

    public GameObject EatSuperPacdotText;

    public Text OnEatSuperPacdotNumberText;

    public float OnEatSuperPacdotTextNumber = 11f;

    public int OnEatSuperPacdotTextNumberConvertInt;

    public float Starttimer;

    public GameTimeText Timer;//计时器

    public bool OnStartGame = true;

    public GameObject winObject;

    public int dotNumber = 0;

    private void Awake()
    {
        winObject.SetActive(false);
        Timer.enabled = false;
        instance = this;
        int tempCount = rawIndex.Count;
        for (int i = 0; i < tempCount; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }

        foreach (Transform t in GameObject.Find("Maze").transform)
        {
            pacdotGos.Add(t.gameObject);
        }
        pacdotNum = GameObject.Find("Maze").transform.childCount;
    }

    private void Start()
    {
        OnStartGame = true;
    }

    private void Update()
    {
        if (dotNumber == 181)
        {
            StartCoroutine(WinDelay());
        }

        IEnumerator WinDelay()
        {
            winObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }

        //DontDestroy dontDestroy = GameObject.Find("DontDestroy").GetComponent<DontDestroy>();
        //if (dontDestroy.MaxScore < currentScore)
        //{
        //    dontDestroy.MaxScore = currentScore;
        //}

        if (Directionzuo)
        {
            Vector3 P = DirectionyouPoint.position;
            Player.transform.position = P;
        }

        if (Directionyou)
        {
            Vector3 P = DirectionzuoPoint.position;
            Player.transform.position = P;
        }

        if (nowEat == pacdotNum && pacman.GetComponent<PacStudent>().enabled != false)
        {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            ChangeState(false);
        }
        if (nowEat == pacdotNum)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (gamePanel.activeInHierarchy)
        {
            remainText.text = "Remain:" + (pacdotNum - nowEat);
            nowText.text = "Eaten:" + (nowEat);
            scoreText.text = currentScore.ToString();
        }
        if (IsOnEatSuperPacdot == true)
        {
            EatSuperPacdotText.SetActive(true);
            OnEatSuperPacdotTextNumber -= Time.deltaTime;
            OnEatSuperPacdotTextNumberConvertInt = (int)OnEatSuperPacdotTextNumber;
            OnEatSuperPacdotNumberText.text = OnEatSuperPacdotTextNumberConvertInt.ToString();
            if (OnEatSuperPacdotTextNumberConvertInt == 0)
            {
                IsOnEatSuperPacdot = false;
                EatSuperPacdotText.SetActive(false);
            }
        }

        if (IsOnEatSuperPacdot == false)
        {
            EatSuperPacdotText.SetActive(false);
        }

        if (OnStartGame)
        {
            StartTimer.SetActive(true);
            ghost1.enabled = false;
            ghost2.enabled = false;
            ghost3.enabled = false;
            ghost4.enabled = false;
            foreach (GameObject item in NPC)
            {
                item.GetComponent<Animator>().enabled = false;
            }
            pacman.GetComponent<PacStudent>().enabled = false;
            Starttimer -= Time.deltaTime;
            if (Starttimer <= 0f)
            {
                StartTimer.SetActive(false);
                foreach (GameObject item in NPC)
                {
                    item.GetComponent<Animator>().enabled = true;
                }
                ghost1.enabled = true;
                ghost2.enabled = true;
                ghost3.enabled = true;
                ghost4.enabled = true;
                pacman.GetComponent<PacStudent>().enabled = true;
                Timer.enabled = true;
                OnStartButton();
                OnStartGame = false;
            }
        }
    }

    public void OnStartButton()
    {
        ChangeState(true);
        Invoke("CreateSuperpacdot", 5f);
        AudioSource.PlayClipAtPoint(startClip, new Vector3(0, 0, -5));
        gamestartPanel.SetActive(false);
        gamePanel.SetActive(true);
        Invoke("PlayAudio", 5f);
    }

    public void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void EatDot(GameObject go)
    {
        nowEat++;
        currentScore += 100;
        pacdotGos.Remove(go);
    }

    public void OnEatSuperPacdot()
    {
        isSuperPacman = true;
        IsOnEatSuperPacdot = true;
        currentScore += 200;
        Invoke("CreateSuperpacdot", 20f);
        ChangeEnemy();
        ghost1.GetComponent<SpriteRenderer>().enabled = false;
        ghost2.GetComponent<SpriteRenderer>().enabled = false;
        ghost3.GetComponent<SpriteRenderer>().enabled = false;
        ghost4.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(RecoverEnemy());
    }

    IEnumerator RecoverEnemy()
    {
        yield return new WaitForSeconds(10f);
        ghost1.GetComponent<SpriteRenderer>().enabled = true;
        ghost2.GetComponent<SpriteRenderer>().enabled = true;
        ghost3.GetComponent<SpriteRenderer>().enabled = true;
        ghost4.GetComponent<SpriteRenderer>().enabled = true;
        ShowEnemy();
        UnChangeEnemy();
        isSuperPacman = false;
    }

    private void CreateSuperpacdot()
    {
        if (pacdotGos.Count < 5)
        {
            return;
        }

        int tempIndex = Random.Range(0, pacdotGos.Count);
        pacdotGos[tempIndex].transform.localScale = new Vector3(3, 3, 3);
        pacdotGos[tempIndex].GetComponent<DotObject>().isBig = true;
    }


    private void ChangeEnemy()//改变敌人颜色
    {
        ghost1.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.7f, 0.7f, 0.7f);
        ghost2.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        ghost3.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        ghost4.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }

    private void UnChangeEnemy()//改变敌人颜色
    {
        ghost1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        ghost2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        ghost3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        ghost4.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    public void FreezeEnemy(GameObject go)
    {
        go.GetComponent<Ghost>().enabled = false;//关闭敌人功能脚本
    }

    public void OnKillEnemy(GameObject enemyObj)
    {
        enemyObj.SetActive(false);//关闭销毁敌人
        if (ghost1.gameObject.activeInHierarchy == false)
        {
            StartCoroutine(BirthEnemy(ghost1.gameObject));
        }
        if (ghost2.gameObject.activeInHierarchy == false)
        {
            StartCoroutine(BirthEnemy(ghost2.gameObject));
        }
        if (ghost3.gameObject.activeInHierarchy == false)
        {
            StartCoroutine(BirthEnemy(ghost3.gameObject));
        }
        if (ghost4.gameObject.activeInHierarchy == false)
        {
            StartCoroutine(BirthEnemy(ghost4.gameObject));
        }
    }

    IEnumerator BirthEnemy(GameObject goman)
    {
        yield return new WaitForSeconds(10f);
        goman.SetActive(true);
    }

    public void ShowEnemy()
    {
        ghost1.enabled = true;
        ghost2.enabled = true;
        ghost3.enabled = true;
        ghost4.enabled = true;
    }

    private void ChangeState(bool state)
    {
        pacman.GetComponent<PacStudent>().enabled = state;
        ghost1.enabled = state;
        ghost2.enabled = state;
        ghost3.enabled = state;
        ghost4.enabled = state;
    }
}
