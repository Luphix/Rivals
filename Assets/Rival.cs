using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rival : MonoBehaviour
{

    public GameObject[] SkillObj;
    public bool jump = true;
    public float dir = 0;
    public GameObject fireballHold;
    public GameObject vulc;
    private Vector3 vet = new Vector3(0, 0, 0);

    public int skillChose;

    public int fase = 1;

    public GameObject objetivo;

    public CharacterController controller;

    public float minDist = 10000;
    public GameObject alvo;
    public NavMeshAgent agent;
    public Animator anim;

    public GameObject cam;
    public Vector3 camOffset;

    public bool camFollow = true;

    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -20f;
    private Vector3 move;
    private bool jumping = false;

    void Shot()
    {
        GameObject sk;
        sk = Instantiate(SkillObj[skillChose], transform.position, transform.rotation);
        sk.GetComponent<SpellFireball>().player = 2;
    }

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

        if(fase == 1)
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

        else if(fase == 2 && !GameControl.P2End)
        {
            foreach (GameObject snowball in GameControl.snowballList)
            {
                if (snowball == null)
                {
                    GameControl.snowballList.Remove(snowball);
                }
                if (alvo == null)
                {
                    minDist = Vector3.Distance(snowball.transform.position, gameObject.transform.position);
                    alvo = snowball;
                }
                else if (Vector3.Distance(snowball.transform.position, gameObject.transform.position) < minDist && snowball.transform.position.z > transform.position.z)
                {
                    minDist = Vector3.Distance(snowball.transform.position, gameObject.transform.position);
                    alvo = snowball;
                }
            }
            if (alvo != null)
            {
                if (Mathf.Abs(alvo.transform.position.x - transform.position.x) > 1)
                {
                    if (alvo.transform.position.x > transform.position.x)
                    {
                        dir = 1;
                    }
                    else if (alvo.transform.position.x < transform.position.x)
                    {
                        dir = -1;
                    }
                    else
                    {
                        dir = 0;
                    }
                }
                else
                {
                    dir = 0;
                }
                
            }

            if (camFollow)
            {
                cam.transform.position = Vector3.Slerp(cam.transform.position, gameObject.transform.position + camOffset, 0.1f);
            }
            else
            {
                cam.transform.position = gameObject.transform.position + camOffset;
            }
            anim.SetBool("Walking", true);

            move = new Vector3(dir, 0, 0.5f);

            controller.Move(move * Time.deltaTime * playerSpeed);



            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

        }

        else if (fase == 3 && !GameControl.P2End)
        {

            if (transform.position.y < -6 || transform.position.y > 6)
            {
                GameControl.P2End = true;
            }
            if (camFollow)
            {
                cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3((gameObject.transform.position + camOffset).x, 0, (gameObject.transform.position + camOffset).z), 0.1f);
            }
            else
            {
                cam.transform.position = new Vector3((gameObject.transform.position + camOffset).x, 0, (gameObject.transform.position + camOffset).z);
            }
            if(transform.position.y < -5 && jump == false)
            {
                float chance = Random.Range(0, 100);
                if(chance > 70)
                {
                    jump = true;
                }
                
            }
            else if(transform.position.y < 0 && jump == false)
            {
                float chance = Random.Range(0, 100);
                if (chance > 90)
                {
                    jump = true;
                }
            }
            anim.SetBool("Walking", true);

            move = new Vector3(0, 0, 0);

            controller.Move(move * Time.deltaTime * playerSpeed);
            if (jump == true)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jump = false;
            }

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        else if (fase == 4)
        {
            if (camFollow)
            {
                cam.transform.position = Vector3.Slerp(cam.transform.position, gameObject.transform.position + camOffset, 0.2f);
            }
            else
            {
                cam.transform.position = gameObject.transform.position + camOffset;
            }

        }

    }

    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "End")
        {
            GameControl.P2End = true;
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "End")
        {
            GameControl.P2End = true;
        }
        if (Col.gameObject.tag == "Lava")
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
