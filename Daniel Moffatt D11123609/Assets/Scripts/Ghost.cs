using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

	void OnEnable(){
		transform.localScale = Vector3.one * 5;
		Invoke ("SwitchOff", 1);
	}

	void Start(){

	}

	void Update(){
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, Time.deltaTime*5);
	}

	void SwitchOff(){
		this.gameObject.SetActive (false);
	}

	void OnDisable(){
		CancelInvoke ();
	}
}
