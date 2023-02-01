using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
	//temporary
	int difficulty, score, actualScore;
	//temporary;
	[SerializeField] GameObject projectile1Prefab;
	private GameObject projectile;
	float defaultPosX, defaultPosY, posX, posY, dx, dy, distance, newDist, renderAngle, randX, randY;
	int actionTicks, shootTicks, moveThreshold;
	int pattern = 1;
	int mCurrentHitPoints = 100;
	int mMaxHitPoints = 100;
	bool enemyDead, changeMove, moved;
	public float projectile1Interval = 0.5f;
	float randomXRange1 = -6.4f; float randomXRange2 =  6;
	float randomYRange1 = 2.5f; float randomYRange2 = -(Screen.height/100 - 6.5f);
	public void takeDamage() {
	       if(mCurrentHitPoints - 1 <= 0) {
		       enemyDead = true;
		       mCurrentHitPoints = mMaxHitPoints;
	       } else
		       mCurrentHitPoints--;
	}
	private void moveToXY(float x, float y, float speed) {
		if(changeMove) {
			changeMove = false;
			moved = false;
		}
		if(!moved) {
			float side1 = (y - posY) * 100;
			float side2 = (posX - x) * 100;
			float r = Mathf.Sqrt(side1 * side1 + side2 * side2);
			float sinAngle = (side1/r);
			float cosAngle = (side2/r);
			distance = r;
			if(posX > x)
				dx = -speed*cosAngle;
			else {
				cosAngle = -cosAngle;
				dx = speed*cosAngle;
			}
			if(posY < y)
				dy = speed*sinAngle;
			else {
				sinAngle = -sinAngle;
				dy = -speed*sinAngle;	
			}
			moved = true;
		}
		if(!(distance <= 1)) {
			float distanceX = (posX - x)*100;
			float distanceY = (y - posY)*100;
			newDist = Mathf.Sqrt(distanceX*distanceX + distanceY*distanceY);
			if(!(newDist < 0.1f)) {
				float speedMod = newDist/distance;
				if(speedMod > 1)
					speedMod = 0.1f;
				dx *= speedMod;	
				dy *= speedMod;
				this.transform.Translate(dx * Time.deltaTime, dy * Time.deltaTime, 0);
				dx /= speedMod;
				dy /= speedMod;
			} else {
				distance = 0;
				dx = 0f; dy = 0f;
			}
		}
	}	
    // Start is called before the first frame update
    void Start()
    {

	    defaultPosX = this.transform.position.x;
	    defaultPosY = this.transform.position.y;
	    posX = defaultPosX; posY = defaultPosY;
	    randX = 0.0f;
	    randY = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
	posX = this.transform.position.x; posY = this.transform.position.y;
	Hildegarde hildegarde = gameObject.GetComponent<Hildegarde>();
	actionTicks++;
	if(actionTicks * Time.deltaTime >= 1800 || enemyDead) {
		dx = 0; dy = 0;
		renderAngle += 30.0f;
		this.transform.Rotate(0, 0, -renderAngle);
		if(renderAngle >= 720.0) {
			if(pattern == 1)
				//clear Projectiles
			enemyDead = false;
			randX = Random.Range(randomXRange1, randomXRange2);
			randY = Random.Range(randomYRange1, randomYRange2);
			//pattern = Random.Range(0, 3);
			changeMove = true; actionTicks = 0;
			renderAngle = 0;
			if(!hildegarde.death)
				actualScore += 1000;
			if(actualScore == 5000)
				difficulty++;
			else if(actualScore == 10000)
				difficulty++;
			mCurrentHitPoints = mMaxHitPoints;
		}
	} else {
		switch(pattern) {
			case 1:	if(difficulty == 0)
					moveThreshold += 1;
				else if(difficulty == 1)
					moveThreshold += 2;
				else if(difficulty == 2)
					moveThreshold += 3;
				if(moveThreshold * Time.deltaTime >= 3) {
					moveThreshold = 0;
					changeMove = true;
					randX = Random.Range(randomXRange1, randomXRange2);
					randY = Random.Range(randomYRange1, randomYRange2);
				}
				moveToXY(randX, randY, 6.0f);
				shootTicks++;
				if(shootTicks * Time.deltaTime >= projectile1Interval) {
					shootTicks = 0;
					projectile = Instantiate(projectile1Prefab) as GameObject;
					projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
				}
				break;
			/*case 1: moveToXY(SCREEN_WIDTH/2 - mBox.w/2, 50, 6.0);
				 projectile2->shootEnemy2(mBox.x, mBox.y + mBox.h/2, hildegarde); 
				projectile2->checkDie(hildegarde);
				projectile2->checkDie1(hildegarde);
				break;
			case 2: moveToXY(SCREEN_WIDTH/2 - mBox.w/2, SCREEN_HEIGHT/2, 6.0);
				 projectile3->shootEnemy3(mBox.x, mBox.y + mBox.h/2, hildegarde);
				projectile3->checkDie(hildegarde);
				break;
			default: moveToXY(SCREEN_WIDTH/2 - mBox.w/2, 50, 6.0);
				 if(fabs(mBox.x - (SCREEN_WIDTH/2 - mBox.w/2)) < 100) {
				 	projectile2->shootEnemy4(mBox.x, mBox.y + mBox.h/2, hildegarde);
				 	projectile2->checkDie(hildegarde);
				 	projectile2->checkDie(hildegarde);
				 }
				 break;*/
		}
	}
    }
}
