using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Checkpoint
{
    public Vector3 position;
    public Quaternion rotation;
}

public class CheckpointScript : MonoBehaviour
{
    public Checkpoint checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.SetCheckpoint(checkpoint);
    }
}
