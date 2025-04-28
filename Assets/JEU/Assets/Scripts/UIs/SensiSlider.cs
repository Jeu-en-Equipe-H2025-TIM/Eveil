using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SensiSlider : MonoBehaviour
{
    public Slider sensiSlider;
    private float sensiValeur;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    public void changerSensi()
    {
        PlayerPrefs.SetFloat("sensibiliteSouris", sensiSlider.value);
    }
}
