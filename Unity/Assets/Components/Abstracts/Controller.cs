using UnityEngine;
using System.Collections;

public abstract class Controller : MonoBehaviour
{
	public GameObject snowBallPrefab;
	
	private GameObject snowBallInstance;
	
	private SnowBallScript snowBallInstanceController;
	
	public enum CharacterState
	{
		IS_AIMING,
		IS_DUCKING,
		IS_SHOOTING,
		IS_CHARGING
	}
	
	public CharacterState state = CharacterState.IS_AIMING;
	
	public void Charge ()
	{
		state = CharacterState.IS_CHARGING;
		
		float ballDistance = 0.5f;
		
		float ballHeight = 1.8f;
		
		snowBallInstance = Instantiate (
			snowBallPrefab,
			transform.forward + Vector3.one * ballDistance + Vector3.up * ballHeight, 
			transform.rotation) as GameObject;
		
		snowBallInstance.transform.SetParent (transform);
		
		snowBallInstanceController = snowBallInstance.GetComponent<SnowBallScript> ();
		snowBallInstanceController.Charge ();
	}
	
	public void Aim ()
	{
		if (state != CharacterState.IS_AIMING) {
			snowBallInstanceController.Aim ();
		}
	}
	
	public void Shoot ()
	{
		if (snowBallInstanceController != null) {
			snowBallInstanceController.Shoot (transform.forward);
		}
	}
	
	public void Dodge ()
	{
		
	}
}
