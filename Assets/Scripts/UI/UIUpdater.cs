using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] CharacterStatus characterStatus;


    private void Awake()
    {
        characterStatus.OnHealthChanged += UpdateHealtUI;
        characterStatus.OnStaminaChanged += UpdateStaminaUI;
    }

    private void Start() {
        healthSlider.maxValue = characterStatus.Health;
        staminaSlider.maxValue = characterStatus.Stamina;

        healthSlider.value = characterStatus.Health;
        staminaSlider.value = characterStatus.Stamina;
    }

    private void UpdateHealtUI(float value)
    {
        healthSlider.value = value;
    }

    
    private void UpdateStaminaUI(float value)
    {
        staminaSlider.value = value;
    }


    private void OnDestroy()
    {
        characterStatus.OnHealthChanged -= UpdateHealtUI;
        characterStatus.OnStaminaChanged -= UpdateStaminaUI;
    }
}
