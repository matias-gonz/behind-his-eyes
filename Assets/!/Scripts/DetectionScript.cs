using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public EnemyControllerScript enemyControllerScript;
    public GameObject target; 
    public float fov = 135f;
    public float maxViewDistance = 20f;
    public float maxSoundRange = 10f;
    public float tolerance = 0.2f;
    private LayerMask _layerMask;
    private Collider[] _colliders;
    private PlayerController _playerController;

    private void Start()
    {
        _layerMask = LayerMask.GetMask("Level");
        _colliders = target.GetComponents<Collider>();
        _playerController = target.GetComponent<PlayerController>();
        enemyControllerScript.InitialiseEnemyControllerScript(target);
    }

    void Update()
    {
        if (!target)
        {
            return;
        }

        // Check if the target is in range
        float distance = Vector3.Distance(transform.position, target.transform.position);
        float viewDistance = _playerController.GetViewDistance(maxViewDistance);
        if (distance > viewDistance) return;

        // Check if the target is in the field of view OR enemy can hear the target
        Vector3 playerDirection = target.transform.position - transform.position;
        playerDirection.y = 0;
        Vector3 lookDirection = transform.forward;
        lookDirection.y = 0;
        float angle = Vector3.Angle(playerDirection, lookDirection);
        float soundRange = maxSoundRange; // TODO: Implement getSoundRange() from player
        if (distance > soundRange && angle > fov / 2) return;

        // Check if the target is in sight
        Vector3 rayStart = transform.position;
        rayStart.y += 1.65f;

        Collider targetCollider = FindTargetCollider();
        if (!targetCollider) return;

        Vector3[] directions = GenerateDirections(rayStart, targetCollider);

        if (!directions.Any(direction => CheckRay(direction, rayStart, distance))) return;

        enemyControllerScript.EngageTarget(playerDirection);
        
        Invoke(nameof(GameOver), 6f);
    }
    
    private void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    private Collider FindTargetCollider()
    {
        return _colliders.FirstOrDefault(c => c.enabled);
    }

    private Vector3[] GenerateDirections(Vector3 rayStart, Collider targetCollider)
    {
        Vector3 center = targetCollider.bounds.center;
        Vector3 directionTop = targetCollider.bounds.max - rayStart;
        directionTop.y -= tolerance;

        Vector3 directionBottom = targetCollider.bounds.min - rayStart;
        directionBottom.y += tolerance;

        Vector3 directionLeft = new Vector3(center.x, center.y, center.z - targetCollider.bounds.extents.z) - rayStart;
        directionLeft.z += tolerance;

        Vector3 directionRight = new Vector3(center.x, center.y, center.z + targetCollider.bounds.extents.z) - rayStart;
        directionRight.z -= tolerance;

        Vector3 directionFront = new Vector3(center.x + targetCollider.bounds.extents.x, center.y, center.z) - rayStart;
        directionFront.x -= tolerance;

        Vector3 directionBack = new Vector3(center.x - targetCollider.bounds.extents.x, center.y, center.z) - rayStart;
        directionBack.x += tolerance;

        return new[]
        {
            directionTop, directionBottom, directionLeft, directionRight, directionFront, directionBack
        };
    }

    private bool CheckRay(Vector3 direction, Vector3 rayStart, float distance)
    {
        if (Physics.Raycast(rayStart, direction, out _, distance, _layerMask))
        {
            Debug.DrawRay(rayStart, direction * distance, Color.green);
            return false;
        }

        Debug.DrawRay(rayStart, direction * distance, Color.red);
        return true;
    }
    
    private void OnDrawGizmos()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        float viewDistance = _playerController.GetViewDistance(maxViewDistance);
        if (distance > viewDistance*1.5) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
