using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameover;
    public SpawnObject sp;
    public int score;
    private static GameManager _instance = null;
    public GameObject newgame;
    public GameObject Help;
    public GameObject HelpImage;
    public GameObject tryagain;
    public GameObject createrosary;
    public GameObject zombiemove;
    public GameObject spawnfloor;
    public GameObject BasicImage;
    public bool isGameStarted;
    public Text GameScore;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        if (instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void INIT()
    {
        GameScore.text = "" + score;
    }

    public void isGameStart()
    {
        newgame.SetActive(false);
        BasicImage.SetActive(false);
    }

    public void isHelpOver()
    {
        isGameStarted = true;
        Help.SetActive(false);
        HelpImage.SetActive(false);
        INIT();
    }

    public void isGameRestart()
    {
        tryagain.SetActive(false);
        isGameStarted = true;
        newgame.SetActive(false);
        INIT();
    }

    public void GetScore()
    {
        GameScore.text = "" + score;
    }

    public void Gameover()
    {
        tryagain.SetActive(true);
        //StartCoroutine(_Gameover());
    }

    public void ReStart()
    {
        Application.LoadLevel(0);
    }

    IEnumerator _Gameover()
    {
        createrosary.GetComponent<create_rosary>().starttime = 0;
        yield return new WaitForSeconds(5f);
        tryagain.SetActive(true);
        Application.LoadLevel(0);
    }
}
