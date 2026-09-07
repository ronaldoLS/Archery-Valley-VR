using System.Collections.Generic;
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

    [Header("Impacto da flecha")]
    [SerializeField] private float minimumImpactImpulse = 1.1f;


    private int totalScore = 0;
    private readonly HashSet<GameObject> processedArrows = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {

        // Só permite que a ponta da flecha crive no alvo
        if (collision.collider.gameObject.name != "tip")
            return;

        GameObject arrow = collision.transform.root.gameObject;

        // Impede que a mesma flecha seja processada mais de uma vez
        if (processedArrows.Contains(arrow))
            return;


        float impactSpeed = collision.relativeVelocity.magnitude;
        float impactImpulse = collision.impulse.magnitude;

        Debug.Log("Velocidade do impacto: " + impactSpeed);
        Debug.Log("Impulso do impacto: " + impactImpulse);

        if (impactImpulse < minimumImpactImpulse)
        {
            Debug.Log("Impacto fraco. A flecha não ficou presa.");
            return;
        }

        processedArrows.Add(arrow);

        ContactPoint contact = collision.GetContact(0);

        // Collider da região do alvo que recebeu o impacto
        Collider hitCollider = contact.thisCollider;

        int points = GetPoints(hitCollider);

        totalScore += points;

        Debug.Log("Região atingida: " + hitCollider.gameObject.name);
        Debug.Log("Pontuação: " + points);        
        Debug.Log("Pontuação total: " + totalScore);

        Rigidbody arrowRb = collision.rigidbody;

        if (arrowRb != null)
        {
            arrowRb.linearVelocity = Vector3.zero;
            arrowRb.angularVelocity = Vector3.zero;
            arrowRb.isKinematic = true;
        }

        // Posiciona a flecha parcialmente dentro do alvo
        collision.transform.position =
            contact.point - contact.normal * 0.15f;

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
