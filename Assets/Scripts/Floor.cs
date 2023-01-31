using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
	float w = 8.0f;
	//float h = 0.8f;
	int floorTicks;
	float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	    floorTicks++;
	    if(floorTicks * Time.deltaTime >= 0.016f) {
		    this.transform.Translate(-speed, 0, 0);
		    floorTicks = 0;
	    }
	    if(this.transform.position.x <= -w)
		    this.transform.Translate(w, 0, 0);
        
    }
}
