using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
	public int shotType = 1;
	public float speed = 4.2f;
	public float acc = 0.06f;
	//public float interval = 0.08f;
	public float targetX, targetY;
	public bool gravity;
	private float posX, posY, dx, dy;
	private bool shotBullet, shotReturn;
	GameObject HildegardeBox;
	[SerializeField] GameObject projectile41Prefab;
	GameObject projectile;
	private int gravityTicks;
    // Start is called before the first frame update
    void Start()
    {
	switch(shotType) {
		case 1: HildegardeBox = GameObject.Find("HildegardeBox");
			HildegardeBox hildegardeBox = HildegardeBox.GetComponent<HildegardeBox>();
			targetX = hildegardeBox.posX; targetY = hildegardeBox.posY;
			break;
		case 2: HildegardeBox = GameObject.Find("HildegardeBox");
			HildegardeBox hildegardeBox1 = HildegardeBox.GetComponent<HildegardeBox>();
			targetY = hildegardeBox1.posY;
			break;
		case 3: targetX = Random.Range(-4.0f, 4.0f);
			targetY = Random.Range(-1.0f, 2.0f);
			break;
		case 4: HildegardeBox = GameObject.Find("HildegardeBox");
			HildegardeBox hildegardeBox2 = HildegardeBox.GetComponent<HildegardeBox>();
			targetY = hildegardeBox2.posY;
			break;
	}

	posX = this.transform.position.x;
	posY = this.transform.position.y;
	if(!gravity) {
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
    }

    // Update is called once per frame
    void Update()
    {
	    gravityTicks++;
	    if(gravity) {
		if(!shotBullet) {
			float side1 = targetY - posY;
			float side2 = posX - targetX;
			float r = Mathf.Sqrt(side1 * side1 + side2 * side2);
			float sinAngle = (side1/r);
			float cosAngle = (side2/r);
			shotBullet = true;
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
		}
		dy -= acc*gravityTicks*Time.deltaTime;
	}

	    if(shotType != 4) {
	    	this.transform.Translate(dx * Time.deltaTime, dy * Time.deltaTime, 0);
	    	posX = this.transform.position.x; posY = this.transform.position.y;
	    } else {
		    for(int i = 0; i < 10; i ++) {
			    projectile = Instantiate(projectile41Prefab) as GameObject;
			    this.transform.Translate(dx * Time.deltaTime, dy * Time.deltaTime, 0);
			    posX = this.transform.position.x; posY = this.transform.position.y;
			    projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
			}
	    }
	    if(shotType != 3 && shotType != 9) {
	    	if(posX < -7 || posX > 7 || posY > 6 || posY < -3.7f) //8 = screen width
			Destroy(this.gameObject);
	    } else {
		    if(posX < -7 || posX > 7 || posY < -3.7f)
		    	Destroy(this.gameObject);
	    }
  
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
	    if (coll.gameObject.name == "Hildegarde")
	    {
		Hildegarde hildegarde = coll.GetComponent<Hildegarde>();
		hildegarde.death = true;
		Destroy(this.gameObject);
	    }
    }
}
