using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class GameManager : MonoBehaviour
{
    [Header("Plataformas")]
    [SerializeField] private TeleportationAnchor[] platforms;

    [Header("Estado atual")]
    [SerializeField] private int currentMultiplier = 1;

    public int CurrentMultiplier => currentMultiplier;

    private void OnEnable()
    {
        foreach (TeleportationAnchor platform in platforms)
        {
            platform.teleporting.AddListener(OnTeleporting);
        }
    }

    private void OnDisable()
    {
        foreach (TeleportationAnchor platform in platforms)
        {
            platform.teleporting.RemoveListener(OnTeleporting);
        }
    }

    private void OnTeleporting(TeleportingEventArgs args)
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            if (args.interactableObject == platforms[i])
            {
                currentMultiplier = i + 1;

                Debug.Log("Plataforma: " + (i + 1));
                Debug.Log("Multiplicador: x" + currentMultiplier);

                return;
            }
        }
    }
}