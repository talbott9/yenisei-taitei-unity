using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	float w = 16.0f;
	//float h = 0.8f;
	int backgroundTicks;
	float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	    backgroundTicks++;
	    if(backgroundTicks * Time.deltaTime >= 0.416f) {
		    this.transform.Translate(-speed, 0, 0);
		    backgroundTicks = 0;
	    }
	    if(this.transform.position.x <= -w)
		    this.transform.Translate(w, 0, 0);
        
    }
}
