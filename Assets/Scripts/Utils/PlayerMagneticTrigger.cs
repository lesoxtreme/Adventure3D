using UnityEngine;
using Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();
        if( i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }

    }
}
