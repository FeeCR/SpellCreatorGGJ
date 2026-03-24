// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;


public class Teleport : MonoBehaviour {
  	private Vector3 startingPosition;
	public int indice;
	public float contador = 0, tempoTotal = 1f;
	public bool iniciarContagem = false;
	public Color corInicial;

  void Start() {
		startingPosition = transform.localPosition;
		corInicial = GetComponent<SpriteRenderer>().material.color;
    	SetGazedAt(false);

  }


	void Update(){
		if (iniciarContagem) {
			if (contador < tempoTotal) {
				contador += Time.deltaTime;
			} else {
				contador = 0;
				AddIngrediente();
				Debug.Log ("Olhou por um segundo e meio");
			}
		} else {
			contador = 0;
		
		}
	}


  public void SetGazedAt(bool gazedAt) {
		if (gazedAt) {
			if(ElementosController.instance.CheckIfIndiceIngredienteProximo(this.indice)){
				GetComponent<Renderer> ().material.color = Color.green;
				iniciarContagem = true;
				if(GameGlobal.instance != null){
					GameGlobal.instance.TrocaCorPonteiro(true);
				}
			}else{

				GetComponent<Renderer>().material.color = Color.white;
				GameGlobal.instance.TrocaCorPonteiro(false);
			}
		} else {
			GetComponent<Renderer>().material.color = corInicial;
			iniciarContagem = false;
			if(GameGlobal.instance != null){
				GameGlobal.instance.TrocaCorPonteiro(false);
			}
		}
  }

  public void Reset() {
    //transform.localPosition = startingPosition;
  }

  public void ToggleVRMode() {
    Cardboard.SDK.VRModeEnabled = !Cardboard.SDK.VRModeEnabled;
  }

  public void AddIngrediente() {
	if (ElementosController.instance.CheckIfIndiceIngredienteProximo (this.indice)) {
		this.gameObject.SetActive(false);
		ElementosController.instance.AddIngrediente(this.indice);
	}
  }
}
