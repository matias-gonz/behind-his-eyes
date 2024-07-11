using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingSoldier : MonoBehaviour
{
    public GameObject lonelyDeserter;
    private Deserter _deserterScript;

    // Start is called before the first frame update
    void Start()
    {
        _deserterScript = lonelyDeserter.GetComponent<Deserter>();
    }

    // Update is called once per frame
    void Update() { }

    private void KillDeserter()
    {
        _deserterScript.GettingKilled();
    }
}
