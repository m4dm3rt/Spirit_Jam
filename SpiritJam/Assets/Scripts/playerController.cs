using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Rigidbody RB;
    private Camera camera;

    [Header("Float Parameters")]
    public float maxRideDistance = 3f;
    public float rideHeight = 0.8f;
    public float rideSpringStreght = 1f;
    public float rideSpringDamper = 1f;
    public LayerMask isGroundLayerMask;

    [Space, Header("Movement")]
    public float maxSpeed = 8f;
    public float acceleration = 200f;
    public AnimationCurve accelarationFactorFromDot;
    public float maxAccelerationForce = 15f;
    public AnimationCurve maxAccelarationFactorFromDot;
    public float maxGroundAngle = 60f;
    public float slopeCheckDistance = 1f;
    public float slopeCheckRayLenght = 1f;

    [Space, Header("Visual stuff")]
    public float turnLerpSpeed = 1f;


    private Vector3 moveDirection;
    private Vector3 goalVel;

    [HideInInspector]
    public Vector3 groudPos;
    [HideInInspector]
    public bool onGround = false;

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.player.Enable();

        RB = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    private void OnEnable() {
        inputActions.player.shoot.performed += onShoot;    
    }
    
    private void OnDisable() {
        inputActions.player.shoot.performed -= onShoot;    
    }


   private void FixedUpdate() {
        downFireRaycast(out bool hasHit, out RaycastHit hit);
        
        moveDirection = calculateDesiredMoveDirection();

        float slope = slopeInFrontOfPlayer();
        if (slope < maxGroundAngle || slope == 404f){
            movePlayer();
        } else{
            movePlayer(0);
        }

        if (hasHit){    
            if (hit.distance < maxRideDistance){
                floatAboveGround(hit);
                onGround = true;
            }else {
                onGround = false;
            }
        } else {
            onGround = false;
        }
        
        if (moveDirection != Vector3.zero){
            rotatePlayer();
        }
    }

    public void onShoot(InputAction.CallbackContext context){
        print("shots fired");
    }

    public void downFireRaycast(out bool hasHit, out RaycastHit rayhit){
        var ray = new Ray(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Vector3.down);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 10, isGroundLayerMask)){
            groudPos = hit.point;
            hasHit = true;
            rayhit = hit;
        } else {
            hasHit = false;
            rayhit = hit;
        }
    }

    public float slopeInFrontOfPlayer(){
        Vector3 rayPos = transform.position + transform.forward;
        rayPos = Vector3.Lerp(rayPos, this.transform.position, slopeCheckDistance);

        var ray = new Ray(new Vector3(rayPos.x, rayPos.y, rayPos.z), Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, slopeCheckRayLenght, isGroundLayerMask)){
            return Vector3.Angle(hit.normal, Vector3.up);
        } else {
            return 404f;   
        }
    }

    private void floatAboveGround(RaycastHit hit){
        Vector3 vel = RB.velocity;
        Vector3 otherVel = Vector3.zero;
        Rigidbody hitBody = hit.rigidbody;

        if (hitBody != null){
            otherVel = hitBody.velocity;
        }

        float rayDirVel = Vector3.Dot(Vector3.down, vel);
        float otherDirVel = Vector3.Dot(Vector3.down, otherVel);

        float relVel = rayDirVel - otherDirVel;

        float x = hit.distance - rideHeight;
        float springForce = (x * rideSpringStreght) - (relVel * rideSpringDamper);

        RB.AddForce(Vector3.down * springForce);
    }

    private void rotatePlayer(){
        Vector3 playerTransform = this.transform.rotation.eulerAngles;
        float targetAngle = Vector3.SignedAngle(moveDirection, Vector3.back, Vector3.up) + 180;

        Quaternion targetRotation = Quaternion.Euler(playerTransform.x, -targetAngle, playerTransform.z);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, turnLerpSpeed * Time.deltaTime);
    }

    private void movePlayer(float accelMod = 1f){
        Vector3 unitVel = goalVel.normalized;
        Vector3 unitGoal = moveDirection.normalized;
        Vector3 idealVel = moveDirection * maxSpeed;
        
        float velDot = Vector3.Dot(unitGoal, unitVel);
        float accel = acceleration * accelarationFactorFromDot.Evaluate(velDot);
        float maxAccel = maxAccelerationForce * maxAccelarationFactorFromDot.Evaluate(velDot);
        
        goalVel = (Vector3.MoveTowards(goalVel, idealVel, accel * Time.fixedDeltaTime)) * accelMod;
        Vector3 neededAccel = (goalVel - RB.velocity)  / Time.fixedDeltaTime;
        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);

        RB.AddForce(new Vector3(neededAccel.x, 0, neededAccel.z));
    }

    private Vector3 calculateDesiredMoveDirection(){
        float moveVertical = inputActions.player.movement.ReadValue<Vector2>().y;
        float moveHorizontal = inputActions.player.movement.ReadValue<Vector2>().x;

        Vector3 camForward = camera.transform.forward;
        Vector3 camRight = camera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 desiredMoveDirection = (camForward * moveVertical) + (camRight * moveHorizontal);

        return desiredMoveDirection;
    }
}
