using UnityEngine;
using System.Collections;

public class PlayerTargetSet : MonoBehaviour {
	public GameObject currentTarget;
	SteeringBehaviours playerBehaviour;
	public AudioClip bogeySpotted;
	public AudioSource playerAudio;

	// Use this for initialization
	void Start () {
		playerBehaviour = GetComponent<SteeringBehaviours> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTarget != playerBehaviour.target) {
			currentTarget = playerBehaviour.target;
		}
		if (!currentTarget.activeInHierarchy&& currentTarget!=null) { // if player has no target find nearest boid and follow path
			FindNewTarget();
			playerBehaviour.TurnOffAll();
			playerBehaviour.pathFollowEnabled = true;
		}
		if (Vector3.Distance (currentTarget.transform.position, this.transform.position) < 50&&currentTarget.activeInHierarchy == true) { //if distance from player to its target < 50 
			if(playerBehaviour.pathFollowEnabled == true){ //if on patrol pursue behind boid with the random numbers
				playerBehaviour.offsetPursuitOffset = new Vector3(Random.Range (-10, 10), Random.Range (-2, 2), Random.Range (-15,-10));
				playerBehaviour.TurnOffAll ();
				playerBehaviour.offsetPursuitEnabled = true;
				playerBehaviour.cohesionEnabled = true;
				playerBehaviour.alignmentEnabled = true;
				playerBehaviour.seperationEnabled = true;
				playerAudio.clip = bogeySpotted;
				playerAudio.PlayOneShot(bogeySpotted);
			}
		} else { //if the target is killed ie not active
			if(!currentTarget.activeInHierarchy){

				playerBehaviour.TurnOffAll();
				playerBehaviour.pathFollowEnabled = true;
				playerBehaviour.cohesionEnabled = true;
				playerBehaviour.alignmentEnabled = true;
				playerBehaviour.seperationEnabled = true;
			}
		}
	}

	void FindNewTarget(){
		foreach (GameObject boid in GameManager.boids) {
			if(Vector3.Distance(boid.transform.position,this.transform.position)<Vector3.Distance(this.transform.position,currentTarget.transform.position)){
				currentTarget = boid;
				playerBehaviour.target = boid;
			}
		}
	}
}
