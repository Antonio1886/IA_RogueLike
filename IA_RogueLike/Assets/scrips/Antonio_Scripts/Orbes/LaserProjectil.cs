using UnityEngine;

public class LaserProjectil : MonoBehaviour
{
  public float speed = 5f;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
