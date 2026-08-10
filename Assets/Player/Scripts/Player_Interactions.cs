using Unity.VisualScripting;
using UnityEngine;

public class Player_Interactions : MonoBehaviour
{
    [SerializeField] private Input_Handler inputHandler;

    private void Update()
    {
        if (inputHandler.InteractPressedThisFrame)
        {
            Intract(GetRayCastHit());
        }
    }


    private void Intract(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Interactable_Object interactable))
            {
                interactable.Interact();
            }
        }
    }


    float rayCastDistance = 4f;
    private RaycastHit GetRayCastHit()
    {
        RaycastHit hit = Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, rayCastDistance) ? hit : default;
        return hit;
    }
}
