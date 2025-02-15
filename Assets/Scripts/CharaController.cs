using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{

    private Vector3 _inputDirection;
    [SerializeField] float _speed;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _inputDirection = new Vector3(horizontal, 0, vertical).normalized;
    }

    private void FixedUpdate()
    {
        Movement(_inputDirection, _speed);
    }

    
    private void Movement(Vector3 inputDirection, float speed)
    {
        transform.Translate(inputDirection * speed * Time.deltaTime);

    }
    
}