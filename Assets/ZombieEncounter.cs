using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieEncounter : MonoBehaviour
{
    public Image image;
    public GameObject health;
    public GameObject GameOver;
    public GameObject BacktoGraves;
    public GameObject ZombieWalk;
    public GameObject ZombieHit;
    public GameObject ZombieBreak;
    public GameObject BreakEffect;
    public GameObject PumpkinEffect;
    public GameObject BlackEffect;
    public GameObject BonusEffect;
    public GameObject HpEffect;
    GameObject s_ZombieWalk1;
    GameObject s_ZombieWalk2;
    public GameObject ZombieFall;

    bool isMove;
    float val = 1.5f;
    public bool isLeftmove;
    public bool isRightmove;
    int count = 0;
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

    IEnumerator WalkSound()
    {
        yield return new WaitForSeconds(0.65f);
        s_ZombieWalk2 = Instantiate(ZombieWalk);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameStarted == true && count == 0)
        {
            count++;
            s_ZombieWalk1  = Instantiate(ZombieWalk);
            StartCoroutine(WalkSound());
        }
        if (GameManager.instance.isGameover == false && GameManager.instance.isGameStarted == true)
        {
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
                health.GetComponent<Image>().fillAmount -= 0.1f;
                if (health.GetComponent<Image>().fillAmount == 0)
                {
                    GameManager.instance.isGameover = true;
                    zombie.GetComponent<Animator>().SetTrigger("Fallsback");
                    GameObject fall = Instantiate(ZombieFall);
                    Destroy(s_ZombieWalk1);
                    Destroy(s_ZombieWalk2);
                    StartCoroutine(FadeOutOver(1f));
                    GameOver.SetActive(true);
                    GameManager.instance.Gameover();
                }
                else
                {
                    GameObject go = Instantiate(BreakEffect);
                    go.transform.position = other.gameObject.transform.position;
                    GameObject Damage = Instantiate(ZombieHit);
                    Destroy(other.gameObject);
                }
            }
            else if (gameObject.tag == "Attack")
            {
                GameObject go = Instantiate(BreakEffect);
                GameObject Damages = Instantiate(ZombieBreak);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                GameManager.instance.score += 100;
                GameManager.instance.GetScore();
            }
        }
        if (other.tag == "Pumpkin")
        {
            if (gameObject.tag == "Player")
            {
                health.GetComponent<Image>().fillAmount -= 0.1f;
                if (health.GetComponent<Image>().fillAmount == 0)
                {
                    GameManager.instance.isGameover = true;
                    zombie.GetComponent<Animator>().SetTrigger("Fallsback");
                    GameObject fall = Instantiate(ZombieFall);
                    Destroy(s_ZombieWalk1);
                    Destroy(s_ZombieWalk2);
                    StartCoroutine(FadeOutOver(1f));
                    GameOver.SetActive(true);
                    GameManager.instance.Gameover();
                }
                else
                {
                    GameObject go = Instantiate(PumpkinEffect);
                    go.transform.position = other.gameObject.transform.position;
                    GameObject Damage = Instantiate(ZombieHit);
                    Destroy(other.gameObject);
                }
            }
            else if (gameObject.tag == "Attack")
            {
                GameObject go = Instantiate(PumpkinEffect);
                GameObject Damages = Instantiate(ZombieBreak);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                GameManager.instance.score += 100;
                GameManager.instance.GetScore();
            }
        }
        if (other.tag == "Black")
        {
            if (gameObject.tag == "Player")
            {
                health.GetComponent<Image>().fillAmount -= 0.1f;
                if (health.GetComponent<Image>().fillAmount == 0)
                {
                    GameManager.instance.isGameover = true;
                    zombie.GetComponent<Animator>().SetTrigger("Fallsback");
                    GameObject fall = Instantiate(ZombieFall);
                    Destroy(s_ZombieWalk1);
                    Destroy(s_ZombieWalk2);
                    StartCoroutine(FadeOutOver(1f));
                    GameOver.SetActive(true);
                    GameManager.instance.Gameover();
                }
                else
                {
                    GameObject go = Instantiate(BlackEffect);
                    go.transform.position = other.gameObject.transform.position;
                    GameObject Damage = Instantiate(ZombieHit);
                    Destroy(other.gameObject);
                }
            }
            else if (gameObject.tag == "Attack")
            {
                GameObject go = Instantiate(BlackEffect);
                GameObject Damages = Instantiate(ZombieBreak);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                GameManager.instance.score += 100;
                GameManager.instance.GetScore();
            }
        }
        if (other.tag == "DodgeEnemy")
        {
            if (gameObject.tag == "Player" || gameObject.tag == "Attack")
            {
                health.GetComponent<Image>().fillAmount -= 0.1f;
                if (health.GetComponent<Image>().fillAmount == 0)
                {
                    GameManager.instance.isGameover = true;
                    zombie.GetComponent<Animator>().SetTrigger("Fallsback");
                    GameObject fall = Instantiate(ZombieFall);
                    Destroy(s_ZombieWalk1);
                    Destroy(s_ZombieWalk2);
                    StartCoroutine(FadeOutOver(1f));
                    GameOver.SetActive(true);
                    GameManager.instance.Gameover();
                }
                else
                {
                    GameObject Damage = Instantiate(ZombieHit);
                }
            }
            //else if (gameObject.tag == "Attack")
            //{
            //    GameObject go = Instantiate(BreakEffect);
            //    Destroy(other.gameObject);
            //    GameManager.instance.score += 100;
            //    GameManager.instance.GetScore();
            //}
        }
        if (other.tag == "Trap")
        {
            if (gameObject.tag == "Player")
            {
                GameObject go = Instantiate(BlackEffect);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                StartCoroutine(FadeOut(1f));
            }
            else if (gameObject.tag == "Attack")
            {
                GameObject go = Instantiate(BlackEffect);
                GameObject Damages = Instantiate(ZombieBreak);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                GameManager.instance.score += 100;
                GameManager.instance.GetScore();
            }
        }
        if (other.tag == "Bonus")
        {
            if (gameObject.tag == "Player")
            {
                GameObject go = Instantiate(BonusEffect);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                GameManager.instance.score -= 500;
                GameManager.instance.GetScore();
            }
            else if (gameObject.tag == "Attack")
            {
                GameObject go = Instantiate(BreakEffect);
                GameObject Damages = Instantiate(ZombieBreak);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                GameManager.instance.score += 1000;
                GameManager.instance.GetScore();
            }
        }
        if(other.tag == "Goal")
        {
            if(gameObject.tag == "Player")
            {
                GameManager.instance.isGameover = true;
                zombie.GetComponent<Animator>().SetTrigger("Fallfront");
                GameObject fall = Instantiate(ZombieFall);
                Destroy(s_ZombieWalk1);
                Destroy(s_ZombieWalk2);
                StartCoroutine(FadeOutOver(1f));
                BacktoGraves.SetActive(true);
                GameManager.instance.Gameover();
            }
            else if(gameObject.tag == "Attack")
            {
                GameObject go = Instantiate(BlackEffect);
                GameObject Damages = Instantiate(ZombieBreak);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                GameManager.instance.score += 5000;
                GameManager.instance.GetScore();
            }
        }
        if (other.tag == "HpPack")
        {
            if (gameObject.tag == "Player")
            {
                health.GetComponent<Image>().fillAmount -= 0.3f;
                if (health.GetComponent<Image>().fillAmount <= 0)
                {
                    GameManager.instance.isGameover = true;
                    zombie.GetComponent<Animator>().SetTrigger("Fallsback");
                    GameObject fall = Instantiate(ZombieFall);
                    Destroy(s_ZombieWalk1);
                    Destroy(s_ZombieWalk2);
                    StartCoroutine(FadeOutOver(1f));
                    GameOver.SetActive(true);
                    GameManager.instance.Gameover();
                }
                else
                {
                    GameObject go = Instantiate(HpEffect);
                    go.transform.position = other.gameObject.transform.position;
                    GameObject Damage = Instantiate(ZombieHit);
                    Destroy(other.gameObject);
                }
            }
            else if (gameObject.tag == "Attack")
            {
                GameObject go = Instantiate(BreakEffect);
                GameObject Damages = Instantiate(ZombieBreak);
                go.transform.position = other.gameObject.transform.position;
                Destroy(other.gameObject);
                health.GetComponent<Image>().fillAmount += 0.3f;
            }
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
        StartCoroutine(FadeIn(1f));
    }

    public IEnumerator FadeOutOver(float time)
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
