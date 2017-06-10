using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
	public KeybindingManager keybindingManager;
	protected AvailablePlayerActions availablePlayerActions;
	public GameObject thrustersHolder;
	private List<ParticleSystem> thrusterSystems;

	// Use this for initialization
	void Start () {
		thrusterSystems = new List<ParticleSystem>();
		gameObject.GetComponentsInChildren<ParticleSystem>(true, thrusterSystems);

		availablePlayerActions = keybindingManager.GetCurrentAvailablePlayerActions();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if(availablePlayerActions.forward.State) 
		{
			for(int i = 0; i < thrusterSystems.Count; i++) 
			{
				thrusterSystems[i].Emit(2);
			}
		}
	}
}
