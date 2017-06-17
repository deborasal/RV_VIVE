using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCursor : MonoBehaviour
{

    private static float offsetFromPlayer = 1f;

    public Transform player;
    public Transform mousePosMarker;

    private Vector3 aux_position;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        aux_position = player.position;
        aux_position.z += offsetFromPlayer;
        mousePosMarker.position = aux_position;
	}
}
