using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ActorController : MonoBehaviour
{
    public NavMeshAgent agent;

    private Interactable currentInteractable;
    private ScriptPlayer currentScriptPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnInteract();
                currentInteractable = null;
            }
            else if (currentScriptPlayer != null)
            {
                ScriptPlayer scriptPlayer = currentScriptPlayer;
                currentScriptPlayer = null;
                scriptPlayer.Continue();
            }
        }
    }

    public void OnGroundClick(BaseEventData data)
    {
        currentInteractable = null;
        currentScriptPlayer = null;

        Vector3 destinationPosition;
        PointerEventData pData = (PointerEventData)data;

        if (NavMesh.SamplePosition(pData.pointerCurrentRaycast.worldPosition, out NavMeshHit hit, 2, NavMesh.AllAreas))
        {
            destinationPosition = hit.position;
        }
        else
        {
            destinationPosition = pData.pointerCurrentRaycast.worldPosition;
        }

        agent.SetDestination(destinationPosition);
    }

    public void OnInteractableClick(Interactable interactable)
    {
        currentInteractable = interactable;
        currentScriptPlayer = null;

        Vector3 destinationPosition = currentInteractable.location.position;
        agent.SetDestination(destinationPosition);
    }

    public void Walk(Vector3 destination, ScriptPlayer scriptPlayer)
    {
        currentInteractable = null;
        currentScriptPlayer = scriptPlayer;

        agent.SetDestination(destination);
    }
}
