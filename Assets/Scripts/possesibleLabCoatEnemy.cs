using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class possesibleLabCoatEnemy : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Cinemachine.CinemachineFreeLook cinemachineFreeLook;
    private GameObject Player;
    private NavMeshAgent navMeshAgent;

    public GameObject goal1;
    public GameObject goal2;
    public GameObject key;
    public float standStillTime = 2f;
    public bool hasKey = false;

    bool possed = false;
    bool isWaiting = false;

    Vector3 g1;
    Vector3 g2;

    string state = "idle"; //states: idle, wanderG1, wanderG2, runFromPlayer, possessed
    string nextGoal = "G2";

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.player.Enable();

        navMeshAgent = GetComponent<NavMeshAgent>();

        cinemachineFreeLook = FindObjectOfType<Cinemachine.CinemachineFreeLook>();

        g1 = goal1.transform.position;
        g2 = goal2.transform.position;
        goal1.SetActive(false);
        goal2.SetActive(false);

        if (hasKey){
            GameObject keyIndicator = Instantiate(key, new Vector3(this.transform.position.x, this.transform.position.y + 1.7f, this.transform.position.z), Quaternion.identity);
            keyIndicator.transform.parent = this.transform;
        }
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

            state = "possessed";

            navMeshAgent.SetDestination(calculateDesiredMoveDirection() + this.transform.position);
        }else{
            inputActions.player.Disable();
        }


        if (state == "idle"){
            if (!isWaiting){
                StartCoroutine(waitAfterIdle());
            }
        }

        if (state == "wanderG1"){
            navMeshAgent.SetDestination(g1);
            nextGoal = "G2";
        }
        
        if (state == "wanderG2"){
            navMeshAgent.SetDestination(g2);
            nextGoal = "G1";
        }
        
        if (!navMeshAgent.pathPending){
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f){
                    state = "idle";
                }
            }
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
            Player.GetComponent<playerController>().hasKey = hasKey;
            Player.SetActive(true);
            
            possed = false;

            Destroy(this.gameObject);
        }
    }

    private IEnumerator waitAfterIdle(){
        isWaiting = true;
        yield return new WaitForSeconds(standStillTime);

        state = "wander" + nextGoal;
        isWaiting = false;
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
