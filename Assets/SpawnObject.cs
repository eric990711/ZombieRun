using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] obj;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        Spawn_();
    }

    // Update is called once per frame
    void Update()
    {
        if (go.transform.position.x <=20f && GameManager.instance.isGameover == false)
        {
            Spawn_();
        }
    }

   public void Spawn_()
    {
        go = Instantiate(obj[Random.Range(0,obj.Length)]);
        go.transform.position = new Vector3(40f,-0.5f,0);
    }
}
