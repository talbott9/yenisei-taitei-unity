using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherProjectile : MonoBehaviour
{
	float ticks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    ticks += Time.deltaTime;
	    if(ticks >= 1)
	    	Destroy(this.gameObject);

        
    }
}
