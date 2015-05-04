using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text killsUI;
	public static GameObject[] boids;
	public GameObject player, rocketPrefab,front;
	public int numberOfRockets;
	public static int kills=0;
	public static GameObject leader,leaderTarget,lamp;
	public static bool rocketHit = false;
	public static Transform camStart;
	public static float lazerEnergy;

	void Start () 
	{
		lamp = front;
		camStart = Camera.main.transform;
		lazerEnergy = 5;
		leader = player;
		FindBoids();
		GiveBoidsChaseState();
	}

	void Update(){
		killsUI.text = "Kills: " + kills;
		leaderTarget = leader.GetComponent<SteeringBehaviours> ().target;
		if (Camera.main.GetComponent<LookAt> ().enabled == false) {
			Camera.main.transform.rotation = leader.transform.localRotation;
		}
//		if (lazerEnergy <= 5) {
//			lazerEnergy += Time.deltaTime;
//		}
	}
	
	void FindBoids()
	{
		boids = GameObject.FindGameObjectsWithTag("Boid");
	}
	
	void GiveBoidsChaseState()
	{
		foreach (GameObject boid in boids) {
			if (boids.Length != 0) {
				boid.GetComponent<StateMachine>().SwitchState (new ChaseState(boid));
			}
		}
	}
}
