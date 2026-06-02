using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(CharacterController))]
public class ControladorJugador : MonoBehaviour {
	[Header("Velocidades")]
	public float VelocidadNormal = 7f;
	public float VelocidadCorrer = 15f;
	public float VelocidadRotacion = 30f;
	public float SaltoVelocidad = 4f;
	public float Gravedad = -20f;
	public float VelocidadActual = 0;

	[Header("Variables")]
	public bool puedoSaltar = true;
	public bool Muerto = false;
	public bool Invencible = false;
	public bool Energizante = false;
	private float cameraVerticalAngle;

	[Header("Energizante")]
	public int puntosmultiplicador = 0;

	[Header("Imports")]
	public AudioSource sonidomuerto;
	public GameManager manager;
	public Animator animator;
	public Camera camara;
	public FadeManager fade;
	public GameObject luz;

	Vector3 Tecla = Vector3.zero;
	Vector3 RotacionInput = Vector3.zero;
	CharacterController characterController;
	
	private void Awake() {
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animator.SetBool("Salto", false);
		animator.SetBool("Grounded", false);
	}
	
	private void Update(){
		CheckMuerto();
		Look();
		if (manager.Pausa == false) {
			Move();
		}
		CheckEnergizante();
	}
	
	private void Move(){
		if (characterController.isGrounded) {
			Tecla = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
			Tecla = Vector3.ClampMagnitude(Tecla, 1f);
			transform.Rotate(0, Tecla.x * Time.deltaTime * VelocidadRotacion, 0);
			animator.SetBool("Grounded", true);
			animator.SetFloat("VelX", Tecla.x);
			animator.SetFloat("VelZ", Tecla.z);
			if (Input.GetKey(KeyCode.Z)) {
				VelocidadActual = VelocidadCorrer;
			} else {
				VelocidadActual = VelocidadNormal;
			}
			Tecla = transform.TransformDirection(Tecla) * VelocidadActual;

			if (puedoSaltar == true) {
				if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
					animator.SetBool("Salto", true);
					animator.SetBool("Grounded", false);
					puedoSaltar = false;
					Tecla.y = Mathf.Sqrt(SaltoVelocidad * -2f * Gravedad);
				}
			} else {
				animator.SetBool("Salto", false);
				animator.SetBool("Grounded", true);
				puedoSaltar = true;
			}

		} else {
			puedoSaltar = false;
			animator.SetBool("Grounded", false);
		}
		Tecla.y += Gravedad * Time.deltaTime;
		characterController.Move(Tecla * Time.deltaTime);
	}
	
	private void Look() {
		RotacionInput.x = Input.GetAxis ("Mouse X") * VelocidadRotacion* Time.deltaTime;
		RotacionInput.y = Input.GetAxis ("Mouse Y") * VelocidadRotacion* Time.deltaTime;

		cameraVerticalAngle += RotacionInput.y;
		cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

		transform.Rotate(Vector3.up* RotacionInput.x);
		camara.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle,0f,0f);

	}

	void CheckMuerto() {
		if (Muerto == true) {
			VelocidadNormal = 0;
			VelocidadCorrer = 0;
			sonidomuerto.Play();
			Invencible = true;
			fade.FadeToLevel(0);
			manager.NivelCompletado = false;
			Muerto = false;
		}
    }

	void CheckEnergizante()  {
		if (Energizante == true) {
			luz.SetActive(true);
		} else {
			luz.SetActive(false);
		}

	}
}