using UnityEngine;
using System.Collections;

public class RocketsYo : MonoBehaviour {
	GameObject player;
	SteeringBehaviours myBehaviourScript, playerBehaviourScript;
	// Use this for initialization
	void Start () {
		player = GameManager.leader;
		myBehaviourScript = this.GetComponent<SteeringBehaviours> ();
		playerBehaviourScript = player.GetComponent<SteeringBehaviours> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerBehaviourScript.target != player) {
			myBehaviourScript.target = playerBehaviourScript.target;
		}
		myBehaviourScript.pursueEnabled = true;
		myBehaviourScript.alignmentEnabled = true;
		if(Vector3.Distance(this.transform.position,myBehaviourScript.target.transform.position)<3){
			myBehaviourScript.target.SetActive(false);
			this.gameObject.SetActive(false);
		}

	}

	void OnCollisionEnter(Collision col){
		Debug.Log (col.gameObject);
	}
}
