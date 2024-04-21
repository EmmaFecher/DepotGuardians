using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{
    [SerializeField] PlayerInputHandler input;

    [Header("Movement")]
    Rigidbody rb = null;
    Vector3 playerMoveInput = Vector3.zero;
    [SerializeField] float movementMultiplier = 30.0f;
    [SerializeField] float slopeMovementMultiplier = 50.0f;
    [SerializeField] float runMultiplyer = 2.5f;

    [Header("Camera & Turning")]
    public Transform cameraFollow;
    Vector3 playerLookInput = Vector3.zero;
    Vector3 prevPlayerLookInput = Vector3.zero;
    [SerializeField] float cameraPitch = 0.0f;
    [SerializeField] float playerLookInputLerpTime = 0.35f;
    [SerializeField] float rotationSpeedModifier = 180.0f;
    [SerializeField] float pitchSpeedModifier = 180.0f;

    [Header("Ground Check")]
    CapsuleCollider capsuleCollider = null;
    bool isPlayerGrounded = true;
    [SerializeField][Range(0.0f, 1.8f)] float groundCheckRadiusMultiplier = 0.9f;
    [SerializeField][Range(-0.95f, 1.05f)] float groundCheckDistance = 0.05f;
    RaycastHit groundCheckHit = new RaycastHit();

    [Header("Gravity")]
    [SerializeField] float gravFallCurrent = -100.0f;
    [SerializeField] float gravFallMin = -100.0f;
    [SerializeField] float gravFallMax = -500.0f;
    [SerializeField][Range(-5.0f, -35.0f)] float gravFallIncrementAmount = -20.0f;
    [SerializeField] float gravFallIncrementTime = 0.05f;
    [SerializeField] float playerFallTimer = 0.0f;
    [SerializeField] float gravityGrounded = -1.0f;
    [SerializeField] float maxSlopeAngle = 47f;

    [Header("Jumping")]
    [SerializeField] float initialJumpForce = 750.0f;
    [SerializeField] float continualJumpForceMultiplier = 0.1f;
    [SerializeField] float jumpTime = 0.175f;
    [SerializeField] float jumpTimeCounter = 0.0f;
    [SerializeField] float coyoteTime = 0.15f;
    [SerializeField] float coyoteTimeCounter = 0.0f;
    [SerializeField] float jumpBufferTime = 0.2f;
    [SerializeField] float jumpBufferTimeCounter = 0.0f;
    [SerializeField] bool  playerIsJumping = false;
    [SerializeField] bool  jumpWasPressedLastFrame = false;

    [Header("Menu stuff")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject hudMenu;

    private void Awake()
    {
        //lock the curser
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //get stuff
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        CheckForEscape();
    }
    private void FixedUpdate()
    {

        playerLookInput = GetLookInput();
        PlayerLook();
        PitchCamera();
        
        playerMoveInput = GetMoveInput();//get input
        isPlayerGrounded = PlayerGroundCheck();//check for ground
        playerMoveInput = PlayerMove();//get the movement specifically
        playerMoveInput = PlayerSlope();//get if it is on/touching a slope
        playerMoveInput = PlayerRun();//change movement.speed if running
        playerMoveInput.y = PlayerFallGravity();//apply grav, using ifgrounded
        playerMoveInput.y = PlayerJump();//get movement up, if jumping
        playerMoveInput *= rb.mass;
        rb.AddRelativeForce(playerMoveInput, ForceMode.Force);//apply the movement gotten above
    }
    //rotation
    private Vector3 GetLookInput()
    {
        prevPlayerLookInput = playerLookInput;
        playerLookInput = new Vector3(input.LookInput.x, -input.LookInput.y, 0.0f);
        return Vector3.Lerp(prevPlayerLookInput, playerLookInput * Time.deltaTime, playerLookInputLerpTime);
    }
    private void PlayerLook()
    {
        rb.rotation = Quaternion.Euler(0.0f, rb.rotation.eulerAngles.y + (playerLookInput.x * rotationSpeedModifier), 0.0f);
    }
    private void PitchCamera()
    {
        Vector3 rotaionValues = cameraFollow.rotation.eulerAngles;
        cameraPitch += playerLookInput.y * pitchSpeedModifier;
        cameraPitch = Mathf.Clamp(cameraPitch, -40f, 40f);
        cameraFollow.rotation = Quaternion.Euler(cameraPitch, rotaionValues.y, rotaionValues.z);
    }
    //moving

    private Vector3 GetMoveInput()
    {
        return new Vector3(input.MoveInput.x, 0.0f, input.MoveInput.y);
    }

    private Vector3 PlayerRun()
    {
        Vector3 calculatedPlayerRunSpeed = playerMoveInput;
        if(input.SprintValue != 0f && input.MoveInput != Vector2.zero)
        {
            calculatedPlayerRunSpeed *= runMultiplyer;
        }
        return calculatedPlayerRunSpeed;
    }
    private Vector3 PlayerMove()
    {
        return new Vector3(playerMoveInput.x * movementMultiplier,
            playerMoveInput.y,
            playerMoveInput.z * movementMultiplier);
    }
    //grav and ground check
    private bool PlayerGroundCheck()
    {
        float sphereCastRadius = capsuleCollider.radius * groundCheckRadiusMultiplier;//radius of player - a little
        float sphereCastTravelDist = capsuleCollider.bounds.extents.y - sphereCastRadius + groundCheckDistance;//how far to the ground - the sphere/radius
        return Physics.SphereCast(rb.position, sphereCastRadius, Vector3.down, out groundCheckHit, sphereCastTravelDist);//cast the sphere, return if it hits ground
    }
    private float PlayerFallGravity()
    {
        float gravity = playerMoveInput.y;
        if(isPlayerGrounded)//grounded == grav not needed
        {
            gravFallCurrent = gravFallMin;
        }
        else
        {
            playerFallTimer -= Time.fixedDeltaTime;
            if(playerFallTimer < 0.0f)
            {
                if(gravFallCurrent > gravFallMax)
                {
                    gravFallCurrent += gravFallIncrementAmount;
                }
                playerFallTimer = gravFallIncrementTime;
                
            }
            gravity = gravFallCurrent;
        }
        return gravity;
    }
    private Vector3 PlayerSlope()
    {
        Vector3 calculatedPlayerMovement = playerMoveInput;
        if (isPlayerGrounded)
        {
            Vector3 localGroundCheckHitNormal = rb.transform.InverseTransformDirection(groundCheckHit.normal);
            float groundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, rb.transform.up);
            if(groundSlopeAngle == 0.0f)
            {
                if (input.MoveInput != Vector2.zero)
                {
                    RaycastHit rayHit;
                    float rayHeightFromGround = 0.1f;
                    float rayCalculatedRayHeight = rb.position.y - capsuleCollider.bounds.extents.y + rayHeightFromGround;
                    Vector3 rayOrigin = new Vector3(rb.position.x, rayCalculatedRayHeight, rb.position.z);
                    if(Physics.Raycast(rayOrigin, rb.transform.TransformDirection(calculatedPlayerMovement), out rayHit, 0.75f))
                    {
                        if (Vector3.Angle(rayHit.normal, rb.transform.up) > maxSlopeAngle)
                        {
                            calculatedPlayerMovement.y = -slopeMovementMultiplier;
                        }
                    }
                }
                if(calculatedPlayerMovement.y == 0.0f)
                {
                    calculatedPlayerMovement.y = gravityGrounded;
                }
            }
            else
            {
                Quaternion slopeAngleRot = Quaternion.FromToRotation(rb.transform.up, localGroundCheckHitNormal);
                calculatedPlayerMovement = slopeAngleRot * calculatedPlayerMovement;
                float relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, rb.transform.up) - 90.0f;
                calculatedPlayerMovement += calculatedPlayerMovement * (relativeSlopeAngle / 90.0f);
                if (groundSlopeAngle < maxSlopeAngle)
                {
                    if (input.MoveInput != Vector2.zero)
                    {
                        calculatedPlayerMovement.y += gravityGrounded;
                    }
                }
                else
                {
                    float calculatedSlopeGrav = groundSlopeAngle * -0.2f;
                    if(calculatedSlopeGrav < calculatedPlayerMovement.y)
                    {
                        calculatedPlayerMovement.y = calculatedSlopeGrav;
                    }
                }
            }
        }
        return calculatedPlayerMovement;
    }
    //Jump stuff
    private float PlayerJump()
    {
        float calculatedJumpinput = playerMoveInput.y;
        SetJumpTimeCounter();
        SetCoyoteTimeCounter();
        SetJumpBufferTimeCounter();

        if(jumpBufferTimeCounter > 0.0f && !playerIsJumping && coyoteTimeCounter > 0.0f)
        {
            calculatedJumpinput = initialJumpForce;
            playerIsJumping = true;
            jumpBufferTimeCounter = 0.0f;
            coyoteTimeCounter = 0.0f;
        }
        else if (input.JumpTriggered && playerIsJumping && !isPlayerGrounded && jumpTimeCounter > 0.0f)
        {
            calculatedJumpinput = initialJumpForce * continualJumpForceMultiplier;
        }
        else if (playerIsJumping && isPlayerGrounded)
        {
            playerIsJumping = false;
        }
        return calculatedJumpinput;
    }
    private void SetJumpTimeCounter()
    {
        if(playerIsJumping && !isPlayerGrounded)
        {jumpTimeCounter -= Time.fixedDeltaTime;}
        else
        {jumpTimeCounter = jumpTime;}
    }
    private void SetCoyoteTimeCounter()
    {
        if (isPlayerGrounded)
        {coyoteTimeCounter = coyoteTime;}
        else
        {coyoteTimeCounter -= Time.fixedDeltaTime;}
    }
    private void SetJumpBufferTimeCounter()
    {
        if(!jumpWasPressedLastFrame && input.JumpTriggered)
        {jumpBufferTimeCounter = jumpBufferTime;}
        else if (jumpBufferTimeCounter > 0.0f)
        {jumpBufferTimeCounter -= Time.fixedDeltaTime;}
        jumpWasPressedLastFrame = input.JumpTriggered;
    }

    private void CheckForEscape()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            input.Pause();
            hudMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

}