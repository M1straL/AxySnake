using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour, IMoveable
{
    private CharacterController _characterController;
    private readonly float _playerSpeed = 8.0f;
    private readonly float _rotationSpeed = 50.0f;
    private Camera _playerCamera;
    
    private void Start()
    {
        _characterController = gameObject.AddComponent<CharacterController>();
        _playerCamera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    public void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(0,0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * (_playerSpeed * Time.deltaTime), Space.Self);
    }

    private void Rotate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 rotateTo = new Vector3(0, horizontalInput, 0) * (_rotationSpeed * Time.deltaTime);
        transform.Rotate(rotateTo);
    }
}
