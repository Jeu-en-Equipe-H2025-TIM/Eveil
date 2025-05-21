
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class soundsSlider : MonoBehaviour
{

    public Slider slider;
    public AudioMixer mixerPrincipal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setVolume(PlayerPrefs.GetFloat("volumeSauvegarder", 100));
    }

    public void setVolume(float valeur)
    {
        if (valeur < 1)
        {
            valeur = 0.001f;
        }
        updateSlider(valeur);
        mixerPrincipal.SetFloat("MasterVolume", Mathf.Log10(valeur / 100) * 20f);

    }

    public void SetVolumeDuSlider()
    {
        setVolume(slider.value);
    }

    public void updateSlider(float valeur)
    {
        slider.value = valeur;
    }
}
