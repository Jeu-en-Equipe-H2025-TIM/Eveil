using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class questsManager : MonoBehaviour
{
    // Hub pour toutes les quetes (similaires à gererAccesZones)

    public List<String> listeQuetes = new List<String>();
    public Color couleurDuTexte;





    public void queteTrigger(int delais, bool estUneHallucination)
    {
        if (estUneHallucination)
        {
            couleurDuTexte = Color.red;
        } else
        {
            couleurDuTexte = Color.white;
        }

        Debug.Log("QM : Quete qui va se faire delete dans " + delais);
        Invoke("queteUpdate", delais);
    }

    void queteUpdate()
    {
          Debug.Log("QM : Quete qui se fait delete maintenant");
        // Update de la quete
        // Delete quete actuelle
          listeQuetes.RemoveAt(0);
        //

        // Trigger un son, un commentaire de l'IA, whatever, mettre ici

        //
    }

}


