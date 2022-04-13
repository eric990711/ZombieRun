using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_move : MonoBehaviour
{
    public float speed;
    public GameObject Player;
    public GameObject ZombieBite;
    public bool isClick,isAttack;
    public bool isMoving;
    Vector3 targetL = new Vector3(0, 0, -1.5f);
    Vector3 targetB = new Vector3(0, 0, 0);
    Vector3 targetR = new Vector3(0, 0, 1.5f);
    Vector3 velo = Vector3.zero;
    void Start()
    {
       
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            if (GameManager.instance.isGameStarted == true)
            {
                isMoving = true;
                GetStarted();
            }
        }

        if (GameManager.instance.isGameover == false && GameManager.instance.isGameStarted == true)
        {
            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    if (Player.transform.position.z == 0 || Player.transform.position.z == 1.5)
            //    {
            //        while(Player.transform.position.z == -1.5f)
            //        {
            //            Player.transform.Translate(Vector3.back * Time.deltaTime * speed);
            //        }
            //    }
            //    //Player.transform.Translate(new Vector3(0, 0, -1.5f));
            //    //Player.transform.position = Vector3.SmoothDamp(Player.transform.position, targetL, ref velo, 0.01f);
            //    else if (Player.transform.position.z == -1.5)
            //        Player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            //    //Player.transform.Translate(new Vector3(0, 0, 0));
            //    //Player.transform.position = Vector3.SmoothDamp(Player.transform.position, targetB, ref velo, 0.01f);
            //    //else
            //    //    transform.position = Vector3.SmoothDamp(transform.position, targetL, ref velo, 0.01f);
            //}
            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    if (Player.transform.position.z == 0 || Player.transform.position.z == -1.5)
            //        Player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            //    //Player.transform.Translate(new Vector3(0, 0, 1.5f));
            //    //Player.transform.position = Vector3.SmoothDamp(Player.transform.position, targetR, ref velo, 0.01f);
            //    else if (Player.transform.position.z == 1.5)
            //        Player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            //    //Player.transform.Translate(new Vector3(0, 0, 0));
            //    //Player.transform.position = Vector3.SmoothDamp(Player.transform.position, targetB, ref velo, 0.01f);
            //    //else
            //    //    transform.position = Vector3.SmoothDamp(transform.position, targetR, ref velo, 0.01f);
            //}

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                Attack_off();           
            }
            //if (Input.GetKey(KeyCode.Space) || isAttack == true)
            //{
            //    GetComponent<Animator>().SetBool("Attacks", true);
            //}
            if (isAttack == true)
            {
                print("false");
                GetComponent<Animator>().SetTrigger("Attack_Done");
                isAttack = false;

            }
            transform.localPosition = Vector3.zero;
        }
    }
    public void GetStarted()
    {
        if (GameManager.instance.isGameStarted == true)
            GetComponent<Animator>().SetTrigger("Run");
    }
    public void Attack()
    {
        if (isClick == false && GameManager.instance.isGameover == false && GameManager.instance.isGameStarted == true)
        {
            StartCoroutine(Playerattack());
            isClick = true;
            isAttack = true;
            GetComponent<Animator>().SetTrigger("Attack");
            GameObject go = Instantiate(ZombieBite);
        }

    }
    public void Attack_off()
    {
        // print("Attack_off");
        if (GameManager.instance.isGameover == false)
            isClick = false;
    }

    IEnumerator Playerattack()
    {
        Player.tag = "Attack";
        yield return new WaitForSeconds(0.3f);
        Player.tag = "Player";

    }
}
