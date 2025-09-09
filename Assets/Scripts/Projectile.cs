using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 dir;
    public float timeToDestroy = 2f;
    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    public void Awake()
    {
        //Destroy(gameObject, timeToDestroy);
    }
     public void StartProjectile()   
   {
        Invoke(nameof(FinishUsage), timeToDestroy);
   }
    private void FinishUsage()
    {
        gameObject.SetActive(false);
    }
}

