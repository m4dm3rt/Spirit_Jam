using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentFollowTarget : MonoBehaviour
{
    private agentManager agentManager;

    public bool showDebugInfo = false;

    public float pointRadius = 1.5f;
    public float lineAmount = 10f;
    public float minDistanceBetweenPoints = 0.5f;

    private List<Vector3> lines = new List<Vector3>();
    private List<Vector3> points = new List<Vector3>();

    private void Awake() {
        agentManager = FindObjectOfType<agentManager>();
    }

    private void Update()
    {
        lines.Clear();
        points.Clear();

        var stepSize = (1 / lineAmount) * pointRadius;

        //generate lines
        for (float i = -pointRadius + stepSize; i < pointRadius - stepSize; i += stepSize)
        {
            float chordLegnth = Mathf.Sqrt((pointRadius * pointRadius) - (i * i));

            Vector3 dir1 = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0) * new Vector3(chordLegnth, 0, i);
            Vector3 dir2 = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0) * new Vector3(-chordLegnth, 0, i);

            Vector3 pos1 = dir1 + this.transform.position;
            Vector3 pos2 = dir2 + this.transform.position;
            
            lines.Add(pos1);
            lines.Add(pos2);
        }

        //generate points on all lines
        for (int i = 0; i < lines.Count; i += 2)
        {
            float thisLineLength = Vector3.Distance(lines[i], lines[i + 1]);
            float thisLineMaxPoints = Mathf.Round(thisLineLength / minDistanceBetweenPoints);

            for (float j = 0; j < 1.000001f; j += 1 / thisLineMaxPoints)
            {
                Vector3 newPoint = Vector3.Lerp(lines[i], lines[i + 1], j);

                points.Add(newPoint);
            }
        }
        
        if (showDebugInfo){
            for (int i = 0; i < lines.Count; i += 2)
            {
                Debug.DrawLine(lines[i], lines[i + 1]);
            }

            for (int i = 0; i < points.Count; i++)
            {
                Debug.DrawLine(points[i], new Vector3(points[i].x, points[i].y + 0.25f, points[i].z), Color.red);
            }
        }

        agentManager.availableTargetPoints = points;
    }
}





/* von felix ein ansatz um weniger daten zu nutzen glaube ich
//generate lines
        for (float i = -pointRadius + stepSize; i < pointRadius - stepSize; i += stepSize)
        {
            float chordLegnth = Mathf.Sqrt((pointRadius * pointRadius) - (i * i));

            Vector3 dir1 = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0) * new Vector3(chordLegnth, 0, i);
            Vector3 dir2 = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0) * new Vector3(-chordLegnth, 0, i);

            Vector3 pos1 = dir1 + this.transform.position;
            Vector3 pos2 = dir2 + this.transform.position;
            
            Vector3 dir = pos2 - pos1;
            
            lines.Add(dir);
        }

        for (int i = 0; i < lines.Count; i += 2)
        {
            float thisLineLength = Vector3.Distance(lines[i], lines[i + 1]);
            float thisLineMaxPoints = Mathf.Round(thisLineLength / minDistanceBetweenPoints);
            float distance = thisLineLength / thisLineMaxPoints;
            Vector3 normalizedDirection = (lines[i + 1] - lines[i]).normalized;
        
            for (float j = 0; j < thisLineMaxPoints + 1; j++)
            {
                Vector3 newPoint = lines[i] + normalizedDirection * (distance * j);

                points.Add(newPoint);
            }
        }


*/
