using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy1 : MonoBehaviour
{
	//temporary
	int difficulty, score, actualScore;
	//temporary;
	[SerializeField] TMP_Text scoreLabel;
	[SerializeField] GameObject projectile1Prefab;
	[SerializeField] GameObject projectile2Prefab;
	[SerializeField] GameObject projectile21Prefab;
	[SerializeField] GameObject projectile3Prefab;
	[SerializeField] GameObject projectile31Prefab;
	[SerializeField] GameObject projectile4Prefab;
	GameObject projectile, projectile1;
	GameObject hildegarde;
	public int lifeTime = 180;
	float defaultPosX, defaultPosY, posX, posY, dx, dy, distance, newDist, renderAngle, randX, randY, shotRandX, shotRandY, actionTicks, shootTicks, shootTicks1, moveTicks;
	int moveThreshold, shotCount, shootAttack4Rand;
	public int pattern = 1;
	int mCurrentHitPoints = 200;
	int mMaxHitPoints = 200;
	bool enemyDead, changeMove, moved, shootReturn, rolledShootAttack4Rand;
	public float projectile1Interval = 0.5f;
	public float projectile2Interval = 0.16f;
	public float projectile21Interval = 0.03f;
	public float projectile3Interval = 0.04f;
	public float projectile31Interval = 0.04f;
	public float projectile4Interval = 0.5f;
	float randomXRange1 = -6.4f; float randomXRange2 = 6f;
	float randomYRange1 = 1.8f; float randomYRange2 = -1f;
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
	    scoreLabel.text = $"Score....{actualScore}";
	    defaultPosX = this.transform.position.x;
	    defaultPosY = this.transform.position.y;
	    posX = defaultPosX; posY = defaultPosY;
	    randX = 0.0f;
	    randY = 0.0f;
	    pattern = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
	posX = this.transform.position.x; posY = this.transform.position.y;
	actionTicks += Time.deltaTime;
	if(actionTicks >= lifeTime || enemyDead) {
		dx = 0; dy = 0;
		renderAngle += 30.0f;
		this.transform.Rotate(0, 0, -renderAngle);
		if(renderAngle >= 720) {
			hildegarde = GameObject.Find("Hildegarde");
			Hildegarde hildegardeScript = hildegarde.GetComponent<Hildegarde>();
			if(pattern == 1) {}
				//clear Projectiles
			enemyDead = false;
			randX = Random.Range(randomXRange1, randomXRange2);
			randY = Random.Range(randomYRange1, randomYRange2);
			pattern = Random.Range(1, 5);
			changeMove = true; actionTicks = 0;
			shootTicks = 0; moveThreshold = 0;
			shootTicks1 = 0; shotCount = 0;
			moveTicks = 0; shootReturn = false;
			renderAngle = 0; rolledShootAttack4Rand = false;
			if(!hildegardeScript.death)
				actualScore += 1000;
			if(actualScore == 5000)
				difficulty++;
			else if(actualScore == 10000)
				difficulty++;
			mCurrentHitPoints = mMaxHitPoints;
			scoreLabel.text = $"Score.....{actualScore}";
		}
	} else {
		switch(pattern) {
			case 1:	if(difficulty == 0)
					moveThreshold = 3;
				else if(difficulty == 1)
					moveThreshold = 2;
				else if(difficulty == 2)
					moveThreshold = 1;
				moveTicks += Time.deltaTime;
				if(moveTicks >= moveThreshold) {
					moveTicks = 0;
					changeMove = true;
					randX = Random.Range(randomXRange1, randomXRange2);
					randY = Random.Range(randomYRange1, randomYRange2);
				}
				moveToXY(randX, randY, 6.0f);
				shootTicks += Time.deltaTime;
				if(shootTicks >= projectile1Interval) {
					shootTicks = 0;
					projectile = Instantiate(projectile1Prefab) as GameObject;
					projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
				}
				break;
			case 2: moveToXY(0f, 0f, 6.0f);
				if(this.transform.position.x < 1 && this.transform.position.x > -1) {
					shootTicks += Time.deltaTime;
					shootTicks1 += Time.deltaTime;
					//Shot draws a line through the screen and then returns with the same motion
					if(!shootReturn) {
						moveThreshold += 5; 
						if(moveThreshold/100.0f >= 6)
							shootReturn = true;
					} else {
						moveThreshold -= 5;
						if(moveThreshold/100.0f <= -6)
							shootReturn = false;
					}
					if(shotCount < 5 && shootTicks1 >= projectile21Interval) {
						projectile1 = Instantiate(projectile21Prefab) as GameObject;
						projectile1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
						shootTicks1 = 0;

						shotCount++;
					}
					if(projectile1 == null)
						shotCount = 0;
					if(shootTicks >= projectile2Interval) {
						shootTicks = 0;
						projectile = Instantiate(projectile2Prefab) as GameObject;
						ProjectileMove projectile2 = projectile.GetComponent<ProjectileMove>();
						projectile2.targetX = moveThreshold/100.0f;
						projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
						projectile = Instantiate(projectile2Prefab) as GameObject;
						projectile2 = projectile.GetComponent<ProjectileMove>();
						projectile2.targetX = -moveThreshold/100.0f;
						projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
			}
				}
				break;
			case 3: moveToXY(0, -1.5f, 6.0f);
				shootTicks += Time.deltaTime; 
				shootTicks1 += Time.deltaTime;
				if(shootTicks >= projectile3Interval) {
					shootTicks = 0;
					projectile = Instantiate(projectile3Prefab) as GameObject;
					projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
				}

				if(projectile1 == null) {
					shotCount = 0;
					shotRandX = Random.Range(-1.0f, 1.0f);
					shotRandY = Random.Range(0.0f, 6.0f);
				}

				if(shotCount < 10 && shootTicks1 >= projectile31Interval) {
					projectile1 = Instantiate(projectile31Prefab) as GameObject;
					ProjectileMove projectile31 = projectile1.GetComponent<ProjectileMove>();
					projectile31.targetX = shotRandX;
					projectile31.targetY = shotRandY;
					projectile1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
					shootTicks1 = 0;

					shotCount++;
				}
				break;
			default: moveToXY(0f, 0f, 6.0f);
				 shootTicks1 += Time.deltaTime;
				 if(this.transform.position.x < 1 && this.transform.position.x > -1) {
				 	if(shotCount < 3 && shootTicks1 >= projectile31Interval) {
						if(!rolledShootAttack4Rand) {
							shootAttack4Rand = Random.Range(0, 2);
							rolledShootAttack4Rand = true;
						}
						projectile1 = Instantiate(projectile31Prefab) as GameObject;
						ProjectileMove projectile31 = projectile1.GetComponent<ProjectileMove>();
						if(shootAttack4Rand == 0) {
							projectile31.targetX = -6;
							projectile31.targetY = 2;
						} else {
							projectile31.targetX = 6;
							projectile31.targetY = 2;
						}
						projectile1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
						shootTicks1 = 0;

						shotCount++;
					}
					if(projectile1 == null) {
						if(!shootReturn) {
							moveTicks += Time.deltaTime + 1; 
							if(moveTicks >= 12)
								shootReturn = true;
						} else {
							moveTicks -= Time.deltaTime + 1;
							if(moveTicks <= -12)
								shootReturn = false;
						}
						projectile = Instantiate(projectile4Prefab) as GameObject;
						ProjectileMove projectile4 = projectile.GetComponent<ProjectileMove>();
						if(shootAttack4Rand == 0)
							projectile4.targetX = moveTicks + 15 - 4.5f;
						else
							projectile4.targetX = 6 - moveTicks - 15;
						projectile.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
					}
				 }
				 break;
		}
	}
    }
}
