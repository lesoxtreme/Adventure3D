using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
public class ItemCollectableBase : MonoBehaviour
{
    public ItemType itemType;
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float timeToHide = 3;
    public GameObject graphicItem;
    public Collider collider;

    [Header("Sounds")]
    public AudioSource audioSource;
    private SFXSetup _currentSFXSetup;
    public SFXType sfxType;

    private void Awake()
    {
       // if(particleSystem != null) particleSystem.transform.SetParent(null);   
       _currentSFXSetup = SoundManager.Instance.GetSFXByType(sfxType);
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    private void PlaySFX()
    {
        SFXPool.Instance.Play(sfxType);
    }

    protected virtual void Collect()
    {
        PlaySFX();
        if(collider !=null) collider.enabled = false;
        if(graphicItem !=null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        Debug.Log("teste");
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if(particleSystem != null) particleSystem.Play();

        
        //audioSource.clip = _currentSFXSetup.audioClip;
        //audioSource.Play();


        ItemManager.Instance.AddByType(itemType);
    }
}
}
