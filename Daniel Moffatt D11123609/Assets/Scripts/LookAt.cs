using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public GameObject leader;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (GameManager.leader.transform.position, GameManager.leaderTarget.transform.position) < 50) {
			transform.LookAt (GameManager.leaderTarget.transform.position);
		} else {
			transform.LookAt(GameManager.leader.transform.position);
		}
	}
}
