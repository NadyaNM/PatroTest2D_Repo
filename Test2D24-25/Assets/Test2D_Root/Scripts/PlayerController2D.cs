using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    //Referencias privadas generales
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] PlayerInput playerInput;
    Vector2 moveInput; //Variable para referenciar el input de los controladores


    [Header("Movement Parameters")]
    public float speed;
    [SerializeField] bool isFacingRight;

    [Header("Jump Parameters")]
    public float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] GameObject groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        //Para autoreferenciar: nombre de variable = GetComponenet <tipo de variable>()
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();
    }
    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        playerRb.velocity = new Vector3(moveInput.x * speed, playerRb.velocity.y, 0);
    }

    void Flip()
    {
        Vector3 currentEscale = transform.localScale;
        currentEscale.x *= -1;
        transform.localScale = currentEscale;
        isFacingRight =!isFacingRight;
    }

    void GroundCheck()
    {
        //isGrounded es verdaero cuando el circulo detector toquela layer ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }


    #region Input Methods

    //Métodos que permiten leer el input del New Input System
    //Crearemos un método por cada acción 

    public void HandleMovement(InputAction.CallbackContext context)
    {
        //Las acciones de tipo VALUE deben almacenarse = ReadValue
        moveInput = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (isGrounded) 
            {
             playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
            
        }
 
    }

    #endregion






}
