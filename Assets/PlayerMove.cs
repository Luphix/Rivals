using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator anim;
    public CharacterController controller;

    public GameObject fireballHold;
    public GameObject vulc;

    public GameObject cam;
    public Vector3 camOffset;

    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -20f;
    private Vector3 move;
    private bool jumping = false;


    void Update()
    {
        cam.transform.position = Vector3.Slerp(cam.transform.position, gameObject.transform.position + camOffset, 0.2f);

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

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
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

        if (anim.GetBool("Walking"))
        {
            if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
            {
                move = new Vector3(Input.GetAxisRaw("Horizontal") * 0.66f, 0, Input.GetAxisRaw("Vertical") * 0.66f);
            }
            else
            {
                move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            }
            
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
       

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown(KeyCode.Space) && jumping == false && (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Run")))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumping = true;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Lava")
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