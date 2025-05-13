using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Rigidbody2D rb;
    public Animator anim;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Solo procesar movimiento y dash si no estamos en medio de un dash
        if (!isDashing)
        {
            Direcction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            bool isMoving = Direcction.sqrMagnitude > 0;
            anim.SetBool("isMoving", isMoving);

            // Rotación basada en posición del mouse
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (mouseWorldPos.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (mouseWorldPos.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            // Lógica del dash
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                StartCoroutine(Dash());
                anim.SetTrigger("Dash");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + Direcction * MoveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // Determinar dirección del dash
        if (Direcction.sqrMagnitude > 0)
        {
            dashDirection = Direcction;
        }
        else
        {
            // Dash hacia la posición del mouse si no hay movimiento
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
}