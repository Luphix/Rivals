using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject[] SkillObj;
    public Animator anim;
    public CharacterController controller;
    public int fase = 1;
    public GameObject fireballHold;
    public GameObject vulc;

    public GameObject[] round;

    public int skillChose;

    public GameObject cam;
    public Vector3 camOffset;

    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -20f;
    private Vector3 move;
    private bool jumping = false;

    public bool camFollow = true;

    public IEnumerator ShakeCam(float mag)
    {
        camFollow = false;
        Vector3 ofset = camOffset;
        for (float i = mag; i > 0; i -= (mag/5))
        {
            cam.transform.position = new Vector3((gameObject.transform.position + camOffset).x - mag, (gameObject.transform.position + camOffset).y, (gameObject.transform.position + camOffset).z);
            yield return new WaitForSeconds(0.02f * 60 * Time.deltaTime);
            cam.transform.position = new Vector3((gameObject.transform.position + camOffset).x + mag, (gameObject.transform.position + camOffset).y, (gameObject.transform.position + camOffset).z);
            yield return new WaitForSeconds(0.02f * 60 * Time.deltaTime);
        }
        camFollow = true;
        
    }

    void Shot()
    {
        if(skillChose < 3)
        {
            
            GameObject sk;
            sk = Instantiate(SkillObj[skillChose], new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), transform.rotation);
            sk.transform.Translate(Vector3.forward * 10);
            sk.GetComponent<SpellFireball>().player = 1;
        }
        else if(skillChose >= 3 && skillChose <= 5)
        {
            
            GameObject sk;
            sk = Instantiate(SkillObj[skillChose], new Vector3(transform.position.x, transform.position.y , transform.position.z), transform.rotation);
            sk.GetComponent<SpellIce>().player = 1;
        }
        else if (skillChose >= 6 && skillChose <= 8)
        {

            GameObject sk;
            sk = Instantiate(SkillObj[skillChose], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            sk.transform.Translate(Vector3.forward * 30);
            //sk.GetComponent<SpellIce>().player = 1;
        }

    }

    void Update()
    {
       
        

        if(fase == 1 || fase == 4)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    Instantiate(round[0], transform.position, Quaternion.identity);
                    skillChose = 1;
                    anim.Play("Spell");
                    anim.SetBool("Walking", false);
                    /*
                    if (GameManager.FireLvP1 == 0)
                    {

                    }
                    else
                    {
                        skillChose = GameManager.FireLvP1 - 1;
                        anim.Play("Spell");
                        anim.SetBool("Walking", false);
                    }
                    */
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    Instantiate(round[1], transform.position, Quaternion.identity);
                    skillChose = 4;
                    anim.Play("Spell");
                    anim.SetBool("Walking", false);
                    /*
                    if (GameManager.FireLvP1 == 0)
                    {

                    }
                    else
                    {
                        skillChose = GameManager.FireLvP1 - 1;
                        anim.Play("Spell");
                        anim.SetBool("Walking", false);
                    }
                    */
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    Instantiate(round[2], transform.position, Quaternion.identity);
                    skillChose = 6;
                    anim.Play("Spell");
                    anim.SetBool("Walking", false);
                    /*
                    if (GameManager.FireLvP1 == 0)
                    {

                    }
                    else
                    {
                        skillChose = GameManager.FireLvP1 - 1;
                        anim.Play("Spell");
                        anim.SetBool("Walking", false);
                    }
                    */
                }
            }
            if (camFollow)
            {
                cam.transform.position = Vector3.Slerp(cam.transform.position, gameObject.transform.position + camOffset, 0.2f);
            }
            else
            {
                cam.transform.position = gameObject.transform.position + camOffset;
            }

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer)
            {
                jumping = false;
            }
            anim.SetBool("InAir", jumping);
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    anim.SetBool("Walking", true);
                }
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            if (anim.GetBool("Walking") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Spell"))
            {
                if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
                {
                    move = new Vector3(Input.GetAxisRaw("Horizontal") * 0.66f, 0, Input.GetAxisRaw("Vertical") * 0.66f);
                }
                else
                {
                    move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                }

                controller.Move(move * Time.deltaTime * playerSpeed);
            }
            else
            {
                move = Vector3.zero;
            }

            // Changes the height position of the player..
            if (Input.GetKeyDown(KeyCode.Space) && jumping == false && (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Run")))
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumping = true;
            }

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

        }

        else if(fase == 2 && !GameControl.P1End)
        {

            if (camFollow)
            {
                cam.transform.position = Vector3.Slerp(cam.transform.position, gameObject.transform.position + camOffset, 0.1f);
            }
            else
            {
                cam.transform.position = gameObject.transform.position + camOffset;
            }
            anim.SetBool("Walking", true);

            move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0.5f);
            
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

        else if(fase == 3 && !GameControl.P1End)
        {
            if(transform.position.y < -6 || transform.position.y > 6)
            {
                GameControl.P1End = true;
            }
            if (camFollow)
            {
                cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3((gameObject.transform.position + camOffset).x, 0, (gameObject.transform.position + camOffset).z), 0.1f);
            }
            else
            {
                cam.transform.position = new Vector3((gameObject.transform.position + camOffset).x, 0, (gameObject.transform.position + camOffset).z);
            }
            anim.SetBool("Walking", true);

            move = new Vector3(0 , 0, 0);

            controller.Move(move * Time.deltaTime * playerSpeed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }


            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

        }

        
    }

    void OnCollisionEnter(Collision Col)
    {
        if(Col.gameObject.tag == "End")
        {
            GameControl.P1End = true;
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "End")
        {
            GameControl.P1End = true;
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
        if(Col.gameObject.tag == "Volcano")
        {
            if(fireballHold.activeSelf == true)
            {
                fireballHold.SetActive(false);
                vulc.GetComponent<Volcano>().Pl();
                GameControl.P1Fireballs += 1;
            }
        }
    }

}
