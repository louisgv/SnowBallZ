using UnityEngine;
using System.Collections;

public class SnowBallImpactScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	IEnumerator TimedDeath(){
		yield return new WaitForSeconds(1.6f);
		Destroy(this.gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
