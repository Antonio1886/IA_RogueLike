using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Vector2 Direcction;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Direcction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        bool isMoving = Direcction.sqrMagnitude > 0;
        anim.SetBool("isMoving", isMoving);

        // Obtener posición del mouse en el mundo
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Girar sprite hacia la izquierda o derecha según la posición del mouse
        if (mouseWorldPos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (mouseWorldPos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Direcction * MoveSpeed * Time.fixedDeltaTime);
    }
}
