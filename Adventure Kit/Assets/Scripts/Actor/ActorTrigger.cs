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
        if (localScriptPlayerToStop != null)
        {
            localScriptPlayerToStop.StopExecution();
        }
        actor.OnInteractableClick(interactable, location.position);
    }
}
