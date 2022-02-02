using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellIce : MonoBehaviour
{
    public int player;
    public float speed = 50;

    public GameObject burst;

    public ParticleSystem[] ps;

    void Start()
    {

    }

    void Update()
    {
        if(ps[0].IsAlive() && ps[1].IsAlive())
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        

        if (transform.position.x > 220 || transform.position.x < -200 || transform.position.z > 290 || transform.position.z < -270)
        {
            ps[0].Stop();
            ps[1].Stop();
        }

        if (!ps[0].IsAlive() && !ps[1].IsAlive())
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player" && ((player == 1 && Col.gameObject.name == "Eol") || (player == 2 && Col.gameObject.name == "Adam")))
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            ps[0].Stop();
            ps[1].Stop();
            if (player == 1 && Col.gameObject.name == "Eol")
            {
                if (GameManager.IceLvP1 == 1)
                {
                    GameManager.HP2 -= 40;
                }
                else if (GameManager.IceLvP1 == 2)
                {
                    GameManager.HP2 -= 60;
                }
                else if (GameManager.IceLvP1 == 2)
                {
                    GameManager.HP2 -= 80;
                }

            }
            else if (player == 2 && Col.gameObject.name == "Adam")
            {
                if (GameManager.IceLvP2 == 1)
                {
                    GameManager.HP1 -= 40;
                }
                else if (GameManager.IceLvP2 == 2)
                {
                    GameManager.HP1 -= 60;
                }
                else if (GameManager.IceLvP2 == 2)
                {
                    GameManager.HP1 -= 80;
                }
            }

        }


        if (Col.gameObject.tag == "wall")
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            ps[0].Stop();
            ps[1].Stop();

        }

    }
}
