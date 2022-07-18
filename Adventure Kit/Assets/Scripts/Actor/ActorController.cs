using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ActorController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Color textColor = Color.white;

    private Action onArriveAction;
    private TextMeshProUGUI textMeshPro;
    private IEnumerator sayCoroutine;

    private bool isWalking;
    private bool isSaying;

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
            isWalking = false;
            if (onArriveAction != null)
            {
                Action action = onArriveAction;
                onArriveAction = null;
                action();
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
        if (GlobalScriptPlayer.Instance.IsRunning)
        {
            return;
        }

        Cancel();

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

        Walk(destinationPosition, null);
    }

    public void Cancel()
    {
        if (isWalking)
        {
            agent.ResetPath();
            onArriveAction = null;
            isWalking = false;
        }
        if (isSaying)
        {
            StopCoroutine(sayCoroutine);
            sayCoroutine = null;
            textMeshPro.text = null;
            isSaying = false;
        }
    }

    public void Walk(Vector3 destination, Action completion)
    {
        if (isWalking)
        {
            throw new UnityException("Already walking");
        }
        isWalking = true;
        onArriveAction = completion;
        agent.SetDestination(destination);
    }

    public void Say(string text, Action completion)
    {
        if (isSaying)
        {
            throw new UnityException("Already saying");
        }
        isSaying = true;
        textMeshPro.color = textColor;
        textMeshPro.text = text;
        sayCoroutine = SayCoroutine(2, completion);
        StartCoroutine(sayCoroutine);
    }

    IEnumerator SayCoroutine(float seconds, Action completion)
    {
        yield return new WaitForSeconds(seconds);
        sayCoroutine = null;
        textMeshPro.text = null;
        isSaying = false;
        completion();
    }
}
