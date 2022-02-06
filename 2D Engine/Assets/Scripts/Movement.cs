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
    float control;
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
        if (!isDashing) {
            control = Run();
        }
        Jump();
        HoldToWall(control);
        Crouch();
        Dash();
        ClimbLadder();
    }

    private float Run()
    {
        //Run animation.
        playerAnimator.SetBool("running", true);
        float control = Input.GetAxisRaw("Horizontal");
        float x = control * runSpeed;

        Vector2 playerVelocity = new Vector2(x, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;
        if (control == 0) {
            playerAnimator.SetBool("running", false);
        }
        if (control > 0 && facingRight)
        {
            FlipSprite();
        }
        else if(control<0 && !facingRight){
            FlipSprite();
        }
        

        return x;
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
    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            extraJumps = extraJumpValue;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            //jump animation.
            particleSystem.Play();
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpPower);
            playerRB.velocity += jumpVelocityToAdd;
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            //jump animation.
            particleSystem.Play();
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpPower);
            playerRB.velocity += jumpVelocityToAdd;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            extraJumps = extraJumpValue;
        }
    }
    [System.Obsolete]
    private void HoldToWall(float control)
    {
        bool played =false;
        if (isWallJumpActive)
        {
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
            playerCollider.sharedMaterial.friction = 0.4f;
            playerRB.sharedMaterial.friction = 0.4f;

            if (isTouchingFront == true && isGrounded == false && control != 0)
            {
                if (!played)
                {
                    particleSystem.Play();
                    print("holding to wall ");
                    played = true;
                }
               
               wallSliding = true;
                
            }
            else
            {
                wallSliding = false;
            }

            if (wallSliding)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }
            if (Input.GetButtonDown("Jump") && wallSliding == true)
            {
                wallJumping = true;
                Invoke("SetWallJumpingToFalse", WallJumpTime);
            }
            if (wallJumping)
            {
                playerRB.velocity = new Vector2(xWallForce * -control, yWallForce);
            }

        }
        else {
            playerCollider.sharedMaterial.friction = 0f;
            playerRB.sharedMaterial.friction = 0f;
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

    private void SetWallJumpingToFalse() {
        wallJumping = false;
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
