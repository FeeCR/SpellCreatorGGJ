using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public float contador = 0;
	private bool comeca = false;
	public SpriteRenderer botaoMenu;

	void Start(){
		if (GameGlobal.instance != null) {
			Destroy (GameGlobal.instance.gameObject);
		}

		if (SoundController.instance != null) {
			Destroy(SoundController.instance.gameObject);
		
		}

	
	}

	public void Olhando(bool sim){
		if (sim) {
			Debug.Log("Olhando");
			botaoMenu.material.color = Color.green;
			comeca = true;
			
		} else {
			Debug.Log("Cancelando");
			comeca = false;
			botaoMenu.material.color = Color.white;
		
		}

	}

	void Update(){
		if (comeca) {

			if (contador < 3) {
				contador += Time.deltaTime;
			}else{

				CarregaMenu();
			}
		
		} else {
			contador = 0;
		
		}
	}

	public void CarregaMenu(){
		Application.LoadLevel(0);

	}

}
