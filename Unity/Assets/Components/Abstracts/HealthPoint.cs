using UnityEngine;
using System.Collections;

public class HealthPoint : MonoBehaviour
{

	public int value = 9;
		
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void OneDown ()
	{
		--value;
	}
}
