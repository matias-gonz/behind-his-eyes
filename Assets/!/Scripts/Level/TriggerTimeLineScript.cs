using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;


public class TriggerTimeLineScript : MonoBehaviour
{
    public TimeLine timeLine;
    public TimeLinesDirector timeLinesDirector;

    private void OnTriggerEnter(Collider other)
    {
        timeLinesDirector.PlayTimeLine(timeLine);
        Destroy(gameObject);
    }
}
