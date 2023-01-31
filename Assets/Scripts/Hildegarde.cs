using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hildegarde : MonoBehaviour
{
	int frameTicks;
	float animWobble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    frameTicks++;
	    this.transform.Translate(0, -animWobble, 0);
	    animWobble = 0;
	    if(frameTicks * Time.deltaTime >= 0.183f) {
		    animWobble = 0.01f;
		    this.transform.Translate(0, animWobble, 0);
		    frameTicks = 0;
	    }
    }
}
