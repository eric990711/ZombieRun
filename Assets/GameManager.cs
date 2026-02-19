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
        // 광고 매니저가 있으면 광고 먼저 표시 → 광고 종료 후 ShowTryAgain() 호출
        // 광고 매니저가 없으면 바로 패널 표시
        if (AdManager.instance != null)
        {
            AdManager.instance.ShowInterstitialAd();
        }
        else
        {
            ShowTryAgain();
        }
    }

    public void ShowTryAgain()
    {
        tryagain.SetActive(true);
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
