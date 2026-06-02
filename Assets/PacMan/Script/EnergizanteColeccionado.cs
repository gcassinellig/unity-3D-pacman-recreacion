using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergizanteColeccionado : MonoBehaviour {
	[Header("Imports")]
	public ControladorJugador pacman;
	public GameManager manager;
	public ControladorEnemigo[] fantasmas;

	[Header("Sonidos")]
	public AudioSource puntocomido1;
    public AudioSource puntocomido2;

    void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player"){
			InterfazPuntos.puntajetotal += 50;
			InterfazBolitas.bolitascomidas += 1;
			pacman.puntosmultiplicador = 1;

			if (InterfazBolitas.bolitascomidas % 2 == 0){
				puntocomido2.Play();
			} else {
				puntocomido1.Play();
			}

			for (int i = 0; i < 4; i++) {
				if (fantasmas[i].Estado < 2) {
					fantasmas[i].Estado = 1;
				}
			}

			pacman.Energizante = true;
			manager.energizantereset = true;

			Destroy(gameObject);
		}
    }
}
