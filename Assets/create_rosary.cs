using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_rosary : MonoBehaviour
{
    public GameObject[] cross;
    float[] location = new float[3] { -1.5f, 0, 1.5f };

    // Start is called before the first frame update
    void Start()
    {
        crossmake();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void crossmake()
    {
        transform.position = new Vector3(15, 0, location[Random.Range(0, 3)]);
        GameObject go = Instantiate(cross[Random.Range(0,cross.Length)]);
        go.transform.position = transform.position;
        if (GameManager.instance.isGameover == false)
            Invoke("crossmake", Random.Range(0.5f, 1.0f));
    }
}
