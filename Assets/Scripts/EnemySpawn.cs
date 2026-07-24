using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabEnnemi;
    public float intervalleSpawn = 2f;

    [Header("Zone de spawn (rectangle sur la map)")]
    public float minX = -19f;
    public float maxX = 18f;
    public float minY = -18f;
    public float maxY = 17f;

    [Header("Securite")]
    public float distanceMinJoueur = 5f; // evite de spawn sur le joueur

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
        Vector3 spawnPos = PositionAleatoireDansLaZone();
        Instantiate(prefabEnnemi, spawnPos, Quaternion.identity);
    }

    Vector3 PositionAleatoireDansLaZone()
    {
        const int maxTentatives = 30;
        for (int i = 0; i < maxTentatives; i++)
        {
            Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            if (joueur == null || Vector3.Distance(pos, joueur.position) >= distanceMinJoueur)
                return pos;
        }
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 centre = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f);
        Vector3 taille = new Vector3(maxX - minX, maxY - minY, 0f);
        Gizmos.DrawWireCube(centre, taille);
    }
}
