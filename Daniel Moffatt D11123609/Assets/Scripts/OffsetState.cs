using UnityEngine;
using System.Collections;

public class OffsetState : State {

	public float changeStateTime, changeStateTimer = Random.Range(5,10); //A very simple change state condition!
	bool hitByRocket;
	public OffsetState(GameObject myGameObject):base (myGameObject) //constructor that needs the same argument as the State base class constructor. use :base(GameObject) to inherit the same myGameObject reference, so this class can access the gameobject it's refereing to
	{
		
	}
	
	public override void Enter() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//This is where you toggle the steering behaviours ON!
		hitByRocket = false;
		myGameObject.GetComponent<SteeringBehaviours>().offsetPursuitEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().seperationEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().cohesionEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().alignmentEnabled = true;
	}
	
	public override void Update() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//This is where we calculate stuff, like the condition to transition to the next state
		changeStateTime += Time.deltaTime;
		if (hitByRocket) {
			myGameObject.transform.position = Vector3.zero;
			myGameObject.GetComponent<StateMachine> ().SwitchState (new ArriveState (myGameObject));

		} else if (changeStateTime < 0) {
			myGameObject.GetComponent<SteeringBehaviours>().offsetPursuitOffset = new Vector3(Random.Range (-10, 10), Random.Range (0, 10), Random.Range (-50,-10));
			changeStateTimer = 5;
		}
	}
	
	public override void Exit() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//this is where we turn off all steering behaviours, as the new state we transition to will enable only the ones it needs
		myGameObject.GetComponent<SteeringBehaviours>().TurnOffAll();
	}

	void RandState(){
		int randomState = Random.Range(1,5);
		if(randomState == 1){
			myGameObject.GetComponent<StateMachine>().SwitchState (new ChaseState(myGameObject));
		}else if(randomState == 2){
			myGameObject.GetComponent<StateMachine>().SwitchState (new ArriveState(myGameObject));
			
		}else if(randomState == 3){
			myGameObject.GetComponent<StateMachine>().SwitchState (new PatrolState(myGameObject));
			
		}else if(randomState == 4){
			myGameObject.GetComponent<StateMachine>().SwitchState (new TrackState(myGameObject));
			
		}else if(randomState == 5){
			myGameObject.GetComponent<StateMachine>().SwitchState (new FleeState(myGameObject));
		}
	}
}

