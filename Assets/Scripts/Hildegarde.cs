using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hildegarde : MonoBehaviour
{
	int frameTicks;
	float animWobble;
	private Animator anim;
	int attackDirection;
	float attackDirectionTicks;
	float attackDirectionInterval = 0.08f;
	[SerializeField] GameObject arrowPrefab;
	private GameObject arrow;
	int shootTicks;
	public float shootInterval = 0.08f;
	public bool death;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
	    if(Input.GetAxisRaw("Vertical") > 0 && attackDirection != 1 && attackDirection != 0) {
		    attackDirectionTicks++;
		    if(attackDirectionTicks * Time.deltaTime >= attackDirectionInterval) {
			    attackDirection--;
			    attackDirectionTicks = 0;
		    }
	    }
	    else if(Input.GetAxisRaw("Vertical") < 0 && attackDirection != 5) {
		    attackDirectionTicks++;
		    if(attackDirectionTicks * Time.deltaTime >= attackDirectionInterval) {
			    attackDirection++;
			    attackDirectionTicks = 0;
		    }
	    }
	    anim.SetInteger("attackDirection", attackDirection);
	    frameTicks++;
	    this.transform.Translate(0, -animWobble, 0);
	    animWobble = 0;
	    if(frameTicks * Time.deltaTime >= 0.183f) {
		    animWobble = 0.01f;
		    this.transform.Translate(0, animWobble, 0);
		    frameTicks = 0;
	    }
	    if(Input.GetKey(KeyCode.Z) && attackDirection != 0) {
		    shootTicks++;
		    if(shootTicks * Time.deltaTime >= shootInterval) {
		    	shootTicks = 0;
		    	arrow = Instantiate(arrowPrefab) as GameObject;
		    	HGArrow hgarrow = arrow.GetComponent<HGArrow>();
		    	float spd = hgarrow.getSpeed();
		    	switch(attackDirection) {
				    case 1: arrow.transform.Rotate(0, 0, -90);
					    hgarrow.setVel(0, spd);
					    break;
				    case 2: arrow.transform.Rotate(0, 0, -45);
					    hgarrow.setVel(0, spd); break;
				    case 3: hgarrow.setVel(0, spd); break;
				    case 4: arrow.transform.Rotate(0, 0, 45);
					    hgarrow.setVel(0, spd); break;
				    case 5: arrow.transform.Rotate(0, 0, 90);
					    hgarrow.setVel(0, spd); break;
			    }
			    arrow.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
		    }
	    }
    }
}
