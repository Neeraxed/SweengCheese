using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
	public static bool gameOver;
	public GameObject gameOverPanel;
	
	public static bool isGameStarted;
	public GameObject startingText;
	public GameObject SwipeToMoveText;
	public GameObject ArrowR;
	public GameObject ArrowL;
	
	public static int numberOfCheese;
	
	public Text cheeseText;
	/*
	public Text distance;
	
	public GameObject playerForCoordinates;
	*/
	

	
    void Start()
    {
        gameOver = false;
		Time.timeScale =1 ;
		isGameStarted = false;
		numberOfCheese = 0;
		
		/*playerForCoordinates = GameObject.FindGameObjectWithTag("Player");*/
		
    }

    void Update()
    {
        if(gameOver) 
		{
			Time.timeScale = 0;
			gameOverPanel.SetActive(true);
			
		}
		cheeseText.text = "Cheese: " +numberOfCheese;
		/*distance.text = "Distance: " + playerForCoordinates.transform.position.z; */
		if(SwipeManager.tap)
		{
			
			Destroy(startingText); 
			Destroy(SwipeToMoveText); 
			Destroy(ArrowR); 
			Destroy(ArrowL); 
			isGameStarted = true;
		}
		
		
    }
}
