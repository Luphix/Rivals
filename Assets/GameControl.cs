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

    public bool fim = false;

    public static bool P1End = false;
    public static bool P2End =  false;

    public Slider HP1;
    public Slider HP2;

    public int fase = 1;
    public Transform[] spawns;
    public Transform[] spawnsLava;
    public GameObject lava;
    public GameObject snowball;

    public GameObject StageImg;

    public GameObject StageResult;
    public Text SkillName1;
    public Text SkillName2;
    public Text SkillInfo1;
    public Text SkillInfo2;
    public Image SkillSpr1;
    public Image SkillSpr2;

    public Sprite[] SprSki;

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

        else if(fase == 4)
        {
            HP1.value = GameManager.HP1 / 600;
            HP2.value = GameManager.HP2 / 600;
        }

    }

    IEnumerator snowTemp()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1f) * 60 * Time.deltaTime);
        GameObject fb;
        fb = Instantiate(snowball, new Vector3(Random.Range(-13.1f,16.1f),135,97), Quaternion.identity);
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

        if (Input.GetKeyDown(KeyCode.Space) && StageImg.activeSelf)
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
                
                StageResult.SetActive(true);

               

                Time.timeScale = 0;
                if(P1Fireballs < 3)
                {
                    SkillName1.text = "Nenhuma";
                    SkillInfo1.text = "";
                    SkillSpr1.sprite = SprSki[0];
                    GameManager.FireLvP1 = 0;
                }
                else if(P1Fireballs >= 3 && P1Fireballs <= 6)
                {
                    SkillName1.text = "BOLA DE FOGO NÍVEL 1";
                    SkillInfo1.text = "LANÇA UMA BOLA DE FOGO CAUSANDO 60 DE DANO";
                    SkillSpr1.sprite = SprSki[1];
                    GameManager.FireLvP1 = 1;
                }
                else if (P1Fireballs >= 7 && P1Fireballs <= 9)
                {
                    SkillName1.text = "BOLA DE FOGO NÍVEL 2";
                    SkillInfo1.text = "LANÇA UMA BOLA DE FOGO CAUSANDO 80 DE DANO E DEIXA O INIMIGO EM CHAMAS POR 5 SEGUNDOS";
                    SkillSpr1.sprite = SprSki[2];
                    GameManager.FireLvP1 = 2;
                }
                else if (P1Fireballs >= 10)
                {
                    SkillName1.text = "BOLA DE FOGO NÍVEL 3";
                    SkillInfo1.text = "LANÇA UMA BOLA DE FOGO CAUSANDO 100 DE DANO E DEIXA O INIMIGO EM CHAMAS POR 10 SEGUNDOS";
                    SkillSpr1.sprite = SprSki[3];
                    GameManager.FireLvP1 = 3;
                }




                if (P2Fireballs < 3)
                {
                    SkillName2.text = "Nenhuma";
                    SkillInfo2.text = "";
                    SkillSpr2.sprite = SprSki[0];
                    GameManager.FireLvP2 = 0;
                }
                else if (P2Fireballs >= 3 && P2Fireballs <= 6)
                {
                    SkillName2.text = "BOLA DE FOGO NÍVEL 1";
                    SkillInfo2.text = "LANÇA UMA BOLA DE FOGO CAUSANDO 60 DE DANO";
                    SkillSpr2.sprite = SprSki[1];
                    GameManager.FireLvP2 = 1;
                }
                else if (P2Fireballs >= 7 && P2Fireballs <= 9)
                {
                    SkillName2.text = "BOLA DE FOGO NÍVEL 2";
                    SkillInfo2.text = "LANÇA UMA BOLA DE FOGO CAUSANDO 80 DE DANO E DEIXA O INIMIGO EM CHAMAS POR 5 SEGUNDOS";
                    SkillSpr2.sprite = SprSki[2];
                    GameManager.FireLvP2 = 2;
                }
                else if (P2Fireballs >= 10)
                {
                    SkillName2.text = "BOLA DE FOGO NÍVEL 3";
                    SkillInfo2.text = "LANÇA UMA BOLA DE FOGO CAUSANDO 100 DE DANO E DEIXA O INIMIGO EM CHAMAS POR 10 SEGUNDOS";
                    SkillSpr2.sprite = SprSki[3];
                    GameManager.FireLvP2 = 3;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("Montanha");
                    P1End = false;
                    P2End = false;
                    Time.timeScale = 0;
                }
            }
        }

        else if (fase == 2)
        {
            P1count.text = P1Snowballs.ToString();
            P2count.text = P2Snowballs.ToString();
            bar1.fillAmount = P1Snowballs / 10f;
            bar2.fillAmount = P2Snowballs / 10f;

            if (P1End && P2End)
            {
                StageResult.SetActive(true);

               

                Time.timeScale = 0;

                if (P1Snowballs < 3)
                {
                    SkillName1.text = "Nenhuma";
                    SkillInfo1.text = "";
                    SkillSpr1.sprite = SprSki[0];
                    GameManager.IceLvP1 = 0;
                }
                else if (P1Snowballs >= 3 && P1Snowballs <= 6)
                {
                    SkillName1.text = "ESPINHOS DE GELO NÍVEL 1";
                    SkillInfo1.text = "LANÇA UMA ONDA DE ESPINHOS DE GELO CAUSANDO 40 DE DANO E DEIXANDO O INIMIGO LENTO POR 5 SEGUNDOS";
                    SkillSpr1.sprite = SprSki[1];
                    GameManager.IceLvP1 = 1;
                }
                else if (P1Snowballs >= 7 && P1Snowballs <= 9)
                {
                    SkillName1.text = "ESPINHOS DE GELO NÍVEL 2";
                    SkillInfo1.text = "LANÇA UMA ONDA DE ESPINHOS DE GELO CAUSANDO 60 DE DANO E DEIXANDO O INIMIGO LENTO POR 7 SEGUNDOS";
                    SkillSpr1.sprite = SprSki[2];
                    GameManager.IceLvP1 = 2;
                }
                else if (P1Snowballs >= 10)
                {
                    SkillName1.text = "ESPINHOS DE GELO NÍVEL 3";
                    SkillInfo1.text = "LANÇA UMA ONDA DE ESPINHOS DE GELO CAUSANDO 80 DE DANO E CONGELANDO POR 5 SEGUNDOS";
                    SkillSpr1.sprite = SprSki[3];
                    GameManager.IceLvP1 = 3;
                }



                if (P2Snowballs < 3)
                {
                    SkillName2.text = "Nenhuma";
                    SkillInfo2.text = "";
                    SkillSpr2.sprite = SprSki[0];
                    GameManager.IceLvP2 = 0;
                }
                else if (P2Snowballs >= 3 && P2Snowballs <= 6)
                {
                    SkillName2.text = "ESPINHOS DE GELO NÍVEL 1";
                    SkillInfo2.text = "LANÇA UMA ONDA DE ESPINHOS DE GELO CAUSANDO 40 DE DANO E DEIXANDO O INIMIGO LENTO POR 5 SEGUNDOS";
                    SkillSpr2.sprite = SprSki[1];
                    GameManager.IceLvP2 = 1;
                }
                else if (P2Snowballs >= 7 && P2Snowballs <= 9)
                {
                    SkillName2.text = "ESPINHOS DE GELO NÍVEL 2";
                    SkillInfo2.text = "LANÇA UMA ONDA DE ESPINHOS DE GELO CAUSANDO 60 DE DANO E DEIXANDO O INIMIGO LENTO POR 7 SEGUNDOS";
                    SkillSpr2.sprite = SprSki[2];
                    GameManager.IceLvP2 = 2;
                }
                else if (P2Snowballs >= 10)
                {
                    SkillName2.text = "ESPINHOS DE GELO NÍVEL 3";
                    SkillInfo2.text = "LANÇA UMA ONDA DE ESPINHOS DE GELO CAUSANDO 80 DE DANO E CONGELANDO POR 5 SEGUNDOS";
                    SkillSpr2.sprite = SprSki[3];
                    GameManager.IceLvP2 = 3;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("Ceu");
                    P1End = false;
                    P2End = false;
                    Time.timeScale = 0;
                }
            }
        }
        else if(fase == 3)
        {
            P1count.text = ((int)P1Fly).ToString();
            P2count.text = ((int)P2Fly).ToString();
            bar1.fillAmount = P1Fly / 300f;
            bar2.fillAmount = P2Fly / 300f;

            if(P1End == false)
            {
                P1Fly += 2 * Time.deltaTime;
            }

            if(P2End == false)
            {
                P2Fly += 2 * Time.deltaTime;
            }
            
            

            if(P1Fly >= 200)
            {
                P1End = true;
            }

            if (P2Fly >= 200)
            {
                P2End = true;
            }


            if (P1End && P2End)
            {
                StageResult.SetActive(true);

                

                Time.timeScale = 0;

                if (P1Fly < 50)
                {
                    SkillName1.text = "Nenhuma";
                    SkillInfo1.text = "";
                    SkillSpr1.sprite = SprSki[0];
                    GameManager.WindLvP1 = 0;
                }
                else if (P1Fly >= 50 && P1Fly <= 150)
                {
                    SkillName1.text = "TORNADO NÍVEL 1";
                    SkillInfo1.text = "INVOCA UM TORNADO PEQUENO QUE DURA 3 SEGUNDOS E CAUSA 10 DE DANO POR SEGUNDO, AUMENTANDO SUA VELOCIDADE EM 10% ENQUANTO DURAR";
                    SkillSpr1.sprite = SprSki[1];
                    GameManager.WindLvP1 = 1;
                }
                else if (P1Fly > 150 && P1Fly <= 199)
                {
                    SkillName1.text = "TORNADO NÍVEL 2";
                    SkillInfo1.text = "INVOCA UM TORNADO MÉDIO QUE DURA 4 SEGUNDOS E CAUSA 15 DE DANO POR SEGUNDO, AUMENTANDO SUA VELOCIDADE EM 15% ENQUANTO DURAR";
                    SkillSpr1.sprite = SprSki[2];
                    GameManager.WindLvP1 = 2;
                }
                else if (P1Fly >= 200)
                {
                    SkillName1.text = "TORNADO NÍVEL 3";
                    SkillInfo1.text = "INVOCA UM TORNADO GRANDE QUE DURA 5 SEGUNDOS E CAUSA 20 DE DANO POR SEGUNDO, AUMENTANDO SUA VELOCIDADE EM 20% ENQUANTO DURAR";
                    SkillSpr1.sprite = SprSki[3];
                    GameManager.WindLvP1 = 3;
                }


                if (P2Fly < 50)
                {
                    SkillName2.text = "Nenhuma";
                    SkillInfo2.text = "";
                    SkillSpr2.sprite = SprSki[0];
                    GameManager.WindLvP2 = 0;
                }
                else if (P2Fly >= 50 && P2Fly <= 150)
                {
                    SkillName2.text = "TORNADO NÍVEL 1";
                    SkillInfo2.text = "INVOCA UM TORNADO PEQUENO QUE DURA 3 SEGUNDOS E CAUSA 10 DE DANO POR SEGUNDO, AUMENTANDO SUA VELOCIDADE EM 10% ENQUANTO DURAR";
                    SkillSpr2.sprite = SprSki[1];
                    GameManager.WindLvP2 = 1;
                }
                else if (P2Fly > 150 && P2Fly <= 199)
                {
                    SkillName2.text = "TORNADO NÍVEL 2";
                    SkillInfo2.text = "INVOCA UM TORNADO MÉDIO QUE DURA 4 SEGUNDOS E CAUSA 15 DE DANO POR SEGUNDO, AUMENTANDO SUA VELOCIDADE EM 15% ENQUANTO DURAR";
                    SkillSpr2.sprite = SprSki[2];
                    GameManager.WindLvP2 = 2;
                }
                else if (P2Fly >= 200)
                {
                    SkillName2.text = "TORNADO NÍVEL 3";
                    SkillInfo2.text = "INVOCA UM TORNADO GRANDE QUE DURA 5 SEGUNDOS E CAUSA 20 DE DANO POR SEGUNDO, AUMENTANDO SUA VELOCIDADE EM 20% ENQUANTO DURAR";
                    SkillSpr2.sprite = SprSki[3];
                    GameManager.WindLvP2 = 3;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("Luta");
                    P1End = false;
                    P2End = false;
                    Time.timeScale = 0;
                }
            }
        }

    }
}
