using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireLazers : MonoBehaviour {
	LineRenderer myLazer;
	Material lazerMaterial;
	public AudioSource lazerSound,gameMusic;
	Light myLight;
	public Text shootNowText;
	public float lazerSpeed;
	// Use this for initialization
	void Start () {
		lazerSound.enabled = false;
		shootNowText.enabled = false;
		myLazer = GetComponent<LineRenderer> ();
		myLight = GetComponent<Light> ();
		myLight.enabled = false;
		myLazer.enabled = false;
		shootNowText.enabled = false;
		lazerMaterial =myLazer.sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			StopCoroutine (CanShoot ());
			StartCoroutine (CanShoot ());
		}
			//if mouse not pressed and you can shoot then show player they can shoot
		if (Vector3.Distance (GameManager.leader.transform.position, GameManager.leaderTarget.transform.position) < 50 && Input.GetMouseButton(0) == false) {
			shootNowText.enabled = true;
		} else {
			shootNowText.enabled = false;
		}
		if (!GameManager.leaderTarget.activeInHierarchy) {
			shootNowText.enabled = false;
		}
	}

	IEnumerator CanShoot(){
		lazerMaterial.mainTextureOffset =Vector2.zero;

		if (Vector3.Distance (GameManager.leader.transform.position, GameManager.leaderTarget.transform.position) < 50) {
			myLazer.enabled = true;
			lazerMaterial.mainTextureOffset += new Vector2 (Time.deltaTime,Time.deltaTime);
			myLight.enabled = true;
			gameMusic.Pause();
			lazerSound.pitch = Random.Range(0.95f,1.05f);
			lazerSound.enabled = true;
			Camera.main.GetComponent<LookAt> ().leader = GameManager.leaderTarget;
			lazerSound.Play();
			GameManager.leaderTarget.GetComponent<ParticleSystem>().enableEmission = true;
		}
		while (Input.GetMouseButton(0)&&GameManager.leaderTarget.activeInHierarchy) {
			myLazer.SetPosition (0, this.transform.position);
			myLazer.SetPosition (1, GameManager.leaderTarget.transform.position);
			
			yield return null;
		}
		lazerSound.Stop ();
		Camera.main.GetComponent<LookAt> ().leader = GameManager.lamp;
		GameManager.leaderTarget.GetComponent<ParticleSystem>().enableEmission = false;
		myLazer.enabled = false;
		gameMusic.UnPause ();
		lazerSound.enabled = false;
		myLight.enabled = false;
	}
}
