using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEncounter : MonoBehaviour
{
    bool isMove;
    float val = 1.5f;
    public bool isLeftmove;
    public bool isRightmove;
    Vector3 v1, v2;
    public float speed;
    public GameObject zombie;
    Vector3 targetL = new Vector3(0, 0, -1.5f);
    Vector3 targetB = new Vector3(0, 0, 0);
    Vector3 targetR = new Vector3(0, 0, 1.5f);
    Vector3 velo = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameover == false)
        {
            //if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z == 0)
            //{
            //    //while(transform.position.z != -1.5f)
            //    //    transform.Translate(Vector3.back*Time.deltaTime*speed);
            //    transform.position = Vector3.MoveTowards(transform.position, targetL, 1);
            //}
            //else if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.z == 1.5)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, targetB, 1);
            //}
            //else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z == 0)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, targetR, 1);
            //}
            //else if(Input.GetKeyDown(KeyCode.RightArrow) && transform.position.z == -1.5f)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, targetB, 1);
            //}
            if (isMove == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || isLeftmove == true)
                {
                    val = transform.position.z - val;
                    if (val <= -1.5f)
                    {
                        val = -1.5f;
                    }
                    StartCoroutine(move());

                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || isRightmove == true)
                {
                    val = transform.position.z + val;
                    if (val >= 1.5f)
                    {
                        val = 1.5f;
                    }
                    StartCoroutine(move());
                }
            }
        }
    }
    IEnumerator move()
    {
        isMove = true;
        v1 = transform.position;
        v2 = new Vector3(v1.x, v1.y, val);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 3f;
            transform.position = Vector3.Lerp(v1, v2, t);
            yield return null;
        }
        val = 1.5f;
        isMove = false;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    public void Moveleft()
    {
        isLeftmove = true;
    }

    public void MoveleftOff()
    {
        isLeftmove = false;
    }

    public void MoveRight()
    {
        isRightmove = true;
    }

    public void MoveRightOff()
    {
        isRightmove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (gameObject.tag == "Player")
            {
                GameManager.instance.isGameover = true;
                zombie.GetComponent<Animator>().SetTrigger("Fallsback");
            }
            else if (gameObject.tag == "Attack")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
