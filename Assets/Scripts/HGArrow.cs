using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGArrow : MonoBehaviour
{
	public float speed = 3.6f;
	public int damage = 1;
	public float posX, posY, dx, dy;
	public void setVel(float velX, float velY) {
		dx = velX; dy = velY;
	}
	public float getSpeed() {
		return speed;
	}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	this.transform.Translate(dx * Time.deltaTime, dy * Time.deltaTime, 0);
        posX = this.transform.position.x; posY = this.transform.position.y;
	if(posX < -8 || posX > 8 || posY > 6 || posY < -4) //8 = screen width
		Destroy(this.gameObject);
    }
}
