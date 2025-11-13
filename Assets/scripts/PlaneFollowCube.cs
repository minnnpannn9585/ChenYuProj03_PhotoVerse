using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFollowCube : MonoBehaviour
{
    [Tooltip("The cube (or any target) the plane should follow")]
    public Transform target;

    [Tooltip("Horizontal distance (in world XZ plane) to keep from the target")]
    public float distance = 5f;

    [Tooltip("Vertical offset relative to the target's Y position")]
    public float heightOffset = 0f;

    [Tooltip("How quickly the plane moves to the desired position (higher = snappier)")]
    public float moveSmooth = 10f;

    [Tooltip("How quickly the plane rotates to match the target's Y rotation")]
    public float rotationSmooth = 10f;

    // Use LateUpdate so we follow after the target has moved this frame
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position;

        // Compute a horizontal direction (XZ plane) from the target to the plane.
        // If the plane is exactly on top of target on XZ, default to target's backward.
        Vector3 dir = transform.position - targetPos;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f)
        {
            dir = -target.forward;
            dir.y = 0f;
            if (dir.sqrMagnitude < 0.0001f) dir = Vector3.back;
        }
        dir.Normalize();

        // Desired position: keep the specified horizontal distance and the specified vertical offset
        Vector3 desiredPos = targetPos + dir * distance;
        desiredPos.y = targetPos.y + heightOffset;

        // Smoothly move to desired position
        float moveT = 1f - Mathf.Exp(-moveSmooth * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, desiredPos, moveT);

        // Match the target's Y-axis rotation while keeping the plane upright (no pitch/roll)
        float targetYaw = target.eulerAngles.y;
        Quaternion desiredRot = Quaternion.Euler(0f, targetYaw, 0f);
        float rotT = 1f - Mathf.Exp(-rotationSmooth * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotT);
    }
}
