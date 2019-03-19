using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * store the turret attribute
 * find the enemy
 * when the enemy in the range shoot the bullet
 * */
public class Turret : MonoBehaviour {

    private Transform target;
    [Header("Attribute")]
    public float range = 15f;
    public float fireRate = 1.5f;
    private float fireCountDown = 0f;
    public float turnSpeed = 15f;
	private int sellPrice;
	//This is used for text on sell menu with sellPrice
	public int purchasePrice;
	public bool userControl = false;

	[Header("Laser Pointer Settings")]
	public LineRenderer laserPointer;
	public GameObject laserStartPoint;
	public ParticleSystem laserGlow;
	static private ParticleSystem glowEffect;

	[Header("Tower Controll Settings")]
	public bool canShoot = false;
	public float shootCooldown = 0;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
	public GameObject rangeFinder;
	public Tile currentTile;
    /*
     * allowed into the start method from outside 
     * */
    public void getStart()
    {
        this.Start();
    }

    void Start()
    {
        //Check the parameter is logical
        doCheck(range > 0,"Range must be positive");
        doCheck(fireRate > 0,"Fire Rate must be positive");
        doCheck(turnSpeed > 0, "Turn Speed must be positive");
		sellPrice = (int)(purchasePrice * 0.5);
        UpdateTarget();//find target
		rangeFinder.transform.localScale = new Vector3(range*2f,0.1f,range*2f);//set up ranger finder
		if(glowEffect == null){
			glowEffect = Instantiate(laserGlow);
			glowEffect.Stop ();
		}
			
    }

    public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);//Using enemy tag find all enemies in the map and add to array
        float shortestDistance = Mathf.Infinity;//initial the distance with nearest enemy
        GameObject nearestEnemy = null;//initial nearst enemy

        //the loop read the enemy array
        foreach (GameObject enemy in enemies)
        {
            //calculate the distance between turret and enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //if the distance shorter than current the shortest enemy
            if (shortestDistance > distanceToEnemy)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;//change the nearest enemy to current enemy
            }
        }

        //if nearest enemy is not null and in the fire range
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;//update the turret target transform
        }
        else
        {
            target = null;
        }
    }

    void FixedUpdate()
    {
		if (userControl) {
			

			float distance = 100;
			RaycastHit hit;
//			laserPointer.enabled = true;
			Vector3 currentLaserStart = laserStartPoint.transform.position;
			Physics.Raycast (currentLaserStart, laserStartPoint.transform.forward.normalized, out hit);

			if (hit.collider) {
//				if (!hit.collider.gameObject.CompareTag ("Projectiles"))
					distance = hit.distance;
			}
			laserPointer.SetPosition (0, currentLaserStart);
			laserPointer.SetPosition (1, currentLaserStart + laserStartPoint.transform.forward.normalized * distance);
			glowEffect.transform.position = laserPointer.GetPosition (1);
			if (shootCooldown >= 0) {
				canShoot = false;
				shootCooldown -= Time.deltaTime;
			}
			else
				canShoot = true;

		} else {
//			laserPointer.enabled = false;
			//if no target or current target out of the turret range
			if (target == null || Vector3.Distance (target.transform.position, transform.position) > range) {
				UpdateTarget ();//find new target
				return;
			}

			Vector3 dir = target.position - partRotate.transform.position;//find the direction between turret and enemy
			Quaternion lookRotation = Quaternion.LookRotation (dir);//find the enemy forward
			Vector3 rotation = Quaternion.Lerp (partRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;//set up rotation attribute
			partRotate.rotation = Quaternion.Euler (rotation.x, rotation.y, 0f);//the rotated part of turret rotate to the enemy

			if (fireCountDown <= 0f) {//if fire countdown less than zero
				Shoot ();//shooting
				fireCountDown = 1f / fireRate;//reset the fire countdown
			}
			else
				fireCountDown -= Time.deltaTime;
		}

    }

	//Tis is used so that when clicking on towers, it would bring up the menu as well so normal raycasts could still hit it.
	void OnMouseUpAsButton()
	{
		OpenUpgradeMenu ();
	}

	public void OpenUpgradeMenu()
	{
		if (EventSystem.current.IsPointerOverGameObject() ||
			EventSystem.current.currentSelectedGameObject != null) {
			return;
		}

		BuildManager.instance.SelectedTile(currentTile);
	}
//
//	void OnMouseUpAsButton()
//	{
//        //if mouse is opeating on other object or there is other object between the turret and camera , the method would be interrupted
//        if (EventSystem.current.IsPointerOverGameObject() ||
//			EventSystem.current.currentSelectedGameObject != null) {
//			return;
//        }
////		Debug.Log ("draw circle");
//		if (rangeFinder.activeSelf) {
//			rangeFinder.SetActive (false);
//		}
//		else
//			rangeFinder.SetActive (true);
//
//	}

    /*
     * generate the bullet
     * */
    public void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);//generate the bullet
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        
        if (bullet != null)
        {
			if (userControl) 
				bullet.Fire (firePoint.transform.forward);
			else
            	bullet.Seek(target);
        }

    }

	public void ActivateControl()
	{
		userControl = true;
		laserPointer.enabled = true;
		glowEffect.Play ();
	}

	public void ResetControl()
	{
		userControl = false;
		laserPointer.enabled = false;
		glowEffect.Stop ();
	}

    /*
     * check the parameter is logical
     * */
    private void doCheck(bool check, string msg)
    {
        if (!check)
        {
            throw new System.ArgumentException(msg);
        }
    }

	public int GetSellPrice()
	{
		sellPrice = (int)(purchasePrice * 0.5);
		return sellPrice;
	}

	public void SellTurret()
	{
		DestroyImmediate (gameObject,true);
	}

	public void SetTile(Tile tile)
	{
		currentTile = tile;
	}
}
