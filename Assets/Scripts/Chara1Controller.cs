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
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            _inputDirection = new Vector3(horizontal, 0, vertical).normalized;

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
            Movement(_inputDirection,_speed);
            if (_canJump)
            {
                Jump();
                _canJump=false;
            }

            if (Input.GetKeyDown(_swapKey))
            {
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
        GameManager.MainGame.MainChara.SetIsActive(true);
        _isActive = false;
        GameManager.MainGame.CharaController = GameManager.MainGame.MainChara;
        GameManager.MainGame.CameraController.UpdateTarget(GameManager.MainGame.CharaController.transform);
    }
}
