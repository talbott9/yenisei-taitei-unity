using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yenisei : MonoBehaviour
{
	public const float SPEED = 3.6f;
	public float mVelX;
	private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
	    anim =  GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
	    mVelX = Input.GetAxisRaw("Horizontal") * SPEED;
	    this.transform.Translate(mVelX * Time.deltaTime, 0, 0);
	    float posX = this.transform.position.x;
	    if((posX - 0.02 < -6) || (posX > 6 + 0.05))
		    this.transform.Translate(-(mVelX*Time.deltaTime), 0, 0);
            anim.SetFloat("velX", mVelX);
    }
}
