using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Head_Rotation : MonoBehaviour
{
    public GameObject Turret_Head;
    private GameObject Player;

    public float TurnSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Player_Position = Player.transform.position;
        Vector3 Turret_Head_Position = Turret_Head.transform.position;

        Vector3 targetDir = Player_Position - Turret_Head_Position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, -1f, 0.0f);

        Quaternion targetRotation = Quaternion.LookRotation(newDirection);

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
    }
}