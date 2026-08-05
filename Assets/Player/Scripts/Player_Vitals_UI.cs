using UnityEngine;
using UnityEngine.UI;

public class Player_Vitals_UI : MonoBehaviour
{
    [SerializeField] private Player_Vitals playerVitals;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider staminaSlider;

    private void Update()
    {
        healthSlider.value = playerVitals.CurrentHealth / 100f;
        hungerSlider.value = playerVitals.CurrentHunger / 100f;
        staminaSlider.value = playerVitals.CurrentStamina / 100f;
    }

}
