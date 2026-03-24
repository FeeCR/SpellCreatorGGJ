using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementosController : MonoBehaviour {

	public static ElementosController instance;

	public int ElementosColetados = 0;
	private int numeroIngredientes = 0;
	private int numeroTotalElementos = 32;
	public int[] indicesRandons;
	private List<GameObject> listaRandomicaIndices = new List<GameObject>();
	public int[] numeroTiposCadaElemento = new int[8];
	private int[] numeroRestantesCadaElemento;
	private Transform myTransform;



	//numeroPocoes, numeroPenas, numeroTigelas, numeroFlores, numeroPedras, numeroOlhos, numeroGosmas, numeroAleatorios


	// Use this for initialization
	void Start () {
		instance = this;
		myTransform = this.gameObject.transform;
		numeroIngredientes = GameGlobal.instance.GetnumeroIngredientes();

		numeroRestantesCadaElemento = numeroTiposCadaElemento;

		DefineElementosAleatorios ();
		DefineIndicesAleatorios ();
		GameGlobal.instance.TrocaInstruPocao ();


	}


	public void DefineElementosAleatorios(){
		indicesRandons = new int[numeroIngredientes];

		for(int i = 0; i < indicesRandons.Length; i++){

			for(int a = 0; a < 100; a++){
				int random = Random.Range(0, numeroTotalElementos);
				if(!CheckIfIndiceIngrediente(random)){
					indicesRandons[i] = random;
					break;
				}
			}
		}
	}


	public bool CheckIfIndiceIngrediente(int random){
		for (int i = 0; i < indicesRandons.Length; i++) {
			if(random == indicesRandons[i]){

				return true;
			}
		
		}

		return false;
	}

	public void DefineIndicesAleatorios (){
		for (int i = 0; i < numeroTotalElementos; i++) {;
			listaRandomicaIndices.Add (myTransform.GetChild(i).gameObject);
		}

		int indice = 0;

		for (int b = 0; b < numeroTotalElementos; b++) {
			//Debug.Log("Indice sprite: " + b);
			int numeroRandom = Random.Range (0, listaRandomicaIndices.Count);
			Teleport item = listaRandomicaIndices[numeroRandom].GetComponent<Teleport>() as Teleport;
			item.indice = indice;
			indice++;

			//
			int tipoItemRandom = 0;


			for(int random = 0; random < 100; random++){
				tipoItemRandom = Random.Range(0, numeroTiposCadaElemento.Length);
				//Debug.Log("Numero Random: " + tipoItemRandom);

				if(numeroTiposCadaElemento[tipoItemRandom] > 0){
					//Debug.Log("Entrou com numero random: " + tipoItemRandom);
					SpriteRenderer _render = listaRandomicaIndices[numeroRandom].GetComponent<SpriteRenderer>() as SpriteRenderer;
					_render.sprite = SpriteLoader(tipoItemRandom);
					numeroRestantesCadaElemento[tipoItemRandom]--;
					break;
				}
				


			}

			listaRandomicaIndices.Remove(listaRandomicaIndices[numeroRandom]);
		}
	}

	public bool CheckIfIndiceIngredienteProximo(int indice){

		if (indice == indicesRandons [ElementosColetados]) {
		
			return true;
		}else
			return false;
	}

	public Sprite SpriteLoader(int tipoItemRandom){

		Sprite temp = new Sprite();

		if(tipoItemRandom == 0){
			temp = Resources.Load<Sprite> ("Pocoes/Pocoes" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}else if(tipoItemRandom == 1){
			temp = Resources.Load<Sprite> ("Penas/Pena" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}else if(tipoItemRandom == 2){
			temp = Resources.Load<Sprite> ("Potes/pote" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}else if(tipoItemRandom == 3){
			temp = Resources.Load<Sprite> ("Olhos/Olho" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}else if(tipoItemRandom == 4){
			temp = Resources.Load<Sprite> ("Gosmas/gosma" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}else if(tipoItemRandom == 5){
			temp = Resources.Load<Sprite> ("Misc/misc" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}else if(tipoItemRandom == 6){
			temp = Resources.Load<Sprite> ("Flores/flor" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}else if(tipoItemRandom == 7){
			temp = Resources.Load<Sprite> ("Pedras/pedra" + (numeroRestantesCadaElemento[tipoItemRandom] - 1).ToString());
		}

		return temp;

	}

	public void AddIngrediente(int indice){

		if (ElementosColetados < numeroIngredientes) {
			for (int i = 0; i < indicesRandons.Length; i++) {
				
				if (indicesRandons [i] == indice) {
					ElementosColetados++;
					SoundController.instance.PlaySomQuedaIngrediente();

					if(ElementosColetados >= numeroIngredientes){
						GameGlobal.instance.AddGameFeito();
					}else{

						GameGlobal.instance.TrocaInstruPocao ();
					}

					break;
				}
				
			}

		} else {
			Debug.Log("Encontrou todos");
			GameGlobal.instance.AddGameFeito();

		
		}
	}

	public void CallGameOver(){
		Debug.Log ("Game oVer");
		SoundController.instance.PlaySomGameOver ();
	}


	public Sprite GetSpriteInstru(){
		int indice = indicesRandons [ElementosColetados];

		for (int i = 0; i < numeroTotalElementos; i++) {
			Teleport script = myTransform.GetChild (i).GetComponent<Teleport> () as Teleport;
			if (script.indice == indice) {
				return script.gameObject.GetComponent<SpriteRenderer> ().sprite;
			}
		}

		return null;

	}
}

