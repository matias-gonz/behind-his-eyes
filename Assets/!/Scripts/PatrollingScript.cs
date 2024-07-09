using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class PatrollingScript : MonoBehaviour
{
    public EnemyControllerScript enemyControllerScript;
    public string pathName;
    public bool isPatrolling = true;
    private Vector3 _startingPosition;

    private bool _hasReachedCurrentGoal = true; //initialise with true
    private List<PathPoint> _pathPoints = new List<PathPoint>();
    private int _currentPointIndex = 0;
    private bool _isWaiting = false;


    void Start()
    {
        _pathPoints = PathPointsReader.ReadPathPoints(pathName);
        _startingPosition = transform.position; //only set here!
    }

    void Update()
    {
        if (!isPatrolling || _pathPoints.Count == 0 || _isWaiting)
        {
            return;
        }

        Vector3 position = transform.position - _startingPosition;
        position.y = 0;
        if (!_hasReachedCurrentGoal && Vector3.Distance(position, _pathPoints[_currentPointIndex].Position) < 0.2f)
        {
            _hasReachedCurrentGoal = true;
            enemyControllerScript.PatrolWait();
            StartCoroutine(Wait(_pathPoints[_currentPointIndex].WaitTime));
            _currentPointIndex = (_currentPointIndex + 1) % _pathPoints.Count;
        }
        else if (_hasReachedCurrentGoal)
        {
            _hasReachedCurrentGoal = false;
            enemyControllerScript.PatrolTo(_pathPoints[_currentPointIndex].Position + _startingPosition, false);
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        _isWaiting = false;
    }
    
}
