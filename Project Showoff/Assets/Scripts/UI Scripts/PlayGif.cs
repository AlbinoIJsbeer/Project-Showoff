using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGif : MonoBehaviour
{
    [SerializeField]
    private int _maxFrames = 0;
    [SerializeField]
    private Texture2D[] frames;
    private float framesPerSecond = 100f;
    [SerializeField]
    private RawImage _image;
    private int _index = 0;

    void Update() {
        //int index = ((int)Time.time * (int)framesPerSecond) % frames.Length;
        //index = index % frames.Length;
        //GetComponent<RawImage>().texture = frames[index];
        //Debug.Log(index);
        if (_index < frames.Length - 1)
        {
            GetComponent<RawImage>().texture = frames[_index];
            _index++;
        }else if (_index == frames.Length)
        {
            GetComponent<RawImage>().texture = frames[_index];
            _index = 0;
        }
        else
        {
            _index = 0;
        }
        Debug.Log(_index);

    }



}
