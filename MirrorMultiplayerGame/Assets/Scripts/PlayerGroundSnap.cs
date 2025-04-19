using UnityEngine;

public class PlayerGroundSnap : MonoBehaviour
{
    [Header("Ground Snap Settings")]
    public LayerMask groundLayer;        // Assign your "Ground" layer here
    public float snapRayHeight = 1f;     // How high above the player the ray starts
    public float snapDistance = 2f;      // How far down it checks
    public float snapSpeed = 20f;        // Smooth snapping speed

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * snapRayHeight;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, snapDistance, groundLayer))
        {
            float heightOffset = GetComponent<Collider>().bounds.extents.y; // Half height
            Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y + heightOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * snapSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 rayStart = transform.position + Vector3.up * snapRayHeight;
        Gizmos.DrawLine(rayStart, rayStart + Vector3.down * snapDistance);
    }
}
