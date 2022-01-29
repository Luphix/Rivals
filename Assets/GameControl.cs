using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl Instancia { get; private set; }

    public static List<GameObject> fireballList = new List<GameObject>();

    public Transform[] spawns;
    public Transform[] spawnsLava;
    public GameObject lava;

    public Text P1count;
    public Text P2count;
    public Text timeText;
    public int timecount;

    public static int P1Fireballs = 0;
    public static int P2Fireballs = 0;

    public GameObject fireball;

    void Start()
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
