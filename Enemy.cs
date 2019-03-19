using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * store the enemy attribute
 * collect and record waypoint
 * the enemy moved through the way point.
 * */
public class Enemy : MonoBehaviour
{
	StatsManager statsManager;
	WaveSpawner waveSpawner;

	[Header ("Tweeks")]
	public float cornerThreshHold = 0.5f;
	[Header ("Stats")]
    public float speed = 30f;
	public int goldBase = 10;
	public int startingHealth = 10;
	public int currentMaxHealth;
	public int currentHealth;
	public int bonusHealth;
	[Header("Health Bar")]
	[Header ("For Debugging")]
   	public Vector3 target;
	//For testing purpose set to Public
	public float slowDuration = 0;
	//For testing purpose set to Public
	public float slowAmmount = 0;
	//For testing purpose set to Public
	public bool slowed = false;
	private List<Vector3> points;
	private Renderer rend;
	private Color materialColor;
    private int waypointIndex = 0;
	//For testing purpose set to Public
    public float currentSpeed;
    private float hittime;
	private int hitDeadBy = 0;

    void Start()
    {
        currentSpeed = speed;
		statsManager = StatsManager.instance;
		waveSpawner = WaveSpawner.instance;
		rend= GetComponent<Renderer> ();
		materialColor = rend.material.color;
        GetPoints();
		currentMaxHealth = currentHealth = startingHealth + WaveSpawner.instance.multiplyer*bonusHealth;
        target = points[1];
    }

    /*
     * health minus the damage
     * if healath is zero or negative the enemy would be destoryed
     * */
	public void TakeDamage(int damage)
	{
        currentHealth -= damage;
        rend.material.color = new Color(materialColor.r, (float)currentHealth / currentMaxHealth, (float)currentHealth / currentMaxHealth);

        if (currentHealth <=0)
			Die ();
	}

	public void Slow(float duration, float strength)
    {
		slowDuration = duration;
		slowAmmount = strength;
		slowed = true;
    }

	private void SlowBuff()
    {
		currentSpeed = speed * slowAmmount;
		if (slowDuration >= 0) {
			slowDuration -= Time.deltaTime;
		} else {
			currentSpeed = speed;
			slowed = false;
		}
    }

    /*
     * if enemy die the player can get the gold
     * destory the enemy object
     * */
    void Die()
	{
		hitDeadBy++;
		if (hitDeadBy > 0) 
			Destroy (gameObject);

		if (hitDeadBy == 1) {
			statsManager.UpdateGoldCCount (goldBase);
			statsManager.UpdateEnemiesKilledCount ();
			waveSpawner.DestroyedEnemy ();
		}
	}

    /*
     *collect the way point 
     * */
    private void GetPoints()
    {
        points = new List<Vector3>();
        foreach(Vector3 child in MapGenerator.instance.waypoints)
        {
            points.Add(child);
           // Debug.Log("Added point");
        }
    }

    void FixedUpdate()
    {
		if(slowed){
			SlowBuff ();
		}
        Vector3 dir = target - transform.position;//get the direction between enemy and way point
            
        

        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);//the enemy move to the way point
		transform.LookAt(target);

		//if the enemy move to curret way point get next way point
		if (Vector3.Distance(transform.position, target) <= cornerThreshHold)
        {
            GetNextWaypoint();
        }
    }


    /*
     * read next way point
     * if the next way point is end destory the enemy object
     * */
    void GetNextWaypoint()
    {
		if (waypointIndex >= points.Count - 1)
        {
            Destroy(gameObject);
			waveSpawner.DestroyedEnemy ();
			StatsManager.instance.UpdateLivesCount();
            return;
        }

        ++waypointIndex;

        target = points[waypointIndex];
    }
}
