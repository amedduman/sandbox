using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCount : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = DependencyProvider.Instance.Get<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(player == null) Debug.Log($"null");
            Destroy(player.gameObject);
        }
    }
}
