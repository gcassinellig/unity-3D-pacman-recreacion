using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[Header("Otros")]
	public bool Pausa = false;
	public bool NivelCompletado = false;
	
	[Header("Sonidos")]
	public AudioSource s1;
    public AudioSource s2;
    public AudioSource s3;
    public AudioSource s4;
    public AudioSource s5;
    public AudioSource svul;
    public AudioSource sojos;
	public AudioSource start;
	public AudioSource musica;
	public AudioSource completo;

	[Header("Energizante")]
	public byte time;
	public byte time60fps;
	public bool energizantereset;
	
	[Header("Fruta")]
	public byte frutatime;
	public byte frutatime60fps;

	[Header("Imports")]
	public FadeManager fade;
	public ControladorJugador pacman;
	public ControladorEnemigo[] fantasmas;

	void Start() {
		if (NivelCompletado == false) {
			start.Play();
		}
	}
	
	void Update() {
		if (start.isPlaying) {
			Pausa = true;
		} else {
			Pausa = false;
			if (!musica.isPlaying) {
				musica.Play();
			}
			FantasmaSonidos();
			EnergizanteUpdate();
		}
	}
	
	void FantasmaSonidos() {
		int sonidofetch = 0;

		for (int i = 0; i < 4; i++) {
			if (fantasmas[i].Estado == 2) {
				sonidofetch = 2;
				break;
			}
		}
		if (sonidofetch != 2) {
			for (int i = 0; i < 4; i++) {
				if (fantasmas[i].Estado == 1) {
					sonidofetch = 1;
					break;
				} else {
					sonidofetch = 0;
				}
			}
		}

		switch (sonidofetch) {
			case 2:
				s1.Stop();
				s2.Stop();
				s3.Stop();
				s4.Stop();
				s5.Stop();
				svul.Stop();
				if (!sojos.isPlaying) {
					sojos.Play();
				}
				break;
			case 1:
				s1.Stop();
				s2.Stop();
				s3.Stop();
				s4.Stop();
				s5.Stop();
				sojos.Stop();
				if (!svul.isPlaying) {
					svul.Play();
				}
				break;
			case 0:
				svul.Stop();
				sojos.Stop();
				if (InterfazBolitas.bolitascomidas < 116)
				{
					if (!s1.isPlaying)
					{
						s1.Play();
					}
				}
				else if (InterfazBolitas.bolitascomidas >= 116 && InterfazBolitas.bolitascomidas < 180)
				{
					s1.Stop();
					if (!s2.isPlaying)
					{
						s2.Play();
					}
				}
				else if (InterfazBolitas.bolitascomidas >= 180 && InterfazBolitas.bolitascomidas < 212)
				{
					s2.Stop();
					if (!s3.isPlaying)
					{
						s3.Play();
					}
				}
				else if (InterfazBolitas.bolitascomidas >= 212 && InterfazBolitas.bolitascomidas < 228)
				{
					s3.Stop();
					if (!s4.isPlaying)
					{
						s4.Play();
					}
				}
				else if (InterfazBolitas.bolitascomidas >= 228 && InterfazBolitas.bolitascomidas < 244)
				{
					s4.Stop();
					if (!s5.isPlaying)
					{
						s5.Play();
					}
				}
				else if (InterfazBolitas.bolitascomidas == 244)
				{
					Pausa = true;
					fade.FadeToLevel(0);
					NivelCompletado = true;
					s1.Stop();
					s2.Stop();
					s3.Stop();
					s4.Stop();
					s5.Stop();
					svul.Stop();
					musica.Stop();
					completo.Play();
				}
                break;
            default:
				break;
        }
    }

	void EnergizanteUpdate() {
		if (energizantereset == true) {
			time60fps = 0;
			time = 0;
			energizantereset = false;
		}
		if (pacman.Energizante == true) {
            time60fps += 1;
			if (time60fps == 60) {
				time++;
				time60fps = 0;
			}
			if (time == 8) {
				pacman.Energizante = false;
				time60fps = 0;
				time = 0;
				for (int i = 0; i < 4; i++) {
					if (fantasmas[i].Estado < 2) {
						fantasmas[i].Estado = 0;
					}
				}
			}
        }
	}

}