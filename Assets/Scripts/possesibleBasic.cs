using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class possesibleBasic : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Cinemachine.CinemachineFreeLook cinemachineFreeLook;
    private Rigidbody RB;
    private GameObject Player;

    bool grounded = false;
    bool possed = false;

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.player.Enable();

        cinemachineFreeLook = FindObjectOfType<Cinemachine.CinemachineFreeLook>();

        RB = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        foreach (Transform child in transform)
        {
            if(child.tag == "Player"){
                Player = child.gameObject;
                child.gameObject.SetActive(false);
                possed = true;
                continue;
            }else{
                possed = false;
            }       
        }
        
        if (possed){
            inputActions.player.Enable();
            cinemachineFreeLook.LookAt = this.gameObject.transform;
            cinemachineFreeLook.Follow = this.gameObject.transform;

            Vector3 moveDir = calculateDesiredMoveDirection() * 3;

            if (grounded){
                if(moveDir != Vector3.zero){
                    moveDir += Vector3.up * 2;
                }

            } else {
                moveDir = moveDir / 3;
            }

            RB.AddForceAtPosition(moveDir, new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, this.transform.position.z), ForceMode.Impulse);
        }else{
            inputActions.player.Disable();
        }
        
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("possibleObjectGround")){
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("possibleObjectGround")){
            grounded = false;
        }
    }

    private void OnEnable() {
        inputActions.player.posses.performed += dePosses;    
    }
    
    private void OnDisable() {
        inputActions.player.posses.performed -= dePosses;    
    }

    public void dePosses(InputAction.CallbackContext context){
        if (possed){
            Player.transform.position = this.transform.position;
            Player.transform.rotation = Quaternion.identity;
            
            cinemachineFreeLook.LookAt = Player.gameObject.transform;
            cinemachineFreeLook.Follow = Player.gameObject.transform;
           
            Player.transform.parent = null;
            Player.SetActive(true);
            
            possed = false;
        }
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
