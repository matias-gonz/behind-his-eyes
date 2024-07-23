using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCam;
    private bool _doSnapCamera = false;
    private float _gettingKilledAngle = 0f;


    void LateUpdate()
    {
        if (!_doSnapCamera) return;
        //Basically, create two new Quaternions with the values in the y and let Quaternion.Lerp handle it

        thirdPersonCam.m_XAxis.Value = Quaternion
            .Lerp(
                Quaternion.Euler(0, thirdPersonCam.m_XAxis.Value, 0),
                Quaternion.Euler(0, _gettingKilledAngle, 0),
                5 * Time.deltaTime
            )
            .eulerAngles.y;

        thirdPersonCam.m_YAxis.Value = Quaternion
            .Lerp(
                Quaternion.Euler(0, thirdPersonCam.m_YAxis.Value, 0),
                Quaternion.Euler(0, 0.5f, 0),
                5 * Time.deltaTime
            )
            .eulerAngles.y;

        if (Mathf.Abs(thirdPersonCam.m_XAxis.Value - _gettingKilledAngle) < 0.1f)
        {
            _doSnapCamera = false;
        }
    }

    public void RotateTPCam(Vector3 direction)
    {
        _gettingKilledAngle = Mathf.Atan2(-direction.x, -direction.z) * Mathf.Rad2Deg;
        _doSnapCamera = true;
    }
}
