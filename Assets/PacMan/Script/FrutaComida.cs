using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrutaComida : MonoBehaviour {
	[Header("Otros")]
	public AudioSource frutasfx;
    public int frutapuntos = 100;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player"){
			InterfazPuntos.puntajetotal += frutapuntos;
			frutasfx.Play();
			gameObject.SetActive(false);
		}
    }
}