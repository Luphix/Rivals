using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEff : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject inst;

    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                if(inst != null)
                {
                    Instantiate(inst, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
