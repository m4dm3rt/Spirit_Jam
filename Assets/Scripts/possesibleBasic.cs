using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class possesibleBasic : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Rigidbody RB;

    bool grounded = false;
    bool possed = false;

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.player.Enable();

        RB = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        foreach (Transform child in transform)
        {
            if(child.tag == "Player"){
                possed = true;
                continue;
            }else{
                possed = false;
            }       
        }
        
        if (possed){
            Vector3 moveDir = calculateDesiredMoveDirection();

            if (grounded){
                grounded = false;
                moveDir = moveDir * 2;

                if(moveDir != Vector3.zero){
                    moveDir += Vector3.up * 2;
                }
            } else {
                moveDir = moveDir / 3;
            }

            RB.AddForce(moveDir, ForceMode.Impulse);    
        }
    }

    private void OnCollisionStay(Collision other) {
        grounded = true;
    }


    private Vector3 calculateDesiredMoveDirection(){
        float moveVertical = inputActions.player.movement.ReadValue<Vector2>().y;
        float moveHorizontal = inputActions.player.movement.ReadValue<Vector2>().x;

        Vector3 camForward = Vector3.forward; //camera.transform.forward;
        Vector3 camRight = Vector3.right; //camera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 desiredMoveDirection = (camForward * moveVertical) + (camRight * moveHorizontal);

        return desiredMoveDirection;
    }
}
