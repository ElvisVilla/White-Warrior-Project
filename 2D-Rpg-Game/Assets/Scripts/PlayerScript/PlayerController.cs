using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Properties
    public float Speed { get { return speed; } set { speed = Mathf.Clamp(value, 1f, 10f); } }
    #endregion

    #region Variables
    [Header("Stats de Movilidad")]
    [SerializeField] InputType inputMode;
    [SerializeField] Joystick joystick;
    [SerializeField] float speed;                                  
    [SerializeField] int extraJumps;
    [SerializeField] float jumpForce;
    [SerializeField] float extraJumpForce;
    private bool facingRight = true;

    [Header("Checar collision")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private const float radius = 0.02f;
    private bool grounded = false;

    //Components
    Animator anim;
    Rigidbody2D body2D;
    #endregion

    void Awake ()
    {
        anim = GetComponentInChildren<Animator>();
        body2D = GetComponent<Rigidbody2D>();
	}

    void Update ()
    {
        Jump();
    }

    void FixedUpdate()
    {
        //Comprueba la mascara de colisiones "mask".
        grounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
        float deltaX = 0f;

        switch (inputMode)
        {
            case InputType.Teclado:
                deltaX = Input.GetAxisRaw("Horizontal") * speed;
                Movement(deltaX);
                break;

            case InputType.Joystick:
                deltaX = joystick.Horizontal * speed;
                Movement(deltaX);
                break;
        }
    }

    void Movement(float moveX)
    {
        //Controlador de movimiento de personaje.
        body2D.velocity = new Vector2(moveX, Mathf.Clamp(body2D.velocity.y, -9f, 9f));

        //Girando el sprite.
        if (moveX > 0 && !facingRight)
            Flip();
        if (moveX < 0 && facingRight)
            Flip();

        //Refresca la animacion SetBool(Falling);
        bool falling = !grounded;

        //animaciones
        anim.SetFloat("Speed", moveX);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Falling", falling);
    }

    //Common Flip sprite method
    void Flip ()
    {
        facingRight = !facingRight;
        Vector2 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    void Jump ()
    {
        //Cheking for firt jump.
        if (Input.GetKeyDown(KeyCode.Space) &&  grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * jumpForce;
            extraJumps = 1;
        }
        
        else if (Input.GetKeyDown(KeyCode.Space) && (extraJumps > 0) && !grounded)
        {
            anim.SetTrigger("Salto");
            body2D.velocity = Vector2.up * extraJumpForce;
            extraJumps--;
        }
    }

    public void TestJump()
    {
        anim.SetTrigger("Salto");
        body2D.velocity = Vector2.up * jumpForce;
    }
}

