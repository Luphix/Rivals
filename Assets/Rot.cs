using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    public float speed;
    public int state = 1;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeStat(int i)
    {
        state = i;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 1)
        {
            //cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(0, -10, -584), 0.1f);
           
        }
        else if(state == 2)
        {
            //cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3(0, -10, -168), 0.1f);
            
        }
        transform.Rotate(0,speed * Time.fixedDeltaTime,0);
    }
}
