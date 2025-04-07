using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using TMPro;

public class timerManager : MonoBehaviour
{

    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = gameManager.timer.ToString() + " secondes";
    }




}
