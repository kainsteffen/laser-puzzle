using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public static GameController Instance;
	public GameObject gameOverScreen, winningScreen;
	public GameObject[] goalBlocks;
	public float playTime;
	public float timer;

	private void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(Instance);
		}
		else
		{
			Instance = this;
		}
	}

	// Use this for initialization
	void Start () 
	{
		timer = playTime;
		goalBlocks = GameObject.FindGameObjectsWithTag("Goal");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GoalMet())
		{
			StartCoroutine(ShowWinningScreen(2f));
		}
		else if(timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			ShowGameOverScreen();
		}
	}

	public bool GoalMet()
	{
		foreach(GameObject goalBlock in goalBlocks)
		{
			if(!goalBlock.GetComponent<GoalBlock>().GetGoalStatus())
			{
				return false;
			}
		}

		return true;
	}

	public IEnumerator ShowWinningScreen(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if(!winningScreen.activeSelf)
		{
			winningScreen.SetActive(true);
		}
	}

	public void	ShowGameOverScreen()
	{
		if(!gameOverScreen.activeSelf)
		{
			gameOverScreen.SetActive(true);
		}
	}

	public float GetRemainingTimeRatio()
	{
		return timer / playTime;
	}
	public void ReloadScene()
	{
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
	}
}
