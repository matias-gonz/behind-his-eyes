using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapLoadingScript : MonoBehaviour
{
    [SerializeField] GameObject[] mapSegments;
    private GameObject[] ambientList;
    private GameObject[] obstaclesList;
    private GameObject[] buildingsList;
    private GameObject[] streetList;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var segment in mapSegments)
        {
            ambientList.Append(segment.transform.Find("Ambient").gameObject);
            buildingsList.Append(segment.transform.Find("Buildings").gameObject);
            obstaclesList.Append(segment.transform.Find("Obstacles").gameObject);
            streetList.Append(segment.transform.Find("Street").gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
