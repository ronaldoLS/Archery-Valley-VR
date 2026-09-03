using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("O alvo foi atingido por: " + collision.gameObject.name);

        // Pega a posição exata onde a flecha bateu
        ContactPoint contact = collision.GetContact(0);

        Debug.Log("Normal do alvo: " + contact.normal);

        // Para a flecha
        Rigidbody arrowRb = collision.rigidbody;

        if (arrowRb != null)
        {
            arrowRb.linearVelocity = Vector3.zero;
            arrowRb.angularVelocity = Vector3.zero;

            // Faz a flecha deixar de ser afetada pela física
            arrowRb.isKinematic = true;
        }

        // Coloca a flecha exatamente no ponto de impacto
        collision.transform.position = contact.point;
    }

}
