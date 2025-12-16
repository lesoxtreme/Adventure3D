using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
	public void LoadLevel(int level)
	{
		SceneManager.LoadScene(level);
	}
}
