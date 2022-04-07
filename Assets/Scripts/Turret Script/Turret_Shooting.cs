using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Turret_Shooting : MonoBehaviour
{
    public Animator TurretAnimator;
    public GameObject Bullet;
    public GameObject BulletSpawner;
    public float cooldown;
    public float range; 
    [SerializeField]
    private bool PlayerInRange;
    private float firerate;
    private GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        firerate = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Sqrt(((BulletSpawner.transform.position.x-Player.transform.position.x)*(BulletSpawner.transform.position.x - Player.transform.position.x))+ ((BulletSpawner.transform.position.y - Player.transform.position.y) * (BulletSpawner.transform.position.y - Player.transform.position.y))+ ((BulletSpawner.transform.position.z - Player.transform.position.z) * (BulletSpawner.transform.position.z - Player.transform.position.z))) <= range)
        {
            PlayerInRange = true;
        }
        else
        {
            PlayerInRange = false;
        }


        firerate -= Time.deltaTime;
        if (firerate<=0&&PlayerInRange)
        {
            TurretAnimator.SetTrigger("Shooting");
        }
    }
    void shoot()
    {
        Instantiate(Bullet, BulletSpawner.transform.position, Quaternion.identity);
        firerate = cooldown;
    }
}
