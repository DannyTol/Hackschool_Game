using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = target.transform.position + offset;
    }
}
