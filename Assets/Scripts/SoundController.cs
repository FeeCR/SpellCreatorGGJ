using UnityEngine;
using System.Collections;
using System;

public class SoundController : MonoBehaviour {

	public static SoundController instance;
	public AudioSource splash, risadaDemonio, vento, agua;
	public AudioSource[] sustos;
	private DateTime horaSusto = DateTime.Now;

	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else {
			Destroy(gameObject);    
		}
		DontDestroyOnLoad(gameObject);


		DefineSomRandom ();
	}

	void Update(){
		if (DateTime.Compare (DateTime.Now, horaSusto) > 0) {
			PlaySomSustoRandom();
			DefineSomRandom();
		
		}

	}


	public void PlaySomQuedaIngrediente(){
		splash.Play ();
	}


	public void PlaySomSustoRandom(){
		int random = UnityEngine.Random.Range (0, sustos.Length);

		sustos [random].Play ();

	}

	public void DefineSomRandom (){
		int random = UnityEngine.Random.Range (15, 31);
		DateTime temp = DateTime.Now;
		horaSusto = temp.AddSeconds (random);
		Debug.Log ("Susto: " + horaSusto);

	}

	public void PlaySomGameOver(){
		Debug.Log("Aqui primeiro");
		StartCoroutine (GameOverSom ());
	}


	public IEnumerator GameOverSom(){
		Debug.Log("Aqui depois");

		sustos [2].Play ();

		yield return new WaitForSeconds (0.57f);
		PausarSons ();
		GameGlobal.instance.DesativaCamera ();
		yield return new WaitForSeconds (2.2f);
		PlaySomDemoniaco ();
		Application.LoadLevel ("GameOver");
	}

	public void PlaySomDemoniaco(){
		risadaDemonio.Play ();

	}

	public void PausarSons(){
		splash.Stop ();
		vento.Stop ();
		agua.Stop ();
	}

}


