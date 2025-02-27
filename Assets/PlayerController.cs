using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private CinemachineCamera freeLookCamera;
    private Rigidbody rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager.OnMove.AddListener(movePlayer);
        inputManager.OnJump.AddListener(jumpPlayer);
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
        if (isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
