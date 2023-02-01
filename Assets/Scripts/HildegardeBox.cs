using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HildegardeBox : MonoBehaviour
{
	public float posX;
	public float posY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    	posX = this.transform.position.x;
		posY = this.transform.position.y;
        
    }
}
