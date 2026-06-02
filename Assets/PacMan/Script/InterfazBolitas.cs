using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfazBolitas : MonoBehaviour {
    public GameObject bolitasUI;
    public static int bolitascomidas;

    void Update () {
        bolitasUI.GetComponent<Text>().text = "BOLITAS: " + bolitascomidas + " / 244";
    }

}