using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameover;
    public SpawnObject sp;
    private static GameManager _instance = null;
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
}
