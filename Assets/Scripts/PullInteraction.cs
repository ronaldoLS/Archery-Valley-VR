using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;

    public Transform start, end;
    public GameObject notch;
    public float pullAmount { get; private set; } = 0.0f;


    private LineRenderer _lineRenderer;
    private Vector3 initialPullPosition;
    private IXRSelectInteractor pullingInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactorObject;
        initialPullPosition = pullingInteractor.transform.position;
    }

    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        pullingInteractor = null;
        pullAmount = 0f;

        notch.transform.localPosition = new Vector3(
            notch.transform.localPosition.x,
            notch.transform.localPosition.y,
            0f
        );
        UpdateString();
    }

    public override void ProcessInteractable(
        XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 currentPosition = pullingInteractor.transform.position;
                Vector3 pullDelta = currentPosition - initialPullPosition;

                pullAmount = CalculatePull(pullDelta);

                UpdateString();
            }
        }
    }

    private float CalculatePull(Vector3 pullDelta)
    {
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();

        float pullValue = Vector3.Dot(
            pullDelta,
            targetDirection
        ) / maxLength;

        return Mathf.Clamp01(pullValue);    
    }

    private void UpdateString()
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(
            start.transform.localPosition.z,
            end.transform.localPosition.z,
            pullAmount
        );

        notch.transform.localPosition = new Vector3(
            notch.transform.localPosition.x,
            notch.transform.localPosition.y,
            linePosition.z + 0.2f
        );

        _lineRenderer.SetPosition(1, linePosition);
    }
}