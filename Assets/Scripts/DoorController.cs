using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;
    public static bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= 3)
        {
            player.Find("Canvas/Image").gameObject.SetActive(true);
        }
        else
        {
            player.Find("Canvas/Image").gameObject.SetActive(false);
        }
    }
}