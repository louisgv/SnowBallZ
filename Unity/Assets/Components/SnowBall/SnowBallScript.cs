using UnityEngine;
using System.Collections;

public class SnowBallScript : MonoBehaviour
{
	
	public const float SIZE_MAX = 4.5f;
	
	public float sizeSpeed = 0.18f;
	 
	public float speed = 45.0f;
	 
	public GameObject owner;
	
	private Vector3 direction;
	 
	public enum BallState
	{
		IS_AIMING,
		IS_CHARGING,
		IS_SHOOTING,
		IS_EXPLODING
	}
	 
	public BallState state = BallState.IS_AIMING;
	 
	// Use this for initialization
	void Start ()
	{
		direction = transform.forward;
		transform.localScale *= 0.1f;
	}
	
	public void Aim ()
	{
		state = BallState.IS_AIMING;
	}
	
	public void Charge ()
	{
		state = BallState.IS_CHARGING;
	}
	
	public void Shoot (Vector3 shootDirection)
	{
		direction = shootDirection;
		state = BallState.IS_SHOOTING;
	}
	
	private void Damage (Collider other)
	{
		Debug.Log ("Damaging " + other.name);
		
		if (other.transform.parent != null) {
			// Getting parent's HP and decrease them
			HealthPoint otherHP = other.transform.parent.GetComponent<HealthPoint> ();
			otherHP.OneDown ();
			state = BallState.IS_EXPLODING;
		}
	}
	
	public void OnTriggerEnter (Collider other)
	{		
		//TODO: IF other is AI or Player, decrease health
		if (owner != null) {
			if (other.CompareTag ("Wall") && !owner.Equals (other.transform.parent.GetComponent<WallsScript> ().owner)) {
				Damage (other);
			}
			if (other.CompareTag ("Character") && !owner.Equals (other.transform.parent.gameObject)) {
				Damage (other);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state.Equals (BallState.IS_SHOOTING)) {
			transform.Translate (direction * Time.deltaTime * speed);
		}
		
		if (state.Equals (BallState.IS_CHARGING)) {
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * SIZE_MAX, sizeSpeed * Time.deltaTime);			
		}
		
		if (state.Equals (BallState.IS_EXPLODING)) {
			//TODO: Play the exploding animation
		}
		
		if (transform.localPosition.magnitude > 100.0f) {
			Destroy (gameObject);
		}
	}
}
