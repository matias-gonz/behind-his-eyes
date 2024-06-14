using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class PatrollingScript : MonoBehaviour
{
    public EnemyControllerScript enemyControllerScript;
    public string pathName;
    public bool isPatrolling = true;
    private bool hasReachedCurrentGoal = true; //initialise with true

    private List<PathPoint> _pathPoints = new List<PathPoint>();
    private int _currentPointIndex = 0;
    private bool _isWaiting = false;


    void Start()
    {
        _pathPoints = PathPointsReader.ReadPathPoints(pathName);
    }

    void Update()
    {
        if (!isPatrolling || _pathPoints.Count == 0 || _isWaiting)
        {
            return;
        }

        Vector3 position = transform.position;
        position.y = 0;
        if (!hasReachedCurrentGoal && Vector3.Distance(position, _pathPoints[_currentPointIndex].Position) < 0.02f)
        {
            hasReachedCurrentGoal = true;
            enemyControllerScript.Stop();
            _currentPointIndex = (_currentPointIndex + 1) % _pathPoints.Count;
            StartCoroutine(Wait(_pathPoints[_currentPointIndex].WaitTime));
        }
        else if (hasReachedCurrentGoal == true)
        {
            Debug.Log("Patroling script MoveTo new Goal");
            hasReachedCurrentGoal = false;
        
            enemyControllerScript.MoveTo(_pathPoints[_currentPointIndex].Position, true);
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        _isWaiting = false;
    }
}
