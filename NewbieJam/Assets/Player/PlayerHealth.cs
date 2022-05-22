using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Vector3 offset;


    private void Update() {
        if(slider != null){
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        }
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
