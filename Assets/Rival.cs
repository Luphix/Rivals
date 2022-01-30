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

    public bool camFollow = true;


    public IEnumerator ShakeCam(float mag)
    {
        camFollow = false;
        Vector3 ofset = camOffset;
        for (float i = mag; i > 0; i -= (mag / 5))
        {
            cam.transform.position = new Vector3((gameObject.transform.position + camOffset).x - mag, (gameObject.transform.position + camOffset).y, (gameObject.transform.position + camOffset).z);
            yield return new WaitForSeconds(0.02f * 60 * Time.deltaTime);
            cam.transform.position = new Vector3((gameObject.transform.position + camOffset).x + mag, (gameObject.transform.position + camOffset).y, (gameObject.transform.position + camOffset).z);
            yield return new WaitForSeconds(0.02f * 60 * Time.deltaTime);
        }
        camFollow = true;

    }

    void Update()
    {
        if (camFollow)
        {
            cam.transform.position = Vector3.Slerp(cam.transform.position, gameObject.transform.position + camOffset, 0.2f);
        }
        else
        {
            cam.transform.position = gameObject.transform.position + camOffset;
        }

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
            StartCoroutine("ShakeCam", 0.1f);
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
