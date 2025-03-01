using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCount = 0;
    [SerializeField] private float dashSpeed;
    [SerializeField] private CinemachineCamera freeLookCamera;
    private Rigidbody rb;
    private bool canDash = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager.OnMove.AddListener(movePlayer);
        inputManager.OnJump.AddListener(jumpPlayer);
        inputManager.OnDash.AddListener(dashPlayer);
    }

    void movePlayer(Vector2 direction)
    {
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0;
        Vector3 cameraRight = freeLookCamera.transform.right;
        cameraRight.y = 0;

        Vector3 moveDirection = cameraForward * direction.y + cameraRight * direction.x;
        rb.AddForce(speed * moveDirection);
    }

    void jumpPlayer()
    {
        if (jumpCount < 2) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount += 1;
            canDash = true;
        }
    }

    void dashPlayer()
    {
        if (canDash) {
            Vector3 dashDirection = freeLookCamera.transform.forward;
            rb.linearVelocity = new Vector3(dashDirection.x * dashSpeed, rb.linearVelocity.y, dashDirection.z * dashSpeed);
            canDash = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            jumpCount = 0;
            canDash = false;
        }
    }
}
