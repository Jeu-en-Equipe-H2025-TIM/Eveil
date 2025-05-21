using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine;

public class dialoguesDeleteZone : MonoBehaviour
{
    public List<GameObject> listeZones = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Passe à travers listeZones et delete les tous
            foreach (GameObject zone in listeZones)
            {
                // On delete la zone
                Destroy(zone);
            }
        }
    }
}