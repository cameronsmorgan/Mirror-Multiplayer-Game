using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class PlayerInteraction : NetworkBehaviour
{
    private PickupItem nearbyItem;
    public Transform pickupAttachPoint; // Assign to a "Hip" or "HoldPoint" on the player

    private PickupItem heldItem;

    private void OnCollisionEnter(Collision collision)
    {
        PickupItem item = collision.gameObject.GetComponent<PickupItem>();
        if (item != null && !item.isPickedUp)
        {
            nearbyItem = item;
            Debug.Log("Found pickup item: " + item.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PickupItem>() == nearbyItem)
        {
            nearbyItem = null;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null && nearbyItem != null)
            {
                CmdPickUp(nearbyItem.netIdentity);
            }
            else if (heldItem != null)
            {
                CmdDrop();
            }
        }
    }

    [Command]
    private void CmdPickUp(NetworkIdentity itemNetId)
    {
        PickupItem item = itemNetId.GetComponent<PickupItem>();
        if (item != null && !item.isPickedUp)
        {
            item.OnPickedUp(pickupAttachPoint);
            heldItem = item;
        }
    }

    [Command]
    private void CmdDrop()
    {
        if (heldItem != null)
        {
            heldItem.OnDropped();
            heldItem = null;
        }
    }
}
