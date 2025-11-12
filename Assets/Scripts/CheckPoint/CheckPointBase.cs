using UnityEngine;
using TMPro;

public class CheckPointBase : MonoBehaviour
{
	public MeshRenderer meshRenderer;
	public int key = 01;
	public TMP_Text textMesh;

	private bool checkpointActive = false;
	private string checkpointKey = "CheckpointKey";

	private void OnTriggerEnter(Collider other)
	{
		if(!checkpointActive && other.transform.tag == "Player")
		{
			CheckCheckPoint();
			
		}
	}

	private void CheckCheckPoint()
	{
		TurnItOn();
		SaveCheckPoint();
	}

	[NaughtyAttributes.Button]
	private void TurnItOn()
	{
		meshRenderer.material.SetColor("_EmissionColor", Color.black);


	}
	private void TurnItOff()
	{
		meshRenderer.material.SetColor("_EmissionColor", Color.white);

	}

	private void SaveCheckPoint()
	{
		/*if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
			PlayerPrefs.SetInt(checkpointKey, key); */

		CheckPointManager.Instance.SaveCheckPoint(key);
					 
		checkpointActive = true;
		textMesh.text = "CHECKPOINT";
		Invoke(nameof(DeleteText), 3f);
	}

	private void DeleteText()
	{
		textMesh.text = "";
	}
}
