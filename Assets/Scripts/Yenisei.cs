using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yenisei : MonoBehaviour
{
	public const float SPEED = 3f;
	public float mVelX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    mVelX = Input.GetAxisRaw("Horizontal") * SPEED;
	    this.transform.Translate(mVelX * Time.deltaTime, 0, 0);
	    float posX = this.transform.position.x;
	    if((posX - 0.02 < 0) || (posX > 8))
		    this.transform.Translate(-(mVelX*Time.deltaTime), 0, 0);
        
    }
}
