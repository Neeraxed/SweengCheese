using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float Gravity = -5;
    public float laneDistance = 3;
    public float forwardSpeed;
    public float maxSpeed;
    public Animator animator;

    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1; // 0 - left, 1 - middle, 2 - right

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted) return;

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
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

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.transform.CompareTag("Obstacle"))
    //     {
    //         PlayerManager.gameOver = true;
    //         FindObjectOfType<AudioManager>().PlaySound("GameOver");
    //     }
    // }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            hit.collider.enabled = false;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }
}