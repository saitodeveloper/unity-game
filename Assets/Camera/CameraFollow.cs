using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 1f;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    private void Update()
    {
        if (transform.position != target.position)
        {
            Vector3 aimingPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            aimingPosition.x = Mathf.Clamp(aimingPosition.x,  minPosition.x, maxPosition.x);
            aimingPosition.y = Mathf.Clamp(aimingPosition.y,  minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, aimingPosition, smooth);
        }
    }
}
