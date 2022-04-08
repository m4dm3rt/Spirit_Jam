using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            if (other.gameObject.transform.parent.GetComponent<playerController>().hasKey){
                other.gameObject.transform.parent.GetComponent<playerController>().hasKey = false;
                Destroy(this.gameObject);
            }
        }
    }
}
