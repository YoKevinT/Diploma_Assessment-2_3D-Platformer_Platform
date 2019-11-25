using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 16f;
    public float walkSpeed = 6f;
    public float gravity = -18f;
    public LayerMask groundLayer;
    //public float crouchSpeed = 2f;
    public float jumpHeight = 8f;
    public float groundRayDistance = 1.1f;


    private CharacterController controller;
    private Vector3 motion;
    private bool isJumping = false;
    private float currentSpeed;

    public int damage = 20;
    public Material playerMaterial;
    public Material hitedMaterial;

    void Start()
    { 
        // Scope
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        bool inputSprintPressed = Input.GetButtonDown("Sprint");
        bool inputSprintReleased = Input.GetButtonUp("Sprint");
        bool inputJump = Input.GetButtonDown("Jump");

        // Normalizing movement
        Vector3 normalized = new Vector3(inputH, 0f, inputV);
        normalized.Normalize();

        if (inputSprintPressed)
        {
            Sprint();
        }

        if (inputSprintReleased)
        {
            Walk();
        }

        Move(normalized.x, normalized.z);

        // If Jump button pressed (Space)
        if (IsGrounded() && inputJump)
        {
            // Make character jump
            Jump(jumpHeight);
        }

        // If Is Grounded AND is NOT jumping
        if (IsGrounded() && !isJumping)
        {
            motion.y = 0f;
        }

        motion.y += gravity * Time.deltaTime;

        // If NOT Grounded anymore AND is jumping
        if (!IsGrounded() && isJumping)
        {
            isJumping = false;
        }

        motion.y += gravity * Time.deltaTime;

        // Applies motion to CharacterController
        controller.Move(motion * Time.deltaTime);
    }

    // Test if the Player is Grounded
    private bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        // Performing Raycast
        if (Physics.Raycast(groundRay, groundRayDistance, groundLayer))
        {
            // Return true is hit
            return true; // - Exits the function
        }
        // Return false if not hit
        return false; // - Exits the function
    }

    // Move the Player Character in the direction we give it (horizontal / vertical)
    public void Move(float horizontal, float vertical)
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // Convert local direction to world space
        direction = transform.TransformDirection(direction);

        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }

    public void Sprint()
    {
        currentSpeed = runSpeed;
    }

    public void Walk()
    {
        currentSpeed = walkSpeed;
    }

    public void Jump(float height)
    {
        motion.y = jumpHeight;
        isJumping = true;
    }

    //public void Finish()
    //{
    //    Destroy(gameObject);
    //    gameObject.GetComponent<PlayerHealth>().gameOver.SetActive(true);
    //}

    public void OnCollisionEnter(Collision collision)
    {
        // Kill Enemy
        if (!IsGrounded() && collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        // Jump and then Kill Enemy
        if(!IsGrounded() && collision.gameObject.tag == "EnemyJump")
        {
            Jump(jumpHeight);
            Destroy(collision.gameObject, 0.5f);
        }

        // Take Damage from Enemy
        if (IsGrounded() && collision.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            gameObject.GetComponent<Player>().GetComponent<Renderer>().material = hitedMaterial;

            Invoke("changeColorBack", 0.25f);
        }

        // Take Damage from EnemyJump
        if (IsGrounded() && collision.gameObject.tag == "EnemyJump")
        {
            gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            gameObject.GetComponent<Player>().GetComponent<Renderer>().material = hitedMaterial;

            Invoke("changeColorBack", 0.25f);
        }
    }

    public void changeColorBack()
    {
        gameObject.GetComponent<Player>().GetComponent<Renderer>().material = playerMaterial;
    }
}