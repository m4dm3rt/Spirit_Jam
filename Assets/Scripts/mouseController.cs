using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private GameObject PosIndicator;
    private groundArrowAim groundArrowAim;
    private playerController playerController;

    public GameObject PosIndicatorOG;

    public LayerMask layerMask;

    private bool cameraLocked = false;

    public float maxDistance = 20f;
    public float posIndicatorLerpSpeed = 50f;

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.player.Enable();

        PosIndicator = Instantiate(PosIndicatorOG, new Vector3(0f, 0f, 0f), Quaternion.identity);
        PosIndicator.SetActive(false);

        groundArrowAim = PosIndicator.GetComponent<groundArrowAim>();

        TryGetComponent<playerController>(out playerController);
    }

    private void OnEnable() {
        inputActions.player.cameraUnlock.started += changeCameraLockState;
        inputActions.player.cameraUnlock.canceled += changeCameraLockState;
    }
    
    private void OnDisable() {
        inputActions.player.cameraUnlock.started -= changeCameraLockState;
        inputActions.player.cameraUnlock.canceled -= changeCameraLockState;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputActions.player.mousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        Vector3 targetPos;

        if (hasHit) {
            targetPos = hit.point;

            PosIndicator.transform.position = hit.point;

            if (!cameraLocked) {
                PosIndicator.SetActive(true);    
            } else {
                PosIndicator.SetActive(false);
            }
        } else {
            PosIndicator.SetActive(false);
            targetPos = this.transform.position;
        }

        if (!playerController.onGround){
            PosIndicator.SetActive(false);
        }

        PosIndicator.transform.position = Vector3.Lerp(PosIndicator.transform.position, targetPos, posIndicatorLerpSpeed * Time.deltaTime);

        groundArrowAim.maxDistance = maxDistance;
    }

    private void changeCameraLockState(InputAction.CallbackContext context){
        cameraLocked = !cameraLocked;
    }
}
