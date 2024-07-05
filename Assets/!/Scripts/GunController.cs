using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePos, gun, collimator;
    public GameObject bullet;
    private Vector3 point;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }


    private Vector3 Collimator()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, maxDistance: 1000))
            point = hit.point;
         Vector3 directionToTarget = (point - firePos.position).normalized;
        return directionToTarget;
    }

    private void Fire()
    {
        if (StartGame.isPickUp)
        {
           
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(bullet, firePos.position, Quaternion.EulerAngles(Collimator()));
            go.GetComponent<Rigidbody>().velocity = Collimator() * 30;
            Destroy(go, 3);
        }
    }
}
