using UnityEngine;
using System.Collections;

public class ArriveState : State {

	public float changeStateTime, changeStateTimer = Random.Range(5,10); //A very simple change state condition!
	GameObject leader = GameManager.leader;
	GameObject leaderTarget = GameManager.leaderTarget;
	public ArriveState(GameObject myGameObject):base (myGameObject){

	}

	public override void Enter() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//This is where you toggle the steering behaviours ON!
		myGameObject.GetComponent<SteeringBehaviours>().arriveEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().seperationEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().cohesionEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().alignmentEnabled = true;
	}

	public override void Update() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//This is where we calculate stuff, like the condition to transition to the next state
		changeStateTime += Time.deltaTime;
		if (leader.GetComponent<SteeringBehaviours> ().offsetPursuitEnabled == true) {
			if (GameManager.leaderTarget == myGameObject) { //if chasing this boid put it in front of player using offset pursuit
				myGameObject.GetComponent<SteeringBehaviours> ().offsetPursuitOffset = new Vector3 (Random.Range (-10, 10), Random.Range (0, 10), Random.Range (10, 40));
				myGameObject.GetComponent<StateMachine> ().SwitchState (new FleeState (myGameObject));
			}else{ //flee player
				myGameObject.GetComponent<StateMachine> ().SwitchState (new PatrolState (myGameObject));

			}
		}
	}

	public override void Exit() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//this is where we turn off all steering behaviours, as the new state we transition to will enable only the ones it needs
		myGameObject.GetComponent<SteeringBehaviours>().TurnOffAll();
	}
}
