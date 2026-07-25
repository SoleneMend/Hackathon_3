using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float vitesse = 2f;
    public int degats = 10;

    [Header("Attaque")]
    public float distanceAttaque = 1f;      // Distance a laquelle l'ennemi s'arrete pour attaquer
    public float cadenceAttaque = 1.5f;     // Temps entre deux coups (en secondes)

    [Header("Evitement d'obstacles")]
    public string tagObstacle = "Obstacle";    // Doit correspondre au Tag mis sur vos obstacles
    public float distanceDetection = 1.5f;     // Portee du "capteur" devant l'ennemi
    public float pasAngle = 15f;               // Increment d'angle teste a chaque essai
    public float angleMax = 90f;               // Angle max testé de chaque cote (gauche/droite)

    private Transform joueur;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerHealth vieJoueur;
    private float timerAttaque;

    void Start()
    {
        // Trouve automatiquement le joueur via son tag
        GameObject objJoueur = GameObject.FindGameObjectWithTag("Player");
        joueur = objJoueur.transform;
        vieJoueur = objJoueur.GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (joueur == null) return;

        float distanceJoueur = Vector2.Distance(transform.position, joueur.position);

        if (distanceJoueur <= distanceAttaque)
        {
            // Assez proche du joueur : on s'arrete et on attaque
            rb.linearVelocity = Vector2.zero;
            GererAttaque();
        }
        else
        {
            // Trop loin : on se rapproche normalement (avec evitement d'obstacles)
            Vector2 directionSouhaitee = (joueur.position - transform.position).normalized;
            Vector2 directionFinale = TrouverDirectionLibre(directionSouhaitee);
            rb.linearVelocity = directionFinale * vitesse;
            timerAttaque = 0f; // reset le cooldown si on s'eloigne avant d'attaquer
        }

        // Met a jour l'Animator pour declencher les transitions Idle <-> Run
        if (animator != null)
            animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }

    void GererAttaque()
    {
        timerAttaque -= Time.fixedDeltaTime;
        if (timerAttaque <= 0f)
        {
            timerAttaque = cadenceAttaque;

            if (animator != null)
                animator.SetTrigger("Attaque"); // A relier a un state d'attaque quand il sera cree

            if (vieJoueur != null)
                vieJoueur.SubirDegats(degats);
        }
    }

    // Cherche une direction libre d'obstacle, en partant de la direction souhaitee
    // puis en testant des angles croissants a gauche et a droite si bloque.
    Vector2 TrouverDirectionLibre(Vector2 directionSouhaitee)
    {
        // 1. Chemin direct libre ? On y va.
        if (!ObstacleDevant(directionSouhaitee))
            return directionSouhaitee;

        // 2. Sinon on teste des angles croissants alternativement a droite et a gauche
        for (float angle = pasAngle; angle <= angleMax; angle += pasAngle)
        {
            Vector2 dirDroite = Rotation(directionSouhaitee, -angle);
            if (!ObstacleDevant(dirDroite))
                return dirDroite;

            Vector2 dirGauche = Rotation(directionSouhaitee, angle);
            if (!ObstacleDevant(dirGauche))
                return dirGauche;
        }

        // 3. Rien de libre trouve (coince) : on reste sur place plutot que de foncer dans le mur
        return Vector2.zero;
    }

    bool ObstacleDevant(Vector2 direction)
    {
        // On recupere TOUS les hits sur la distance, puis on ne garde que ceux tagges "Obstacle"
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distanceDetection);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag(tagObstacle))
                return true;
        }
        return false;
    }

    Vector2 Rotation(Vector2 vecteur, float angleDegres)
    {
        float rad = angleDegres * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(
            vecteur.x * cos - vecteur.y * sin,
            vecteur.x * sin + vecteur.y * cos
        );
    }

    // Note : les degats ne sont plus geres par collision physique,
    // mais par le cycle d'attaque (voir GererAttaque ci-dessus)

    // Visualise les rayons de detection et la zone d'attaque dans la Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceAttaque);

        if (joueur == null) return;
        Gizmos.color = Color.yellow;
        Vector2 dir = (joueur.position - transform.position).normalized;
        Gizmos.DrawRay(transform.position, dir * distanceDetection);
    }
}