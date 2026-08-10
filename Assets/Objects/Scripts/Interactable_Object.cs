using UnityEngine;

public class Interactable_Object : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
