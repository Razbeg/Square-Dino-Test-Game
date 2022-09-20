using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController instance = null;
    public static EnemyController self
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<EnemyController>();

            return instance;
        }
    }


    [Header("Game Settings")]
    [SerializeField] private List<EnemyPlatform> enemyPlatforms = new List<EnemyPlatform>();


    private void Start()
    {
        foreach (var enemyPlatform in self.enemyPlatforms)
            enemyPlatform.enemies.ForEach(enemy => enemy.GetComponent<Collider>().enabled = false);
    }


    public static void EnableColliderForEnemies(int id)
    {
        foreach (var enemyPlatform in self.enemyPlatforms)
        {
            if (enemyPlatform.platformId == id)
            {
                enemyPlatform.enemies.ForEach(enemy => enemy.GetComponent<Collider>().enabled = true);

                return;
            }
        }
    }

    public static bool IsEnemyOnPlatformAlive(int id)
    {
        foreach (var enemyPlatform in self.enemyPlatforms.Where(enemyPlatform => enemyPlatform.platformId == id))
            return enemyPlatform.enemies.Where(enemy => enemy.Health > 0f).Any();

        return false;
    }
}

[System.Serializable]
public class EnemyPlatform
{
    public int platformId = -1;
    public List<PawnHealth> enemies = new List<PawnHealth>();
}
