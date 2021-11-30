using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    private float health;
    private float maxHealth;

    // Update is called once per frame
    void Update()
    {
        health = this.transform.parent.GetComponent<Character>().currentHealth;
        maxHealth = this.transform.parent.GetComponent<Character>().maxHealth;
        // have slider follow object it is attached to
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
        SetHealth(health, maxHealth);
    }

    public void SetHealth(float health, float maxHealth)
    {
        // show health bar
        Slider.gameObject.SetActive(health < maxHealth);
        // current health value
        Slider.value = health;
        // max health value;
        Slider.maxValue = maxHealth;
        // fill in health bar
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }
}
