using System;
using UnityEngine;

public class Chara1Controller : CharaController
{
    private bool _canJump;
    public float JumpForce;

    private void Start()
    {
        _isActive = false;
    }
    void Update()
    {
        if (_isActive)
        {
            _translation.x = Input.GetAxisRaw("Horizontal");
            _translation.z = Input.GetAxisRaw("Vertical");


            if (Input.GetKeyDown(_swapKey))
            {
                _swapKeyPressed = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _canJump = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (_isActive)
        {
            if (_translation != Vector3.zero) 
            {
                Movement();
                Rotation();

            }

            if (_canJump)
            {
                Jump();
                _canJump=false;
            }

            if (_swapKeyPressed)
            {
                _swapKeyPressed = false;
                SwapCharacter();
            }
        }
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    public override void SwapCharacter()
    {
        GameManager.MainGame.CharaController = GameManager.MainGame.MainChara;
        GameManager.MainGame.CameraController.UpdateTarget(GameManager.MainGame.CharaController.transform);
        GameManager.MainGame.MainChara.SetIsActive(true);
        _isActive = false;
    }
}
