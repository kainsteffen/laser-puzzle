using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour 
{
	private Quaternion targetRotation;
	public bool rotating = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rotating)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);

			if(Quaternion.Angle(transform.rotation, targetRotation) < 10)
			{
				transform.rotation = targetRotation;
				rotating = false;
			}
		}
	}

	public void rotate90()
	{
		if(!rotating)
		{
			rotating = true;
			targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);	
		}
	}
}
