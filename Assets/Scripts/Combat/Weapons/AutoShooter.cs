using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [Header("Références")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    private float fireTimer;
    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / player.fireRate)
        {
            ShootClosestEnemy();
            fireTimer = 0f;
        }
    }

    void ShootClosestEnemy()
    {
        GameObject enemy = FindClosestEnemy();

        if (enemy == null)
            return;

         Vector2 origin = firePoint != null
            ? firePoint.position
            : transform.position;

        Vector2 direction =
            ((Vector2)enemy.transform.position - origin).normalized;


        GameObject projectileObj =
            Instantiate(projectilePrefab, origin, Quaternion.identity);

        Projectile projectile =
            projectileObj.GetComponent<Projectile>();

        if(projectile != null)
        {
            projectile.SetStats(
                player.projectileSpeed,
                player.damage
            );

            projectile.SetDirection(direction);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");


        GameObject closest = null;
        float closestDistance = player.attackRange /4;


        foreach(GameObject enemy in enemies)
        {
            float distance =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position);


            if(distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }


        return closest;
    }
}