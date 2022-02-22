using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cross_move : MonoBehaviour
{
    public float speed;
    void Start()
    {
        //transform.position = new Vector3(14.21f, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGameover == false)
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < -10)
            Destroy(gameObject);
    }
}
