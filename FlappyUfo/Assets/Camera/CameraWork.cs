using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public float CameraFollowSpeed = 2f;
    public Vector3 offset;
    public Vector3 CameraMinValues, CameraMaxValues;
    public Transform CameraFollowTarget;
    public Canvas pointCount;

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
        pointCount.transform.position = new Vector3(transform.position.x, transform.position.y, -14);
    }

    private void Follow()
    {
        Vector3 targetPosition = CameraFollowTarget.position + offset;
        float angleInRadians = Mathf.Deg2Rad * 30f;
        float moveX = 5f * Mathf.Cos(angleInRadians) * Time.fixedDeltaTime;
        float moveY = 5.5f * Mathf.Sin(angleInRadians) * Time.fixedDeltaTime;

        // if camera position is out of bounds
        Vector3 camerBoundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x,CameraMinValues.x,float.PositiveInfinity),
            Mathf.Clamp(targetPosition.y, CameraMinValues.y, CameraMaxValues.y),
            -15f
            );
        Vector3 cameraNewPosition = new Vector3(targetPosition.x, targetPosition.y, -20f);
        Vector3 cameraNewPosition2 = Vector3.Lerp(transform.position, camerBoundPosition, CameraFollowSpeed * Time.deltaTime);
        
        transform.position = cameraNewPosition2;

        CameraMinValues += new Vector3(moveX, moveY, 0f);
        CameraMaxValues += new Vector3(moveX, moveY, 0f);

    }
}
