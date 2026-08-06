using UnityEngine;

public class PlayerColliderSystem : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;

    private void Awake()
    {
        playerCollider = playerCollider != null ? playerCollider : GetComponent<Collider>();
    }

    public void EnableCollider() => playerCollider.enabled = true;
    public void DisableCollider() => playerCollider.enabled = false;
}
