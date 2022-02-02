using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFireball : MonoBehaviour
{
    public int player;
    public float speed = 100;

    public GameObject burst;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(transform.position.x > 220 || transform.position.x < -200 || transform.position.z > 290 || transform.position.z < -270)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if(player == 1 && Col.gameObject.name == "Eol")
            {
                if(GameManager.FireLvP1 == 1)
                {
                    GameManager.HP2 -= 60;
                }
                else if(GameManager.FireLvP1 == 2)
                {
                    GameManager.HP2 -= 80;
                }
                else if (GameManager.FireLvP1 == 2)
                {
                    GameManager.HP2 -= 100;
                }

            }
            else if(player == 2 && Col.gameObject.name == "Adam")
            {
                if (GameManager.FireLvP2 == 1)
                {
                    GameManager.HP1 -= 60;
                }
                else if (GameManager.FireLvP2 == 2)
                {
                    GameManager.HP1 -= 80;
                }
                else if (GameManager.FireLvP2 == 2)
                {
                    GameManager.HP1 -= 100;
                }
            }

        }


        if (Col.gameObject.tag == "wall")
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
        
    }
}
