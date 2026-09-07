using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Arrow2 : MonoBehaviour
{
    public PullInteraction pullInteraction;
    public Transform arrowNock;
    public float maxForce = 10f;

    [Header("Rotação da flecha no voo")]
    public float downwardTorque = 0.003f;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool isNocked = false;
    private bool canNock = true;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PullInteraction.PullActionReleased += ReleaseArrow;
    }

    private void OnDisable()
    {
        PullInteraction.PullActionReleased -= ReleaseArrow;
    }

    private void FixedUpdate()
    {
        if (!isNocked && !rb.isKinematic)
        {
            rb.AddTorque(transform.right * downwardTorque, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Notch") && canNock)
        {
            isNocked = true;

            grabInteractable.enabled = false;
            rb.isKinematic = true;

            transform.SetParent(pullInteraction.Notch);
            transform.rotation = pullInteraction.Notch.rotation;

            Vector3 offset = pullInteraction.Notch.position - arrowNock.position;

            transform.position += offset;

            Debug.Log("Flecha encaixada!");
        }
    }

    private void ReleaseArrow(float pullAmount)
    {
        if (!isNocked)
            return;

        canNock = false;
        isNocked = false;

        transform.SetParent(null);

        rb.isKinematic = false;

        float force = pullAmount * maxForce;

        rb.AddForce(transform.forward * force, ForceMode.Impulse);

        Debug.Log("Flecha disparada! Força: " + force);
    }
}
