using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazPuntosMax : MonoBehaviour {
    public GameObject puntosUI;
    public static int puntajerecord;

    void Update () {
        puntosUI.GetComponent<Text>().text = "RECORD: " + puntajerecord;
        if (InterfazPuntos.puntajetotal >= puntajerecord) {
            puntajerecord = InterfazPuntos.puntajetotal;
        }
    }

}