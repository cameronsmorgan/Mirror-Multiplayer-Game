using UnityEngine;
using Mirror;
public class PickupItem : NetworkBehaviour
{
    [SyncVar]
    public bool isPickedUp = false;

    public void OnPickedUp(Transform attachPoint)
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            transform.SetParent(attachPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            if (TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = true;
            }
        }
    }

    public void OnDropped()
    {
        isPickedUp = false;
        transform.SetParent(null);

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
        }
    }
}
