using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Vector2 moveDistance = new Vector2(1, 1); 

    private bool isMoving = false; 
    private Vector3 targetPosition; 
    void Start()
    {
        targetPosition = transform.position; 
    }

    void Update()
    {
        if (isMoving)
            return;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
        {
            Vector3 moveDir = new Vector3(input.x * moveDistance.x, input.y * moveDistance.y, 0);
            targetPosition = transform.position + moveDir;

            // Start the movement coroutine
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 destination)
    {
        isMoving = true;

        while ((destination - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Snap to the exact position to avoid floating-point issues
        transform.position = destination;
        isMoving = false;
    }
}
