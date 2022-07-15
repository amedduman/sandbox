using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
    Player player;
    Player p2;

    void Awake()
    {
        player = DependencyProvider.Instance.Get<Player>();

        Destroy(player.gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(player == null) Debug.Log($"null");
            Destroy(player.gameObject);
        }

        if(Input.GetKeyDown(KeyCode.V)) 
        {
            Debug.Log($"log"); 
            Player p = DependencyProvider.Instance.Get<Player>();
        }
    }
}
