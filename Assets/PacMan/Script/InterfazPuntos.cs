using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazPuntos : MonoBehaviour {
    public GameObject puntosUI;
    public static int puntajetotal;

    void Update () {
        puntosUI.GetComponent<Text>().text = "PUNTAJE: " + puntajetotal;
    }

}