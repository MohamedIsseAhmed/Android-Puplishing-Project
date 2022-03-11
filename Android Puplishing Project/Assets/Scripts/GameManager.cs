using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool pauseTheGame = false;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Court.UnPaueTheGame += Court_UnPaueTheGame;
    }
    private void OnDisable()
    {
        Court.UnPaueTheGame -= Court_UnPaueTheGame;
    }
    private void Court_UnPaueTheGame()
    {
       pauseTheGame = false;
    }
}
