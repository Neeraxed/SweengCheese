using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
		transform.Rotate(0, 50 * Time.deltaTime, 0);
        
    }
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			FindObjectOfType<AudioManager>().PlaySound("PickUpCheese");
			PlayerManager.numberOfCheese +=1;
			Destroy(gameObject);
		}			
	}
	
}
