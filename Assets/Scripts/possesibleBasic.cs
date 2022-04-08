using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class possesibleBasic: MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Cinemachine.CinemachineFreeLook cinemachineFreeLook;
    private GameObject Player;

    bool possed = false;

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.player.Enable();

        cinemachineFreeLook = FindObjectOfType<Cinemachine.CinemachineFreeLook>();        
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
        }else{
            inputActions.player.Disable();
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
}
