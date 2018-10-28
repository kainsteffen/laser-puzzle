using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBlock : MonoBehaviour
 {
	public List<string> targetSource;
	public List<string> currentSource;

	public Text targetSourceLabel;
	public Text currentSourceLabel;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentSource.Clear();
		targetSourceLabel.text = sourceToString(targetSource);
		currentSourceLabel.text = "(     )";
	}
	public bool GetGoalStatus()
	{
		targetSource.Sort();
		currentSource.Sort();

		if(targetSource.Count != currentSource.Count)
		{
			return false;
		}

		for(int i = 0; i < currentSource.Count; i++)
		{
			if(currentSource[i] != targetSource[i])
			{
				return false;
			}
		}

		return true;
	}

	public void showSource()
	{
		currentSourceLabel.text = "(" + sourceToString(currentSource) + ")";
	}

	public void injectSource(List<string> source)
	{
		currentSource.AddRange(source);
		showSource();
	}

	string sourceToString(List<string> source)
	{
		source.Sort();
		string result = "";
		foreach(string s in source)
		{
			result += s;
		}

		return result;
	}
}
