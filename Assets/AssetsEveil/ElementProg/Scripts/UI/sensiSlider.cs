using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class sensiSlider : MonoBehaviour
{
    public Slider sensibiliteSlider;
    private float sensiValeur;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public void changerSensi()
    {
        PlayerPrefs.SetFloat("sensibiliteSouris", sensibiliteSlider.value);

    }
}