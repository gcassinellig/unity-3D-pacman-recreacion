using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolitaColeccionada : MonoBehaviour {

    public AudioSource puntocomido1;
    public AudioSource puntocomido2;

    void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player"){
			InterfazPuntos.puntajetotal += 10;
			InterfazBolitas.bolitascomidas += 1;
			if (InterfazBolitas.bolitascomidas % 2 == 0){
				puntocomido2.Play();
			} else {
				puntocomido1.Play();
			}
			Destroy(gameObject);
		}
    }
}
