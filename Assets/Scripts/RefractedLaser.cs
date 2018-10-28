using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractedLaser : MonoBehaviour 
{
    public LineRenderer lineRenderer;
	public float length;

	private List<Vector3> positions;
 
	public List<string> currentSource;

	// Use this for initialization
	void Start ()
    {
		lineRenderer = GetComponent<LineRenderer>();
		currentSource = new List<string>();
		positions = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		currentSource = transform.parent.GetComponent<SplitterBlock>().currentSource;
		positions.Clear();
		
		positions.Add(transform.position);

		shootLaser(transform.position, transform.up);

		Vector3[] positionsArray = positions.ToArray();
		lineRenderer.positionCount = positionsArray.Length;
		lineRenderer.SetPositions(positionsArray);
	}

	void shootLaserOnce(Vector2 startPos, Vector2 direction)
	{
		positions.Add(startPos + direction * length);
	}

	void shootLaser(Vector2 startPos, Vector2 direction)
	{
		RaycastHit2D[] raycast = Physics2D.RaycastAll(startPos + direction * 0.5f, direction, length);

		if(raycast.Length > 0)
		{
			foreach(RaycastHit2D hit in raycast)
			{
				if(hit.collider.tag == "Source")
				{
					currentSource.Add(hit.collider.GetComponent<SourceBlock>().source);
				}
			}

			foreach(RaycastHit2D hit in raycast)
			{
				if(hit.collider.tag != "Source")
				{
					switch(hit.collider.tag)
					{
						case "Reflector":
							positions.Add(hit.point);
							if(hit.collider.GetComponent<Reflector>().rotating)
							{
								shootLaserOnce(hit.point, Vector2.Reflect(direction, hit.normal));
							}
							else
							{
								shootLaser(hit.point, Vector2.Reflect(direction, hit.normal));
							}

							break;

						case "Splitter":
							positions.Add(hit.point);
							hit.collider.GetComponent<SplitterBlock>().ShootLasers(currentSource);
							break;

						case "Goal"	:
							positions.Add(hit.point);
							hit.collider.GetComponent<GoalBlock>().injectSource(currentSource);
							break;
					}	
					break;
				}
			}


		}
		else
		{
			positions.Add(startPos + direction * length);
		}	
	}
}
