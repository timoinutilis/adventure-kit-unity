using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActorTrigger : MonoBehaviour, IPointerClickHandler
{
    public ActorController actor;
    public Transform location;
    public Interactable interactable;
    public LocalScriptPlayer localScriptPlayerToStop;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (GlobalScriptPlayer.Instance.IsRunning)
        {
            return;
        }

        if (localScriptPlayerToStop != null)
        {
            localScriptPlayerToStop.StopExecution();
        }

        InventoryItem item = Inventory.Instance.DraggingItem;
        Inventory.Instance.DraggingItem = null;

        actor.Cancel();
        actor.Walk(location.position, () =>
        {
            if (item != null)
            {
                interactable.OnCombineWithItem(item);
            }
            else
            {
                interactable.OnInteract();
            }
        });
    }
}
