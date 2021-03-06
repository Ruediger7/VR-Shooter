using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    CharacterController cc;
    public AudioClip footstep;
    AudioSource m_MyAudioSource;

    bool m_Play;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.Play();
        m_MyAudioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 1.0f && m_MyAudioSource.isPlaying == false)
        {
            m_MyAudioSource.PlayOneShot(footstep, Random.Range(0.8f, 1));
        }
    }
}
