using UnityEngine;

public class RotateGunToMouse : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject bulletPrefab;
    public float bulletSpeed = 90f;
    public Transform firePoint; // punto de salida de la bala

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Disparo con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(direction);
        }
    }

    private void Shoot(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }

}
