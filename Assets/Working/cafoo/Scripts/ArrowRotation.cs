using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 speed;

    private void LateUpdate()
    {
        transform.Rotate(speed, Space.Self);
    }
}
