using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TeleportManager : MonoBehaviour {
	[Header ("Teleport")]
	public Transform nuevaposicion;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			CharacterController characterController = other.GetComponent<CharacterController>();
			characterController.enabled = false;		//desactivar para mover
			other.transform.position = nuevaposicion.position;
			characterController.enabled = true;			//reactivar
		} else {
			NavMeshAgent navagent = other.GetComponent<NavMeshAgent>();
			navagent.Warp(nuevaposicion.position);
		}
	}
}