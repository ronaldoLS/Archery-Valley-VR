using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Colliders das regiões")]
    [SerializeField] private Collider outerWhiteCollider;
    [SerializeField] private Collider blackCollider;
    [SerializeField] private Collider blueCollider;
    [SerializeField] private Collider redCollider;
    [SerializeField] private Collider yellowCollider;

    [Header("Pontuação")]
    [SerializeField] private int outerWhitePoints = 1;
    [SerializeField] private int blackPoints = 3;
    [SerializeField] private int bluePoints = 5;
    [SerializeField] private int redPoints = 7;
    [SerializeField] private int yellowPoints = 10;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        // Collider da região do alvo que recebeu o impacto
        Collider hitCollider = contact.thisCollider;

        int points = GetPoints(hitCollider);

        Debug.Log("Região atingida: " + hitCollider.gameObject.name);
        Debug.Log("Pontuação: " + points);

        Rigidbody arrowRb = collision.rigidbody;

        if (arrowRb != null)
        {
            arrowRb.linearVelocity = Vector3.zero;
            arrowRb.angularVelocity = Vector3.zero;
            arrowRb.isKinematic = true;
        }

        // Posiciona a flecha parcialmente dentro do alvo
        collision.transform.position =
            contact.point - contact.normal * 0.3f;

        // Mantém a orientação correta da flecha
        collision.transform.rotation =
            Quaternion.LookRotation(contact.normal);
    }

    private int GetPoints(Collider hitCollider)
    {
        if (hitCollider == yellowCollider)
            return yellowPoints;

        if (hitCollider == redCollider)
            return redPoints;

        if (hitCollider == blueCollider)
            return bluePoints;

        if (hitCollider == blackCollider)
            return blackPoints;

        if (hitCollider == outerWhiteCollider)
            return outerWhitePoints;

        return 0;
    }
}
