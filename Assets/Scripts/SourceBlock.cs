using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceBlock : MonoBehaviour 
{

	public string source;
	public Text sourceLabel;

	public ParticleSystem highlighEffect;


	// Use this for initialization
	void Start () 
	{
		sourceLabel.text = source;
	}
	
	// Update is called once per frame
	void Update () 
	{
		StopEmitHighlight();
	}

	public void EmitHighlight()
	{
		highlighEffect.Play();
	}

	public void StopEmitHighlight()
	{
		highlighEffect.Stop();
	}
}
