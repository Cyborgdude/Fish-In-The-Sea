using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = new Quaternion(0,0,0,1f);
    }

}
