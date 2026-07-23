using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabEnnemi;
    public float intervalleSpawn = 2f;
    public float distanceSpawn = 10f; // distance min du joueur pour spawn hors écran

    private Transform joueur;
    private float timer;

    void Start()
    {
        joueur = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalleSpawn)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        // Choisit une direction aléatoire autour du joueur
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = joueur.position + (Vector3)(direction * distanceSpawn);

        Instantiate(prefabEnnemi, spawnPos, Quaternion.identity);
    }
}