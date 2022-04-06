using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private CinemachineFreeLook cinemachine;

    private bool camLocked = true;
    private float currentCamDistance = 0.5f;
    private float targetCamDistance = 0.5f;

    public float horizontalCameraSensitivity = 250f;
    public float verticalCameraSensitivity = 50f;
    public float camLerpSpeed = 0.5f;

    public bool freeLookCam = false;



    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.player.Enable();

        TryGetComponent<CinemachineFreeLook>(out cinemachine);
    }

    private void OnEnable() {
        inputActions.player.cameraUnlock.started += onCameraUnlock;
        inputActions.player.cameraUnlock.canceled += onCameraLock;  
    }
    
    private void OnDisable() {
        inputActions.player.cameraUnlock.started -= onCameraUnlock;
        inputActions.player.cameraUnlock.canceled -= onCameraLock;
    }



    private void Update() {
        float scroll = inputActions.player.scroll.ReadValue<float>();

        if  (!camLocked){
            if (scroll == -120){
                upCameraDistance();
            }

            if (scroll == 120){
                downCameraDistance();
            }

            if (freeLookCam){
                cinemachine.m_YAxis.m_MaxSpeed = verticalCameraSensitivity;
            } else {
                cinemachine.m_YAxis.m_MaxSpeed = 0;

                //lerps cinemachine y axis to target value, this is the way i do different camera angles
                currentCamDistance = Mathf.MoveTowards(currentCamDistance, targetCamDistance, camLerpSpeed * Time.deltaTime);
                cinemachine.m_YAxis.Value = currentCamDistance;
            }
        } else {
            cinemachine.m_YAxis.m_MaxSpeed = 0;
        }
    }


    public void onCameraUnlock(InputAction.CallbackContext context){
        camLocked = false;
        cinemachine.m_XAxis.m_MaxSpeed = horizontalCameraSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void onCameraLock(InputAction.CallbackContext context){
        camLocked = true;
        cinemachine.m_XAxis.m_MaxSpeed = 0f;
        Cursor.lockState = CursorLockMode.None;
    }


    public void upCameraDistance(){
        if (!camLocked && cinemachine.m_YAxis.Value < 1){
            targetCamDistance += 0.5f;
        }
    }
    
    public void downCameraDistance(){
        if (!camLocked && cinemachine.m_YAxis.Value > 0){
            targetCamDistance -= 0.5f;
        }
    }
}
