using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float runSpeed = 5f;
    [SerializeField] public float jumpPower = 25f;
    [SerializeField] public float climbSpeed = 5f;
    
    public float dashDistance = 15f;
    public bool isDashingActive;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;
    private int extraJumps;

    [Header("Jump Settings")]
    public int extraJumpValue = 2;
    public bool isWallJumpActive;
    public Transform frontCheck;
    public float wallSlidingSpeed;

    [Header("Crouch Settings")]
    public bool isCrouchEnabled;

    [Header("Environment Settings")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform playerPOS;
    
    public ParticleSystem particleSystem;

    bool facingRight = false;
    bool wallSliding;
    private bool isGrounded;
    bool isTouchingFront;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float WallJumpTime;
    
    Rigidbody2D playerRB;
    Animator playerAnimator;
    BoxCollider2D playerCollider;



    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerPOS = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        particleSystem.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        playerRB.velocity = new Vector2(input * climbSpeed, playerRB.velocity.y);

        if (input < 0 && facingRight ==false)
        {
            Debug.Log(input + "right" + facingRight.ToString());
            FlipSprite();
        }
        else if (input > 0 && facingRight == true)
        {
            Debug.Log(input + "left"+ facingRight.ToString());
            FlipSprite();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            playerRB.velocity = Vector2.up * jumpPower;
        }
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront == true && isGrounded == false && input != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding) {
            playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding==true) {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", WallJumpTime);
        }
        if (wallJumping == true) {
            playerRB.velocity = new Vector2(xWallForce * -input,yWallForce);
        }
    }

    void SetWallJumpingToFalse() {
        wallJumping = false;
    }
    private void Dash() {
        if (isDashingActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (doubleTapTime>Time.time && lastKeyCode ==KeyCode.A) {
                    StartCoroutine(DashMove(-1f, 0.4f));
                    particleSystem.Play();
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeyCode = KeyCode.A;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
                {
                    StartCoroutine(DashMove(1f,0.4f));
                    particleSystem.Play();
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeyCode = KeyCode.D;
            }

        }
        else {
            return;
        }
    }
    
    
    private void ClimbLadder() {
        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("ladder"))) {
            //Change animation to false 
            playerRB.gravityScale = 1f;
            return;
        }
        //climb ladder animation.
        float controlThrow = Input.GetAxisRaw("Vertical");
        Vector2 climbVelocity = new Vector2(playerRB.velocity.x, controlThrow * climbSpeed);
        playerRB.velocity = climbVelocity;
        playerRB.gravityScale = 0f;
    }
    void Crouch() {
        if (Input.GetButtonDown("Fire1") && isCrouchEnabled) {
            //crouch animation
            playerCollider.size = new Vector2(playerCollider.size.x,0.65f);
            playerCollider.offset = new Vector2(0, -0.2f);
        }
        if (Input.GetButtonUp("Fire1") && isCrouchEnabled) {
           
            playerCollider.size = new Vector2(playerCollider.size.x, 0.85f);
            playerCollider.offset = new Vector2(0, 0f);
        }
        
    }

    private void FlipSprite()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    IEnumerator DashMove(float direction,float dashDurration) {
        //Dash Animation.
        isDashing = true;
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        playerRB.AddForce(new Vector2(dashDistance * direction,0f),ForceMode2D.Impulse);
        playerRB.gravityScale= 0f;
        yield return new WaitForSeconds(dashDurration);
        isDashing = false;
        playerRB.gravityScale = 1f;

    }
}
