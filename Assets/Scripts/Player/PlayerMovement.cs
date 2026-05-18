using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbodyRef;
    [SerializeField] private Transform bodyRef;
    [SerializeField] private PlayerInputObserver inputObserver;
    [SerializeField] private float walkingSpeed;

    private float minMovementMagnitude = 0.0001f;
    private float colliderAdjustment = 0.05f;

    public bool IsMoving { get; private set; }

    private void FixedUpdate()
    {
        Move();
        UpdatePublicVariables();
    }

    private void Move()
    {
        Vector2 inputMovementDir = inputObserver.movement.normalized;

        Vector3 realMovementDir = bodyRef.right * inputMovementDir.x * Time.deltaTime;
        realMovementDir.y = 0f;

        if (realMovementDir.sqrMagnitude > minMovementMagnitude)
            realMovementDir = realMovementDir.normalized;

        Vector3 velocity = Vector3.zero;
        velocity.x = realMovementDir.x;

        rigidbodyRef.linearVelocity = velocity * walkingSpeed;
    }

    private void UpdatePublicVariables()
    {
        Vector3 horizontalVelocity = rigidbodyRef.linearVelocity;
        horizontalVelocity.y = 0f;
        float currentSpeed = horizontalVelocity.magnitude;

        IsMoving = currentSpeed > 0.01f;
    }
}
