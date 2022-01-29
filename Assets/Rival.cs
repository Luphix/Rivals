using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rival : MonoBehaviour
{

    public GameObject fireballHold;
    public GameObject vulc;
    private Vector3 vet = new Vector3(0, 0, 0);

    public GameObject objetivo;

    public float minDist = 10000;
    public GameObject alvo;
    public NavMeshAgent agent;
    public Animator anim;

    public GameObject cam;
    public Vector3 camOffset;



    void Update()
    {
        cam.transform.position = Vector3.Slerp(cam.transform.position, gameObject.transform.position + camOffset, 0.2f);

        if (agent.velocity == vet)
        {
            anim.SetBool("Walking", false);


        }

        //Transição de animação (Parado -> Andando):

        else
        {
            anim.SetBool("Walking", true);

        }

        if (fireballHold.activeSelf)
        {
            agent.SetDestination(objetivo.transform.position);
        }
        else
        {
            foreach (GameObject fireball in GameControl.fireballList)
            {
                if (fireball == null)
                {
                    GameControl.fireballList.Remove(fireball);
                }
                if (alvo == null)
                {
                    minDist = Vector3.Distance(fireball.transform.position, gameObject.transform.position);
                    alvo = fireball;
                }
                else if (Vector3.Distance(fireball.transform.position, gameObject.transform.position) < minDist)
                {
                    minDist = Vector3.Distance(fireball.transform.position, gameObject.transform.position);
                    alvo = fireball;
                }
            }
            if (alvo != null)
            {
                agent.SetDestination(new Vector3(alvo.transform.position.x, alvo.transform.position.y - 1, alvo.transform.position.z));
            }
        }
        
    }

    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Lava")
        {
            fireballHold.SetActive(false);
        }

        if (Col.gameObject.tag == "Collectible")
        {
            if (fireballHold.activeSelf == false)
            {
                Debug.Log("Coletou");
                Destroy(Col.gameObject);
                fireballHold.SetActive(true);
                
            }

        }
        if (Col.gameObject.tag == "Volcano")
        {
            if (fireballHold.activeSelf == true)
            {
                fireballHold.SetActive(false);
                vulc.GetComponent<Volcano>().Pl();
                GameControl.P2Fireballs += 1;
            }
        }
    }
}
