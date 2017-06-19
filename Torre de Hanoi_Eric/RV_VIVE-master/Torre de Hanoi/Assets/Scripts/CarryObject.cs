using UnityEngine;
using System.Collections;

public class CarryObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundToPlay;
    //private AudioSource audio;
    private bool touched = false;

    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 5)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                //RandomAudio();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }
    //void RandomAudio()
    //{
    //    if (audio.isPlaying)
    //    {
    //        return;
    //    }
    //    audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
    //    audio.Play();

    //}
    //void OnTriggerEnter()
    //{
    //    if (beingCarried)
    //    {
    //        touched = true;
    //    }
    //}
}