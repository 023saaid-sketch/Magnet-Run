using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    bool alive = true;

    public float speed = 5;
    public bool canMove = true;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;
    [SerializeField] float leftLimit = -3.5f;
    [SerializeField] float rightLimit = 3.5f;
    private Vector2 lastTouchPosition;
    private bool isDragging = false;

    [SerializeField] float touchSensitivity = 0.02f;

    public float speedIncreasePerPoint = 0.01f;

    private void FixedUpdate()
    {
        if (!alive) return;

        if (!canMove) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;

        Vector3 newPosition = rb.position + forwardMove + horizontalMove;

        // محدود کردن حرکت در محور X
        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);

        rb.MovePosition(newPosition);
    }
    private void Update()
    {
        if (transform.position.y < -5)
        {
            Die();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastTouchPosition = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:

                    if (isDragging)
                    {
                        float deltaX = touch.position.x - lastTouchPosition.x;

                        horizontalInput = deltaX * touchSensitivity;

                        lastTouchPosition = touch.position;
                    }

                    break;

                case TouchPhase.Ended:

                    horizontalInput = 0;
                    isDragging = false;

                    break;
            }
        }
    }

    public void Die()
    {
        alive = false;
        canMove = false;
    }

    
}