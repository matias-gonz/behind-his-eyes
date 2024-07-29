using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGuardAnimationController : MonoBehaviour
{
    private int _isSmokingHash;
    // Start is called before the first frame update
    void Start()
    {
              _isSmokingHash = Animator.StringToHash("IsSmoking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
