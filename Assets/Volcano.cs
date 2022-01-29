using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    public ParticleSystem[] ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pl()
    {
        for (int i = 0; i < ps.Length; i++)
        {
            ps[i].Play();
        }
    }
}
