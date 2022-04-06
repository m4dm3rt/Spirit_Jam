using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public float speed;
    public GameObject Bullet;
    private float pitch;
    public AudioSource ShootSound;
    private GameObject Target;
    private Transform Direction;
    private Rigidbody rb;
    Vector3 moveDirection;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Target = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (Target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y,moveDirection.z);
        pitch = Random.Range(0.9f, 1.3f);
        ShootSound.pitch = pitch;
        ShootSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet.transform.position = new Vector2(transform.position.x + (Time.deltaTime * speed), transform.position.y);
    }
}
