using NUnit.Framework;
using System;
using UnityEngine;

public class gererAccesZones : MonoBehaviour
{

    // Keycards / Progression / Dialogue (Changements)
    // vers
    // Débloque des portes / Zones / Ascenseur (Scan si changements)


    // Zones

    // Pour ajouter des zones: 
    // public bool (NOM DE ZONE) = false;

    public bool accesTutoPart2 = false;
    public bool accesTutoPart3 = false;

    public bool accesZone1 = false;
    public bool accesZone1Part2 = false;
    public bool accesZone1Part3 = false;

    public bool accesZone2 = false;

    public bool puzzle1Fin = false;

    public bool accesEtQuete = false;
    //

    // Permissions
    public bool debloqueAccesZone1 = false;
    //

    // Ascenseurs
    public bool ascenseur1 = false;
    //
}
