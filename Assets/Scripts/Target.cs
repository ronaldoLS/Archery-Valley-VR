using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("O alvo foi atingido por: " + collision.gameObject.name);

        ContactPoint contact = collision.GetContact(0);

        Debug.Log("Normal do alvo: " + contact.normal);

        Rigidbody arrowRb = collision.rigidbody;

        if (arrowRb != null)
        {
            arrowRb.linearVelocity = Vector3.zero;
            arrowRb.angularVelocity = Vector3.zero;
            arrowRb.isKinematic = true;
        }

        // Coloca a flecha ligeiramente para dentro do alvo
        collision.transform.position = contact.point - contact.normal * 0.3f;

        // Orienta a flecha corretamente
        collision.transform.rotation = Quaternion.LookRotation(contact.normal);
    }
}
