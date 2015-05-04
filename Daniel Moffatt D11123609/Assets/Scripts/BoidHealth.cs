using UnityEngine;
using System.Collections;

public class BoidHealth : MonoBehaviour {
	public float boidHealth = 3;
	public GameObject ghost;

	void Update () {
		if (boidHealth < 0) {
			this.GetComponent<SteeringBehaviours>().TurnOffAll();
			boidHealth = Random.Range(3,5);
			GameManager.kills++;
			if(ghost == null){
				ghost = GameObject.Find("Ghost");
			}
			ghost.transform.position = this.transform.position;
			ghost.SetActive(true);
			this.gameObject.SetActive(false);
		}
		if(this.gameObject.GetComponent<ParticleSystem>().enableEmission == true){
			boidHealth -=Time.deltaTime;
		}
	
	}
}
