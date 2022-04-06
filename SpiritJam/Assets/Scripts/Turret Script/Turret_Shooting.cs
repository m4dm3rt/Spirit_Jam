using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform BulletSpawner;
    public float cooldown;
    private float firerate;


    // Start is called before the first frame update
    void Start()
    {
        firerate = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        firerate -= Time.deltaTime;
        if (firerate<=0)
        {
            Instantiate(Bullet, BulletSpawner.transform.position, Quaternion.identity);
            firerate = cooldown;
        }
    }
}
