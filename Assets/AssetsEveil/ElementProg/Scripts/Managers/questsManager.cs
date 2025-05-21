using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class questsManager : MonoBehaviour
{
    // Hub pour toutes les quetes (similaires � gererAccesZones)

    public List<String> listeQuetes = new List<String>();

  public AudioClip audioQueteChange;





    public void queteTrigger(int delais)
  {
    Debug.Log("QM : Quete qui va se faire delete dans " + delais);
    Invoke("queteUpdate", delais);
  }

  void queteUpdate()
  {
    Debug.Log("QM : Quete qui se fait delete maintenant");
    // Update de la quete
    // Delete quete actuelle
    listeQuetes.RemoveAt(0);
    this.GetComponent<AudioSource>().PlayOneShot(audioQueteChange);     
        //

    // Trigger un son, un commentaire de l'IA, whatever, mettre ici

    //
  }

}


