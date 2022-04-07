using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentManager : MonoBehaviour
{
    private List<agentController> childAgents = new List<agentController>();

    [HideInInspector] public List<Vector3> availableTargetPoints;

    private void Awake() {
        refreshChildAgents();
    }

    private void Update() {
        List<float> usedNumbers = new List<float>();
        
        for (int i = 0; i < childAgents.Count; i++) {
            if(!childAgents[i].hasPoint){
                int newPointIndex = Random.Range(101 - childAgents.Count, availableTargetPoints.Count);
                while(usedNumbers.Contains(newPointIndex)){
                    newPointIndex = Random.Range(101 - childAgents.Count, availableTargetPoints.Count);
                }

                usedNumbers.Add(newPointIndex);
                
                Vector3 newPoint = availableTargetPoints[newPointIndex];

                childAgents[i].targetPos = newPoint;
                childAgents[i].pointIndex = newPointIndex;

                childAgents[i].hasPoint = true;
            } else {
                childAgents[i].targetPos = availableTargetPoints[childAgents[i].pointIndex];
            }
        }

    }

    private void refreshChildAgents(){
        foreach(Transform child in transform) {
            childAgents.Add(child.GetComponent<agentController>());
        }
    }
}
