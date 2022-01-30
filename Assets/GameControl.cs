using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl Instancia { get; private set; }

    public static List<GameObject> fireballList = new List<GameObject>();
    public static List<GameObject> snowballList = new List<GameObject>();

    public int fase = 1;
    public Transform[] spawns;
    public Transform[] spawnsLava;
    public GameObject lava;
    public GameObject snowball;

    public GameObject StageImg;

    public Image bar1;
    public Image bar2;

    public Text P1count;
    public Text P2count;
    public Text timeText;
    public int timecount;

    public static int P1Fireballs = 0;
    public static int P2Fireballs = 0;

    public static int P1Snowballs = 0;
    public static int P2Snowballs = 0;

    public static float P1Fly = 0;
    public static float P2Fly = 0;

    public GameObject fireball;

    void Start()
    {
        if(fase == 1)
        {
            Time.timeScale = 0;
            timecount = 60;
            StartCoroutine("tempo");
            StartCoroutine("lavaTemp");
            for (int i = 0; i < spawns.Length; i++)
            {
                GameObject fb;
                fb = Instantiate(fireball, spawns[i].position, Quaternion.identity);
                fireballList.Add(fb);
            }
        }
        else if(fase == 2)
        {
            Time.timeScale = 0;
            StartCoroutine("tempo");
            StartCoroutine("snowTemp");
        }
        else if (fase == 3)
        {
            Time.timeScale = 0;
        }

    }

    IEnumerator snowTemp()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1f) * 60 * Time.deltaTime);
        GameObject fb;
        fb = Instantiate(snowball, new Vector3(Random.Range(-13.1f,16.1f),143,97), Quaternion.identity);
        snowballList.Add(fb);
        StartCoroutine("snowTemp");
    }

    IEnumerator lavaTemp()
    {
        yield return new WaitForSeconds( Random.Range(0f, 0.5f) * 60 * Time.deltaTime);
        Instantiate(lava,spawnsLava[Random.Range(0, spawnsLava.Length - 1)].position,Quaternion.identity);
        StartCoroutine("lavaTemp");
    }

    IEnumerator tempo()
    {
        yield return new WaitForSeconds(60 * Time.deltaTime);
        timecount -= 1;
        StartCoroutine("tempo");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StageImg.SetActive(false);
            Time.timeScale = 1;
        }
      

        if (fase == 1)
        {
            timeText.text = timecount.ToString();
            P1count.text = P1Fireballs.ToString();
            P2count.text = P2Fireballs.ToString();
            bar1.fillAmount = P1Fireballs / 10f;
            bar2.fillAmount = P2Fireballs / 10f;
            if (timecount == 0)
            {
                StopCoroutine("tempo");
                SceneManager.LoadScene("Montanha");
                Time.timeScale = 0;
            }
        }

        else if (fase == 2)
        {
            P1count.text = P1Snowballs.ToString();
            P2count.text = P2Snowballs.ToString();
            bar1.fillAmount = P1Snowballs / 10f;
            bar2.fillAmount = P2Snowballs / 10f;
        }
        else if(fase == 3)
        {
            P1count.text = ((int)P1Fly).ToString();
            P2count.text = ((int)P2Fly).ToString();
            bar1.fillAmount = P1Fly / 300f;
            bar2.fillAmount = P2Fly / 300f;
            P1Fly += 2 * Time.deltaTime;
            P2Fly += 2 * Time.deltaTime;
        }

    }
}
