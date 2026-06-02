using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaNuevoCuerpo : MonoBehaviour {
    [Header("Imports")]
	public ControladorEnemigo[] fantasmas;

	void Update() {
		for (int i = 0; i < 4; i++) {
			if (
				fantasmas[i].transform.position.x - this.transform.position.x < 1.5 &&
				fantasmas[i].transform.position.x + this.transform.position.x > -1.5 &&
				fantasmas[i].transform.position.z - this.transform.position.z > -1.5 &&
				fantasmas[i].transform.position.z + this.transform.position.z < 1.5
			) {
				if (fantasmas[i].Estado == 2){
					fantasmas[i].Estado = 0;
				}
            }
		}
	}
}
