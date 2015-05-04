using UnityEngine;
using System.Collections;
public class beAtThis : MonoBehaviour {
	public GameObject player;
	public Vector3 offset;
	void Update () {
		transform.position = player.transform.position+offset;
	}
}