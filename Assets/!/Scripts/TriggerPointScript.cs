using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class TriggerPointScript : MonoBehaviour
{
    public GameObject target;
    public float radius = 3f;
    public Scene nextScene;
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < radius)
        {
            LoadScene();
        }
    }
    
    private void LoadScene()
    {
        if (nextScene == Scene.None) return;
        
        GameManager.Instance.LoadScene(nextScene);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
