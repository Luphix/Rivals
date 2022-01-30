using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public GameObject burst;

    void Update()
    {
        if(transform.position.y < 11)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision Col)
    {
        if(Col.gameObject.tag == "Inv Ground")
        {
            Physics.IgnoreCollision(Col.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
        if(Col.gameObject.tag == "Player")
        {
            if(Col.gameObject.name == "Adam")
            {
                Instantiate(burst, transform.position, Quaternion.identity);
                Col.gameObject.GetComponent<PlayerMove>().StartCoroutine("ShakeCam", 0.2f);
                Destroy(gameObject);
                GameControl.P1Snowballs += 1;
            }
            else if(Col.gameObject.name == "Eol")
            {
                Instantiate(burst, transform.position, Quaternion.identity);
                Col.gameObject.GetComponent<Rival>().StartCoroutine("ShakeCam", 0.2f);
                Destroy(gameObject);
                GameControl.P2Snowballs += 1;
            }
            
        }
        if (Col.gameObject.tag == "wall")
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnCollisionStay(Collision Col)
    {
        if (Col.gameObject.tag == "Inv Ground")
        {
            Physics.IgnoreCollision(Col.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
       
    }
}
