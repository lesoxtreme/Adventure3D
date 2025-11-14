using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
	public CinemachineVirtualCamera virtualCamera;

	public float shakeTime;

	//public CinemachineBasicMultiChannelPerlin c;
	[Header("Shake Values")]
	public float frequency = 3f;
	public float amplitude = 3f;
	public float time = .3f;

	[NaughtyAttributes.Button]
	public void Shake()
	{
		Shake(amplitude, frequency, time);
	}
	
	public void Shake(float amplitude, float frequency, float time)
	{
		//c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
		virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

		shakeTime = time;
	}

	private void Update()
	{
		if(shakeTime > 0)
		{
			shakeTime -= Time.deltaTime;
		}
		else
		{
			virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
			virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
		}
	}
}
