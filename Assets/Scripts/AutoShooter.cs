using UnityEngine;

public class TestShooter : MonoBehaviour
{
    [Header("Références")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    void Update()
    {
        // Clic gauche de la souris = tir de test
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 origin = firePoint != null ? firePoint.position : transform.position;
            Vector2 direction = (mouseWorldPos - origin).normalized;

            GameObject projectileObj = Instantiate(projectilePrefab, origin, Quaternion.identity);
            Projectile projectile = projectileObj.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.SetDirection(direction);
            }
        }
    }
}