using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using TMPro;

public class ActorController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Color textColor = Color.white;

    private Interactable currentInteractable;
    private InventoryItem currentInventoryItem;
    private ScriptPlayer currentScriptPlayer;
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GameObject.FindGameObjectWithTag("DialogText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasArrived())
        {
            if (currentInteractable != null)
            {
                Interactable interactable = currentInteractable;
                InventoryItem item = currentInventoryItem;
                currentInteractable = null;
                currentInventoryItem = null;
                if (item != null)
                {
                    interactable.OnCombineWithItem(item);
                }
                else
                {
                    interactable.OnInteract();
                }
            }
            else if (currentScriptPlayer != null)
            {
                ScriptPlayer scriptPlayer = currentScriptPlayer;
                currentScriptPlayer = null;
                scriptPlayer.Continue();
            }
        }
    }

    private bool HasArrived()
    {
        return agent != null
            && !agent.pathPending
            && agent.remainingDistance <= agent.stoppingDistance
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    public void OnGroundClick(BaseEventData data)
    {
        currentInteractable = null;
        currentInventoryItem = null;
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

    public void OnInteractableClick(Interactable interactable, Vector3 destinationPosition)
    {
        currentInteractable = interactable;
        currentInventoryItem = Inventory.Instance.DraggingItem;
        Inventory.Instance.DraggingItem = null;
        currentScriptPlayer = null;
        agent.SetDestination(destinationPosition);
    }

    public void Cancel()
    {
        //TODO: implement state and cancel only needed things (especially textMeshPro)
        StopAllCoroutines();
        agent.ResetPath();
        textMeshPro.text = null;

        currentInteractable = null;
        currentInventoryItem = null;
        currentScriptPlayer = null;
    }

    public void Walk(Vector3 destination, ScriptPlayer scriptPlayer)
    {
        currentScriptPlayer = scriptPlayer;
        agent.SetDestination(destination);
    }

    public void Say(string text, ScriptPlayer scriptPlayer)
    {
        textMeshPro.color = textColor;
        textMeshPro.text = text;
        StartCoroutine(WaitCoroutine(scriptPlayer, 2));
    }

    IEnumerator WaitCoroutine(ScriptPlayer scriptPlayer, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        textMeshPro.text = null;
        scriptPlayer.Continue();
    }
}
