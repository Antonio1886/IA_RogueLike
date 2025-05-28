using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Vector2 Direcction;
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;
    private bool canMove = true;

    private Rigidbody2D rb;
    public Animator anim;
    private Camera mainCamera;
    private Health healthScript;
    public GameObject arma;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        healthScript = GetComponent<Health>();
        arma.SetActive(true);
    }

    private void Update()
    {
        if (healthScript != null && healthScript.GetCurrentHealth() <= 0 && canMove)
        {
            StartDeath();
            return;
        }

        if (!isDashing && canMove)
        {
            Direcction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            bool isMoving = Direcction.sqrMagnitude > 0;
            anim.SetBool("isMoving", isMoving);

            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (mouseWorldPos.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (mouseWorldPos.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                StartCoroutine(Dash());
                anim.SetTrigger("Dash");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing && canMove)
        {
            rb.MovePosition(rb.position + Direcction * MoveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        if (Direcction.sqrMagnitude > 0)
        {
            dashDirection = Direcction;
        }
        else
        {
            Vector2 mouseScreenPos = Input.mousePosition;
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
            dashDirection = (mouseWorldPos - rb.position).normalized;
        }

        // Aplicar dash
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;

        // Cooldown del dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void StartDeath()
    {
        canMove = false;
        arma.SetActive(false);
        anim.SetTrigger("Die");
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(2f);

        Muerto();
    }

    private void Muerto()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MenuSeleccionPersonaje");
    }

    public void SetCanMove(bool moveState)
    {
        canMove = moveState;
    }
    // Agrega este método en la clase TopDownMovement
    public bool IsDashing()
    {
        return isDashing;
    }
}