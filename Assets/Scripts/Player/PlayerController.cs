using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
	private Vector3 direction;
	public float forwardSpeed;
	public float maxSpeed;
	//public Healthbar mFoodBar;
	
	//public int startFood;
	//public HUD Hud;
	
	
	
	private int desiredLane = 1; // 0 - left, 1 - middle, 2 - right
	public float laneDistance = 3;
	
	public float jumpForce;
	public float Gravity = -5;
	
	public Animator animator;

	
    void Start()
    {
		controller = GetComponent<CharacterController>();
		
		//mFoodBar = Hud.Transform.Find("Bars_Panel/Foodbar).GetComponent<Healthbar>();
		//mFoodBar.Min = 0;
		//mFoodBar.Max = Food;
		//startFood = Food;
		
		//InvokeRepeating("IncreaseHunger", 0, HungerRate);

    }
	
	//[Tooltip("Amount of food")]
	//public int Food = 100;
	
	//[Tooltip("Rate in seconds in which the hunger increases")]
	//public floar HungerRate = 0.5f;
	
	/*public void IncreaseHunger()
	{
		Food--;
		if (Food < 0) Food = 0;
		mFoodBar.SetValue(Food);
		if (IsDead)
		{
			CancelInvoke();
		}
		
	}
	public bool IsDead
	{
		get 
		{
			return Food == 0;
		}
	}
	
	public void Eat (int amount)
	{
		Food += amount;
		if(Food > startFood)
		{
			Food = startFood;
		}
		mFoodBar.SetValue(Food);
		
	}
	*/

    void Update()
    {
		if(!PlayerManager.isGameStarted) return;
		
		if(forwardSpeed < maxSpeed) 
		{
			forwardSpeed += 0.1f *Time.deltaTime;
		}
		animator.SetBool("isGameStarted", true);	
		controller.Move(direction * Time.fixedDeltaTime);
		
		/*animator.SetBool("isGrounded", controller.isGrounded);
		
		
		if(controller.isGrounded)
		{
			direction.y = 0;
			if(SwipeManager.swipeUp)
			{
				Jump();			
			}
		} else 
		{
			direction.y += Gravity*Time.deltaTime;
		}
		*/
		direction.z = forwardSpeed;
		
		
		if(SwipeManager.swipeRight)
		{
			desiredLane ++;
			if (desiredLane == 3)
				desiredLane = 2;
		}
		if(SwipeManager.swipeLeft)
		{
			desiredLane --;
			if (desiredLane == -1)
				desiredLane = 0;
		}
		
		Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
		
		if(desiredLane ==0)
		{
			targetPosition += Vector3.left *laneDistance;
		} 
		else if(desiredLane == 2) 
		{
			targetPosition += Vector3.right *laneDistance;
		}

		//transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
		if( transform.position == targetPosition)
			return;
		Vector3 diff = targetPosition - transform.position;
		Vector3 moveDir = diff.normalized *30 *Time.deltaTime;
		if(moveDir.sqrMagnitude < diff.sqrMagnitude)
			controller.Move(moveDir);
		else controller.Move(diff);
    }
	private void Jump()
	{
		direction.y = jumpForce;
	}
	
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(hit.transform.tag == "Obstacle")
		{
			PlayerManager.gameOver = true;
			FindObjectOfType<AudioManager>().PlaySound("GameOver");
			
		}
	}
}
