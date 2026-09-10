using UnityEngine;

public class VRUIFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {

        Vector3 direction = player.forward;
        direction.y = 0f;
        direction.Normalize();

        transform.position = player.position + direction + offset;

        transform.LookAt(player);
        transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
