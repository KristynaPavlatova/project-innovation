using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Attach to Main Camera located on player object
    /// </summary>
    
    //general:
    public bool canLookAround = true;
    public bool canWalk = true;
    public bool gravityApplied = true;
    public bool canJump = true;

    public float mouseSensitivity = 100f;
    public float movementSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float groundDistance = 0.4f;//radius of ground check sphere
    public Transform playerBody;
    public Transform groundCheck;
    public LayerMask groundMask;    

    //looking around:
    private float _xRotation = 0f;
    private float _mouseX;
    private float _mouseY;
    //walking:
    private float _x;
    private float _z;
    private Vector3 _move;
    private CharacterController controller;
    //gravity / ground check:
    private Vector3 _velocity;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//coursor won't move out of the screen/window
        controller = playerBody.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canLookAround)
        {
            _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            playerBody.Rotate(Vector3.up * _mouseX);

            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);//player cannot look behind
            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);//setting X rotation of the object this script is attached to to xRotation
        }

        if (canWalk)
        {
            _x = Input.GetAxis("Horizontal");
            _z = Input.GetAxis("Vertical");
            //creating direction to move to
            _move = transform.right * _x + transform.forward * _z;

            controller.Move(_move * movementSpeed * Time.deltaTime);
        }

        if (gravityApplied)
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//creating groundCheck sphere

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;//restarting velocity so, falling is always the same speed each time
            }

            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);//gravity physics rule
        }

        if (canJump)
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);//physics equasion
            }
        }
    }
}
