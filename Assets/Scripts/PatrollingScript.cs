using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingScript : MonoBehaviour
{
    private struct PathPoint
    {
        public Vector3 Position;
        public float WaitTime;
    }

    public EnemyControllerScript enemyControllerScript;
    public bool isPatrolling = true;

    private List<PathPoint> PathPoints = new List<PathPoint>();
    private int _currentPointIndex = 0;
    private bool _isWaiting = false;


    void Start()
    {
        // TODO: Read path points from a file
        PathPoints.Add(new PathPoint { Position = new Vector3(0, 0, 0), WaitTime = 1 });
        PathPoints.Add(new PathPoint { Position = new Vector3(0, 0, 10), WaitTime = 1 });
        PathPoints.Add(new PathPoint { Position = new Vector3(10, 0, 10), WaitTime = 1 });
        PathPoints.Add(new PathPoint { Position = new Vector3(10, 0, 0), WaitTime = 1 });
    }

    void Update()
    {
        if (!isPatrolling || PathPoints.Count == 0 || _isWaiting)
        {
            return;
        }

        Vector3 position = transform.position;
        position.y = 0;
        if (Vector3.Distance(position, PathPoints[_currentPointIndex].Position) < 0.1f)
        {
            enemyControllerScript.Stop();
            _currentPointIndex = (_currentPointIndex + 1) % PathPoints.Count;
            StartCoroutine(Wait(PathPoints[_currentPointIndex].WaitTime));
        }
        else
        {
            enemyControllerScript.MoveTo(PathPoints[_currentPointIndex].Position);
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        _isWaiting = false;
    }
}
