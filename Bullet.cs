using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * store the bullet type
 * seek the enemy
 * hit the enemy
 * */
public class Bullet : MonoBehaviour
{

    private Transform target;

	public int hitDamage = 5;
	public int aoeDamage = 3;
    public float speed = 70f;
    public float explosionRadius = 0f;
	public bool aoe = false;
    public bool slow = false;
	public bool aoeSlow = false;
	public float slowDuration = 0;
	public float aoeSlowDuration = 0;
	public float slowStrength = 0;
	public float aoeSlowStrength = 0;
    public float deltaTime = 0;
	public float projectileLife;
    public GameObject impacteffect;
	public bool userControl = false;
	public Vector3 shotDirection;
	private int objectsHit = 0;


	void Start()
	{
		aoeSlow = aoe && slow;
	}
    /*
     * get the target from turret
     * @param _target get the target's position
     * */
    public void Seek(Transform _target)
    {
        target = _target;
    }

	public void Fire(Vector3 dir)
	{
		userControl = true;
		shotDirection = dir;
	}

	void OnCollisionEnter(Collision col)
	{
//		Debug.Log ("hit?");
		objectsHit ++;
		if (userControl && objectsHit ==1) {
			
			if (col.gameObject.tag != "Enemy" && col.gameObject.tag != "Projectiles") {
				Destroy (gameObject);
				if (aoe) {
					Explode ();
//					Debug.Log (col.gameObject.name + " AOE");
					PlayImpact ();
				} 
				else
//					Debug.Log (col.gameObject.name);
					PlayImpact ();
			} else {
				target = col.gameObject.transform;
				HitTarget ();
			}
		} else {
			if(col.gameObject.transform == target)
				HitTarget();
		}

	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (userControl) {
			float distanceThisFrame = speed * Time.deltaTime;
			if (deltaTime > projectileLife)
			{
				Destroy(gameObject);
				if (aoe) {
					Explode ();
					PlayImpact ();
				}
				return;
			}
			deltaTime += Time.deltaTime;
			transform.Translate(shotDirection.normalized * distanceThisFrame, Space.World);
//			transform.LookAt (shotDirection);
		}
		else{
        //if not target
			if (target == null || deltaTime > projectileLife)
	        {
				Destroy(gameObject);
				if (aoe) {
					Explode ();
					PlayImpact ();
				}
	            return;
	        }
			deltaTime += Time.deltaTime;
	        //get the direction between bullet and target
	        Vector3 dir = target.position - transform.position;

	        //distance between bullet and target
	        float distanceThisFrame = speed * Time.deltaTime;

	        //if bullet get to enemy
//	        if (dir.magnitude <= distanceThisFrame)
//	        {
//	            HitTarget();
//	            return;
//	        }

			transform.Translate(transform.forward * distanceThisFrame, Space.World);//bullet move to target
			transform.LookAt(target);//the bullet look at the target
			}
    }
		
    /*
     * hit the target
     * */
    void HitTarget()
    {
        //if the bullet is exploder  
		if (aoe) {
			Explode (target);
			PlayImpact ();
		}

        //if is normal bullet
        else if(!aoe)
        {
            Damage(target,false);
			PlayImpact();
        }

        Destroy(gameObject);
    }

    /*
     * if rocket hit the enemy
     * find the enemy in the explosion range
     * */
	void Explode(Transform enemy)
    {

		Damage (target, false);//hit the main target
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius*2);//collect enemy in the esplosion range
        foreach (Collider collider in colliders)
        {
            //make damage to other enemies in the esplosion range
            if (collider.transform != enemy && collider.tag == "Enemy")
            {
				Damage(collider.transform, aoe);
            }
        }
    }

    /*
     * if rocket hit null target
     * find the enemy in the explosion range
     * */
    void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);//collect the enemies in the range
		foreach (Collider collider in colliders)
		{
			if (collider.transform != null && collider.tag == "Enemy")
			{
				Damage(collider.transform, aoe);
			}
		}
	}

    /*
     * make the damage to the enemies
     * */
	void Damage(Transform enemy, bool aoe)
    {
		Enemy _enemy = enemy.GetComponent<Enemy> ();
		if (_enemy == null) {
//			Debug.Log ("Blew up, no enemy");
			return;
		}


		if (aoe) {
			_enemy.TakeDamage (aoeDamage);
			if (slow)
				_enemy.Slow (aoeSlowDuration, aoeSlowStrength);
		} else {
			_enemy.TakeDamage (hitDamage);
			if (slow)
				_enemy.Slow (slowDuration, slowStrength);
		}
    }

    /*
     * play the hit impact
     * */
	void PlayImpact()
	{
		GameObject impactEffect = Instantiate (impacteffect, transform.position, impacteffect.transform.rotation);
		Destroy (impactEffect, 2f);
	}
}
