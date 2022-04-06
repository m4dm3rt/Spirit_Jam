using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateToClosestNormal : MonoBehaviour
{
    private GameObject camera;

    public float lerpSpeed = 20f;

    private void Start() {
        camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion targetRot = Quaternion.identity;

        Ray ray = new Ray(camera.transform.position, (this.transform.position - camera.transform.position));
        if (Physics.Raycast(ray, out RaycastHit hit)){
            if (Vector3.Distance(this.transform.position, hit.point) < 0.3f){
                targetRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRot, lerpSpeed * Time.fixedDeltaTime);
    }
}
