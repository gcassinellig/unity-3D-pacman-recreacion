using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TeleportManager : MonoBehaviour {
	[Header ("Teleport")]
	public Transform nuevaposicion;

	void OnTriggerEnter(Collider other) {
		other.transform.position = nuevaposicion.transform.position;
	}
}