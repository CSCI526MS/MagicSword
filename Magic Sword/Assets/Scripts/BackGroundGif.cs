using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundGif : MonoBehaviour {
    public Texture[] frames;
    public int framesPerSecond = 10;

    void Update () {
        int index = (int)(Time.time * framesPerSecond) % frames.Length;
        GetComponent<Renderer>().material.mainTexture = frames[index];
    }
}
