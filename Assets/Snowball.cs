using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public GameObject burst;

    void OnCollisionEnter(Collision Col)
    {
        if(Col.gameObject.tag == "Player")
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            Col.gameObject.GetComponent<PlayerMove>().StartCoroutine("ShakeCam", 0.2f);
            Destroy(gameObject);
        }
        if (Col.gameObject.tag == "wall")
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
