using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
	public float speed = 4.2f;
	public float interval = 0.08f;
	public float posX, posY, dx, dy;
	public float targetX, targetY;
	bool shotBullet;
	GameObject HildegardeBox;
    // Start is called before the first frame update
    void Start()
    {
	HildegardeBox = GameObject.Find("HildegardeBox");
	HildegardeBox hildegardeBox = HildegardeBox.GetComponent<HildegardeBox>();
	targetX = hildegardeBox.posX; targetY = hildegardeBox.posY;
	posX = this.transform.position.x;
	posY = this.transform.position.y;
	if(!shotBullet) {
		float side1 = targetY - posY;
		float side2 = posX - targetX;
		float r = Mathf.Sqrt(side1 * side1 + side2 * side2);
		float sinAngle = (side1/r);
		float cosAngle = (side2/r);
		if(posX > targetX)
			dx = -speed*cosAngle;
		else {
			cosAngle = -cosAngle;
			dx = speed*cosAngle;
		}
		if(posY < targetY)
			dy = speed*sinAngle;
		else {
			sinAngle = -sinAngle;
			dy = -speed*sinAngle;
		}
		shotBullet = true;
	}
    }

    // Update is called once per frame
    void Update()
    {
	    this.transform.Translate(dx * Time.deltaTime, dy * Time.deltaTime, 0);
	    posX = this.transform.position.x; posY = this.transform.position.y;
	    if(posX < -8 || posX > 8 || posY > 6 || posY < -4) //8 = screen width
		    Destroy(this.gameObject);
  
    }
    void OnTriggerEnter(Collider other) {
	    Hildegarde hildegarde = gameObject.GetComponent<Hildegarde>();
            HildegardeBox hildegardeBox = other.GetComponent<HildegardeBox>();
	    if(hildegardeBox != null) {
		    hildegarde.death = true;
	    }
    }
}
