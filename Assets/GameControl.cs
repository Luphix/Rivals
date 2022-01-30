using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl Instancia { get; private set; }

    public static List<GameObject> fireballList = new List<GameObject>();

    public int fase = 1;
    public Transform[] spawns;
    public Transform[] spawnsLava;
    public GameObject lava;
    public GameObject snowball;

    public Text P1count;
    public Text P2count;
    public Text timeText;
    public int timecount;

    public static int P1Fireballs = 0;
    public static int P2Fireballs = 0;

    public GameObject fireball;

    void Start()
    {
        if(fase == 1)
        {
            Time.timeScale = 1;
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
            Time.timeScale = 1;
            timecount = 60;
            StartCoroutine("tempo");
            StartCoroutine("snowTemp");
        }
        
    }

    IEnumerator snowTemp()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1f) * 60 * Time.deltaTime);
        Instantiate(snowball, new Vector3(Random.Range(-13.1f,16.1f),143,97), Quaternion.identity);
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
        timeText.text = timecount.ToString();
        P1count.text = P1Fireballs.ToString();
        P2count.text = P2Fireballs.ToString();
    }
}
