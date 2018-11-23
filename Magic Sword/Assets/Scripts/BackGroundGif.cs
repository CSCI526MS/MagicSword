using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundGif : MonoBehaviour {
    public Texture[] frames;
    public int framesPerSecond = 10;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update () {
        int index = (int)(Time.time * framesPerSecond) % frames.Length;
        rend.material.mainTexture = frames[index];
    }
}
