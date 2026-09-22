using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Plataformas")]
    [SerializeField] private TeleportationAnchor[] platforms;

    [Header("Estado atual")]
    [SerializeField] private int currentMultiplier = 1;

    [Header("Arco")]
    [SerializeField] private Transform bow;
    [SerializeField] private Transform leftBowAttach;
    [SerializeField] private Transform rightBowAttach;

    [Header("hands")]
    [SerializeField] private GameObject leftHandVisual;
    [SerializeField] private GameObject rightHandVisual;

    [Header("Ray")]
    [SerializeField] private RayController leftRayController;
    [SerializeField] private RayController rightRayController;

    private bool bowInLeftHand = true;

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

    private void Start()
    {
        SetBowHand(true);
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
    public void SwitchHand()
    {
        bowInLeftHand = !bowInLeftHand;
        SetBowHand(bowInLeftHand);
    }

    private void SetBowHand(bool leftHand)
    {
        Transform attach = leftHand ? leftBowAttach : rightBowAttach;

        // Arco
        bow.SetParent(attach);
        bow.localPosition = Vector3.zero;
        bow.localRotation = Quaternion.identity;

        // Mãos
        leftHandVisual.SetActive(!leftHand);
        rightHandVisual.SetActive(leftHand);

        // Ray fica na mão oposta ao arco
        leftRayController.SetControl(!leftHand);
        rightRayController.SetControl(leftHand);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}