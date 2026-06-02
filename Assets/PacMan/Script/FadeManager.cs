using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {
    [Header("Fade")]
    public Animator animator;
    public GameManager manager;
    public int nuevaescena = 0;

    public void FadeToLevel(int levelindex) {
        nuevaescena = levelindex;
        animator.SetBool("FadeOut", true);
    }

    public void OnFadeComplete() {
        InterfazBolitas.bolitascomidas = 0;
        if (manager.NivelCompletado == false)  {
            InterfazPuntos.puntajetotal = 0;
        }
        SceneManager.LoadScene(nuevaescena);
    }
}
