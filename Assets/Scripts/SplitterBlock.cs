using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterBlock : MonoBehaviour 
{
	public List<string> currentSource;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		DeactivateLasers();
	}

	public void ShootLasers(List<string> source)
	{

		foreach(LineRenderer lr in GetComponentsInChildren<LineRenderer>())
		{
			lr.enabled = true;
			currentSource = source;
		}
	}

	public void DeactivateLasers()
	{
		foreach(LineRenderer lr in GetComponentsInChildren<LineRenderer>())
		{
			lr.enabled = false;
			currentSource.Clear();
		}
	}
}
