using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plowmovement : MonoBehaviour
{
    public GameObject plow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Attack")
        {
            plow.GetComponent<Animation>().Play();
        }
    }
}
