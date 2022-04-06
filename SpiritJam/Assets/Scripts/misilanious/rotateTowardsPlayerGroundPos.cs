using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTowardsPlayerGroundPos : MonoBehaviour
{
    private playerController playerController;

    public float turnLerpSpeed = 15f;
    public bool invertY = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 groundPos = playerController.groudPos;
        Vector3 thisPos = this.transform.position;

        Vector3 targetDir = groundPos - thisPos;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, 1f, 0.0f);

        Quaternion targetRotation = Quaternion.LookRotation(newDirection);

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, turnLerpSpeed * Time.deltaTime);
    }
}
