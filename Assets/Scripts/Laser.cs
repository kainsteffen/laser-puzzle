using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public bool shooting;
    public LineRenderer lineRenderer;
	public float length;

	private List<Vector3> positions;
 
	public List<string> currentSource;

	private Quaternion targetRotation;
	public bool rotating = false;



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
		if(rotating)
		{
			positions.Clear();
			positions.Add(transform.position);
			shootLaserOnce(transform.position, transform.up);
			Vector3[] positionsArray = positions.ToArray();
			lineRenderer.positionCount = positionsArray.Length;
			lineRenderer.SetPositions(positionsArray);

			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);

			if(Quaternion.Angle(transform.rotation, targetRotation) < 10)
			{
				transform.rotation = targetRotation;
				rotating = false;
			}
		}
		else
		{
			currentSource.Clear();
			positions.Clear();
			
			positions.Add(transform.position);

			shootLaser(transform.position, transform.up);

			Vector3[] positionsArray = positions.ToArray();
			lineRenderer.positionCount = positionsArray.Length;
			lineRenderer.SetPositions(positionsArray);
		}
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

					case "Source":
						currentSource.Add(hit.collider.GetComponent<SourceBlock>().source);
						hit.collider.GetComponent<SourceBlock>().EmitHighlight();
						int index = System.Array.IndexOf(raycast, hit);
						bool found = false;
						for(int i = index; i < raycast.Length; i++)
						{
							if(raycast[i].collider.tag == "Reflector")
							{
								found = true;
								break;
							}
						}

						if(!found)
						{
							positions.Add(startPos + direction * length);
						}

						break;

					case "Splitter":
						positions.Add(hit.point);
						hit.collider.GetComponent<SplitterBlock>().ShootLasers(currentSource);
						break;

					case "Terrain":
						positions.Add(hit.point);
						break;

					case "Goal"	:
						positions.Add(hit.point);
						hit.collider.GetComponent<GoalBlock>().injectSource(currentSource);
						break;
				}

				if(hit.collider.tag == "Reflector" || hit.collider.tag == "Terrain" || hit.collider.tag == "Goal" || hit.collider.tag == "Splitter")
				{
					break;
				}		
			}
		}
		else
		{			
			positions.Add(startPos + direction * length);
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
