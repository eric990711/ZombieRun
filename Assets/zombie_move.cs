using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_move : MonoBehaviour
{
    public float speed;
    public GameObject Player;
    public bool isClick,isAttack;
    Vector3 targetL = new Vector3(0, 0, -1.5f);
    Vector3 targetB = new Vector3(0, 0, 0);
    Vector3 targetR = new Vector3(0, 0, 1.5f);
    Vector3 velo = Vector3.zero;
    void Start()
    {
        //transform.position = new Vector3(0, 0, 0);
        GetComponent<Animator>().SetTrigger("Run");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameover == false)
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
        }
    }

    public void Attack()
    {
        if (isClick == false && GameManager.instance.isGameover == false)
        {
            StartCoroutine(Playerattack());
            isClick = true;
            isAttack = true;
            GetComponent<Animator>().SetTrigger("Attack");
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
