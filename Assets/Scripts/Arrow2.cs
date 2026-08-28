using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Arrow2 : MonoBehaviour
{
    public PullInteraction pullInteraction;
    public Transform arrowNock;


    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private bool isNocked = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNocked)
        {
            Vector3 positionDifference = pullInteraction.Notch.position - arrowNock.position;

            transform.position += positionDifference;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Notch"))
        {
            isNocked = true;
            grabInteractable.enabled = false;
            rb.isKinematic = true;

            Debug.Log("Flecha encaixada!");
        }
    }
}
