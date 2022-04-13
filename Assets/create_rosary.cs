using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_rosary : MonoBehaviour
{
    public GameObject[] cross;
    float[] location = new float[3] { -1.5f, 0, 1.5f };
    public float starttime;
    public bool isResponing;
    float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGameover == false)
            starttime += Time.deltaTime;

        if (isResponing == false)
        {
            if (GameManager.instance.isGameStarted == true)
            {
                isResponing = true;
                GetStart();
            }
        }

    }

    public void GetStart()
    {
        if (GameManager.instance.isGameStarted == true)
            crossmake();
    }
    void crossmake()
    {
        transform.position = new Vector3(15, 0, location[Random.Range(0, 3)]);
        print(starttime);
        if (starttime <= 10)
        {
            GameObject go = Instantiate(cross[Random.Range(0, 2)]);
            go.transform.position = transform.position;
        }
        else if (starttime >= 10 && starttime <= 20)
        {
            GameObject go = Instantiate(cross[Random.Range(0, 4)]);
            go.transform.position = transform.position;
        }
        else if(starttime >= 20)
        {
            time = starttime;
            if ((int)time % 5 == 0)
            {
                print("Yes");
                GameObject go = Instantiate(cross[Random.Range(0, cross.Length)]);
                go.transform.position = transform.position;
            }
            else
            {
                GameObject go = Instantiate(cross[Random.Range(0, cross.Length - 1)]);
                go.transform.position = transform.position;
            }
        }

        if (GameManager.instance.isGameover == false)
            Invoke("crossmake", Random.Range(0.5f, 1.0f));
    }
}
