using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControladorEnemigo : MonoBehaviour {
	[Header("Apariencia")]
	public GameObject modelo1;
	public Material[] material;
	public int materialnumero;
	Renderer rend;

	[Header("Movimiento")]
	public Transform target1;
    public Transform target2;
	public Transform vulnerable;
	public Transform sincuerpo;
	private NavMeshAgent enemy;
	public int targetType = 0;

	[Header("Otros")]
	public int Estado = 0;
	public bool SoyBlinky = false;

	[Header("Imports")]
	public GameManager manager;
	public ControladorJugador pacman;
	public AudioSource comido;

	void Start() {
		rend = modelo1.GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[materialnumero];

		enemy = GetComponent<NavMeshAgent>();
    }
	
    void Update() {
		EnemyMove();
		EnemySpeed();
		EnemyTextura();
	}
	
	void EnemyMove(){
		if (Estado == 2) {		//fantasma sin cuerpo
			enemy.destination = sincuerpo.position;
		} else if (Estado == 1) {		//fantasma vulnerable
			enemy.destination = vulnerable.position;
		} else {
			if (targetType == 1){	//Inky
				enemy.destination = (target1.position * 2) - target2.position;
			} else if (targetType == 2){	//Clyde
				if (
					enemy.transform.position.x - target1.transform.position.x >= 64 || //left
					enemy.transform.position.x - target1.transform.position.x <= -64 || //right
					enemy.transform.position.z - target1.transform.position.z >= 64 || //up
					enemy.transform.position.z - target1.transform.position.z <= -64 //down
				){
					enemy.destination = target1.position;//si esta fuera del radio, perseguir a Pac-Man
				} else {
					enemy.destination = target2.position;//si esta dentro del radio, huir
				}
			} else {		//Blinky y Pinky
				enemy.destination = target1.position;
			}
		}
	}
	void EnemySpeed() {
		if (manager.Pausa == true) {
			enemy.speed = 0;
		} else {
			if (Estado == 2){
				enemy.speed = 40;
			} else if (Estado == 1) {
				enemy.speed = 15;
			} else {
				if (SoyBlinky == true) {
					if (InterfazBolitas.bolitascomidas >= 234) {
						enemy.speed = 35;
					} else if (InterfazBolitas.bolitascomidas >= 224 && InterfazBolitas.bolitascomidas < 234) {
						enemy.speed = 30;
					} else if (InterfazBolitas.bolitascomidas < 224) {
						enemy.speed = 25;
					}
				} else {
					enemy.speed = 25;
				}
			}
		}
	}

	void EnemyTextura() {
		if (Estado == 2) {
			materialnumero = 3;
		} else if (Estado == 1) {
			if (manager.time == 7 && ((manager.time60fps & 0x04) == 0)) {
				materialnumero = 2;
			} else {
				materialnumero = 1;
			}
		} else {
			materialnumero = 0;
		}
		rend.sharedMaterial = material[materialnumero];
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (Estado == 0) {
				if (pacman.Invencible == false) {
					pacman.Muerto = true;
				} else {
					return;
				}
			} else if (Estado == 1) {
				Estado = 2;
				InterfazPuntos.puntajetotal += pacman.puntosmultiplicador * 200;
				pacman.puntosmultiplicador *= 2;
				comido.Play();
			}
		}
	}
}
