using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;
    public static bool isOpen;
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(player.position, transform.position));
        if (Vector3.Distance(Vector3.zero, transform.position) <= 15)
        {
            player.Find("Canvas/Image").gameObject.SetActive(true);
        }
        else
        {
            player.Find("Canvas/Image").gameObject.SetActive(false);
        }
    }


    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.Find("Canvas/Image").gameObject.SetActive(true);
            open = true;
            Debug.Log(open);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.Find("Canvas/Image").gameObject.SetActive(false);
            open = false;
        }
    }*/
}
