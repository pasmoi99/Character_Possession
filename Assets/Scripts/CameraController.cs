using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform Player;
    private Vector3 offset;
    private Vector3 newPos;


    private void Start()
    {
        Player = GameManager.MainGame.CharaController.transform;
        offset = Player.transform.position - transform.position;
    }


    private void Update()
    {
        newPos = Player.transform.position - offset;
        transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);
    }


    public void UpdateTarget(Transform newTarget)
    {
        Player = newTarget;
        if (Player != null)
        {
            RecenterCamera();
            offset = Player.transform.position - transform.position;
        }

    }

    public void RecenterCamera()
    {
        transform.position = Player.position - offset;

    }
}
