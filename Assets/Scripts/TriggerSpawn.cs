using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
	public Transform SpawnPosition;
	public Transform Spawner;
	public Collider collider;
	private Player _player;

	private void Start()
	{
		_player = GameObject.FindObjectOfType<Player>();
	}

	private void OnTriggerEnter(Collider Player)
	{
			SpawnPosition = Instantiate(Spawner, SpawnPosition);
			SpawnPosition.transform.localPosition = SpawnPosition.transform.localEulerAngles = Vector3.zero;
			Destroy(collider);	
	}
}
