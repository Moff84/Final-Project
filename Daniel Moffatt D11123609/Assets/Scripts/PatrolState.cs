using UnityEngine;
using System.Collections;

public class PatrolState : State {
	
	public float changeStateTime, changeStateTimer = Random.Range(5,10); //A very simple change state condition!

	public PatrolState (GameObject myGameObject):base (myGameObject) //constructor that needs the same argument as the State base class constructor. use :base(GameObject) to inherit the same myGameObject reference, so this class can access the gameobject it's refereing to
	{
		
	}
	
	public override void Enter() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//This is where you toggle the steering behaviours ON!
		myGameObject.GetComponent<SteeringBehaviours>().pathFollowEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().seperationEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().cohesionEnabled = true;
		myGameObject.GetComponent<SteeringBehaviours>().alignmentEnabled = true;
	}
	
	public override void Update() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//This is where we calculate stuff, like the condition to transition to the next state
		changeStateTime += Time.deltaTime;
		if(GameManager.leader.GetComponent<SteeringBehaviours>().pathFollowEnabled == true)
		{
			myGameObject.GetComponent<StateMachine>().SwitchState (new ArriveState(myGameObject));

		}	
	}
	
	public override void Exit() //override runs over the base class abstract method of the same name (abstract methods can't handle functionality, they are only a blueprint)
	{
		//this is where we turn off all steering behaviours, as the new state we transition to will enable only the ones it needs
		myGameObject.GetComponent<SteeringBehaviours>().TurnOffAll();
	}
}
