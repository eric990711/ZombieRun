using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool ison;
    public float speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if(GameManager.instance.isGameover == false)
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (gameObject.transform.position.x <= -20)
            Destroy(gameObject);
    }
}
