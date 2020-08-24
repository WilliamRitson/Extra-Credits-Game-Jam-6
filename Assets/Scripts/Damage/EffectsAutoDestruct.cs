using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAutoDestruct : MonoBehaviour
{
    private ParticleSystem psReference;
    // Start is called before the first frame update
    void Start()
    {
        psReference = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(psReference)
        {
            if(!psReference.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
