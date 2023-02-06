using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yenisei : MonoBehaviour
{
	public const float SPEED = 3.6f;
	public float mVelX, mVelY;
	private Animator anim;
	bool moved, changeMove;
	float accTicks, deathTicks, posX, posY;
	public float acc = 0.08f;
	GameObject hildegarde;
	void moveToAcc(float targetX, float targetY, float speed, float acc) {
		if(!moved) {
			float side1 = Mathf.Abs(targetY - posY);
			float side2 = Mathf.Abs(posX - targetX);
			float r = Mathf.Sqrt(side1 * side1 + side2 * side2);
			float sinAngle = (side1/r);
			float cosAngle = (side2/r);
			if(posX > targetX)
				mVelX = -speed*cosAngle;
			else {
				cosAngle = -cosAngle;
				mVelX = speed*cosAngle;
			}
			if(posY < targetY)
				mVelY = speed*sinAngle;
			else {
				sinAngle = -sinAngle;
				mVelY = -speed*sinAngle;
			}
			moved = true;
		}
		mVelX += acc*accTicks;
		this.transform.Translate(mVelX*Time.deltaTime, 0, 0);
	}
    // Start is called before the first frame update
    void Start()
    {
	    anim =  GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
	    posX = this.transform.position.x;
	    posY = this.transform.position.y;
	    hildegarde = GameObject.Find("Hildegarde");
	    Hildegarde hildegardeScript = hildegarde.GetComponent<Hildegarde>();
	    if(!hildegardeScript.death) {
		    mVelX = Input.GetAxisRaw("Horizontal") * SPEED * Time.deltaTime;
		    this.transform.Translate(mVelX, 0, 0);
		    if((this.transform.position.x < -5) || (this.transform.position.x > 5))
			    this.transform.Translate(-mVelX, 0, 0);
	    } else {
		    if(!(posX > 8)) {
			    accTicks += Time.deltaTime; 
			    deathTicks += Time.deltaTime;
			    moveToAcc(posX - 10f, posY, SPEED, acc);
		    }
	    }
	    anim.SetFloat("velX", mVelX);
    }
}
