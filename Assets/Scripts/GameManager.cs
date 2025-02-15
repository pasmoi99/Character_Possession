using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager MainGame;

    [Header("Camera")] 
    public CameraController CameraController;

    [Header("Character")]
    public CharaController MainChara;
    public CharaController CharaController;


    void Awake()
    {
        MainGame = this;
    }

   
}
