using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGlobal : MonoBehaviour {

	public static GameGlobal instance;
	private Text ritual_Ui;
	private Text contador_UI;
	[HideInInspector]
	public static float contador = 5;

	public static int ritual = 1;
	[HideInInspector]
	private static int numeroIngredientes = 3;
	public static int frameCounter = 0;
	private Material _material;
	public Color corVerde = new Color();
	private Image imagemInstrucao;
	private bool trocarInstru = false;
	bool gameOver = false;

	public GameObject tela;

	void Start(){

		if (instance == null)
			instance = this;
		else {
			//Debug.Log("Deletou esse");
			Destroy(gameObject);    
		}
			

			
		DontDestroyOnLoad(gameObject);




		//instance = this;

		//contador = 60;
		//numeroIngredientes = 3;

		GameGlobal.instance.TrocaInstruPocao ();
		EncontraTextos ();

	}
	
	// Update is called once per frame
	void Update () {
		if (contador > 0 && !gameOver) {
			contador -= Time.deltaTime;
			if(contador > 60){
				int minutos = (int)contador/60;
				int segundos = (int)contador -(minutos * 60);
				if(contador_UI != null){
					contador_UI.text = string.Format("{0:00}", minutos) + ":" + string.Format("{0:00}", segundos);

				}else{
					EncontraTextos();
				}

			}else{
				if(contador_UI != null){
					contador_UI.text = string.Format("{0:\\0\\0\\:00}", contador);

				}else{
					EncontraTextos();
				}

			}

		} else if(!gameOver){
			ElementosController.instance.CallGameOver();
			gameOver = true;
		}




		if (frameCounter < 30) {
			frameCounter++;
		} else {
			frameCounter = 0;
			ritual_Ui = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>() as Text;
			if(ritual_Ui != null){
				ritual_Ui.text = "Ritual: " + "\n" + ritual;

			}
		}


		if (trocarInstru) {
			TrocaInstruPocao();
			trocarInstru = false;
		}
	}

	public void AddGameFeito(){
		contador += 20f;
		Debug.Log ("Ritual Antes: " + ritual);
		ritual++;
		ritual_Ui.text = "Ritual: " + "\n" + ritual;
		Debug.Log ("Ritual: " + ritual);

		if (ritual % 2 == 1) {
			numeroIngredientes++;
		}


		Application.LoadLevel("Main");
		//StartCoroutine (EncontraTextosPosGame ()); 
		//EncontraTextos();
	}


	public int GetnumeroIngredientes(){
		return numeroIngredientes;

	}

	public void EncontraTextos(){

		ritual_Ui = GameObject.FindGameObjectWithTag("Level").GetComponent<Text>() as Text;
		ritual_Ui.text = "Ritual: " + ritual.ToString();
		contador_UI = GameObject.FindGameObjectWithTag ("Contador").GetComponent<Text> () as Text;
		_material = GameObject.FindGameObjectWithTag ("Ponteiro").GetComponent<MeshRenderer>().material;
		imagemInstrucao = GameObject.FindGameObjectWithTag ("InstrucaoIng").GetComponent<Image> () as Image;

	}


	public void TrocaCorPonteiro(bool verde){
		if (_material != null) {
			Color cor = Color.white;
			if(verde){
				cor = corVerde;

			}else{

				cor = Color.white;
			}

			_material.color = cor;
		
		} else {
			EncontraTextos();
		
		}
	}



	public void TrocaInstruPocao(){
		if (imagemInstrucao != null) {
			imagemInstrucao.sprite = ElementosController.instance.GetSpriteInstru();

		} else {
			EncontraTextos();
		}

	}


	public void DesativaCamera(){

		tela.SetActive (true);


	}

}
