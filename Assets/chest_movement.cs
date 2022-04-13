using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chest_movement : MonoBehaviour
{
    public GameObject chests;
    public Image image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            StartCoroutine(FadeIn(1));
            StartCoroutine(FadeOut(1));
        }
        else if(other.tag == "Attack")
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeIn(float time)
    {
        Color color = image.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / time;
            image.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float time)
    {
        Color color = image.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            image.color = color;
            yield return null;
        }
    }
}
