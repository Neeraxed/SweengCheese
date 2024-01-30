using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float laneDistance = 3;
    public float forwardSpeed;
    public float maxSpeed;
    public Animator animator;

    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1; // 0 - left, 1 - middle, 2 - right

    public static event Action gameEnded;
    public static event Action CollectedAmountChanged;
    public float pointToCollectActivationRadius = 5f;
    private Collider[] pointToCollectCollidersBuffer = new Collider[1];

    public void DetectPointToCollect()
    {
        var hits = Physics.OverlapSphereNonAlloc(this.transform.position, pointToCollectActivationRadius, pointToCollectCollidersBuffer, 1 << PointToCollect.PointToCollectLayer);

        if (hits > 0)
        {
            var pointToCollect = pointToCollectCollidersBuffer[0].GetComponent<PointToCollect>();
            pointToCollect.Collect(ref PlayerManager.numberOfCheese);
            CollectedAmountChanged?.Invoke();
        }
    }
    private void OnEnable()
    { 
        PointToCollect.PointToCollectTriggered += DetectPointToCollect;
    }
    private void OnDisable()
    { 
        PointToCollect.PointToCollectTriggered -= DetectPointToCollect;
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted) return;
    

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.02f * Time.deltaTime;
        }

        animator.SetBool("isGameStarted", true);
        controller.Move(direction * Time.fixedDeltaTime);
        direction.z = forwardSpeed;


        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else controller.Move(diff);
    }

    private void Update()
    {
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            gameEnded?.Invoke();
            Debug.Log("gameEnded.Invoke()");
            hit.collider.enabled = false;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }
    
}