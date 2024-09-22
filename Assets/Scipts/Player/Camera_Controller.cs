using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Entity
{
    //Gets Access to the player
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z
        );

        if (player.transform.rotation.y >= 0)
        {
            playerPosition = new Vector3(
                playerPosition.x + offset,
                playerPosition.y,
                playerPosition.z
            );
        }
        else
        {
            playerPosition = new Vector3(
                playerPosition.x - offset,
                playerPosition.y,
                playerPosition.z
            );
        }
        transform.position = Vector3.Lerp(
            transform.position,
            playerPosition,
            offsetSmoothing * Time.deltaTime
        );
        transform.position = Vector3.Lerp(
            transform.position,
            playerPosition,
            offsetSmoothing * Time.deltaTime
        );
    }
}
