using UnityEngine;

public class Player : MonoBehaviour, IMoveable
{
    private CharacterController controller;
    private readonly float _playerSpeed = 8.0f;
    private readonly float _rotationSpeed = 50.0f;
    private Camera _playerCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.AddComponent<CharacterController>();
        _playerCamera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput,0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * (_playerSpeed * Time.deltaTime), Space.World);
    }

    private void Rotate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        var eulerAngles = transform.rotation.eulerAngles;

        Vector3 rotateTo = new Vector3(0, horizontalInput, 0) * (_rotationSpeed * Time.deltaTime);
       // Vector3 rotateFrom = transform.rotation
        
        //transform.Rotate(eulerAngles.x, eulerAngles.y);
        transform.Rotate(rotateTo);
    }
}
