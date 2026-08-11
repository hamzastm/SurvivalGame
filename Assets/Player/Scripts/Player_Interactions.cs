using UnityEngine;

public class Player_Interactions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Input_Handler inputHandler;
    [SerializeField] private Transform cameraTransform;

    [Header("Raycast Settings")]
    [SerializeField] private float rayCastDistance = 4f;
    [SerializeField] private LayerMask interactableLayer = ~0; // Default to everything

    private void Awake()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (inputHandler != null && inputHandler.InteractPressedThisFrame)
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (cameraTransform == null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayCastDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out Interactable_Object interactable))
            {
                interactable.Interact();
            }
        }
    }

    // Visualization for the Unity Editor Scene View
    private void OnDrawGizmosSelected()
    {
        if (cameraTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * rayCastDistance);
        }
    }
}