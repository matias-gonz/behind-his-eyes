using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapLoadingScript : MonoBehaviour
{
    [SerializeField] GameObject[] mapSegments;
    private GameObject[] _ambientList;
    private GameObject[] _obstaclesList;
    private GameObject[] _buildingsList;
    private GameObject[] _streetList;
    private GameObject[] _enemiesList;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var segment in mapSegments)
        {
            _ambientList.Append(segment.transform.Find("Ambient").gameObject);
            _buildingsList.Append(segment.transform.Find("Buildings").gameObject);
            _enemiesList.Append(segment.transform.Find("Enemies").gameObject);
            _obstaclesList.Append(segment.transform.Find("Obstacles").gameObject);
            _streetList.Append(segment.transform.Find("Street").gameObject);
        }
    }

}
