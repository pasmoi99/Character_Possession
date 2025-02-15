using UnityEngine;

public class CharaController : MonoBehaviour
{

    protected Vector3 _inputDirection;
    private RaycastHit[] hit;

    [Header("Basic values")]
    [SerializeField] protected float _speed;

    [Header("Swap character")]
    [SerializeField] protected KeyCode _swapKey;
    [SerializeField] LayerMask _charaMask;
    [SerializeField] float _swapRadius;
    protected bool _swapKeyPressed;
    protected bool _isActive;

    private void Start()
    {
        _isActive = true;
        hit = new RaycastHit[3];
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
        }
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            Movement(_inputDirection, _speed);

            if (_swapKeyPressed)
            {
                SwapCharacter();
                _swapKeyPressed = false;
            }
        }
    }


    protected void Movement(Vector3 inputDirection, float speed)
    {
        transform.Translate(inputDirection * speed * Time.deltaTime);

    }
    virtual public void SwapCharacter()
    {

        Physics.SphereCastNonAlloc(transform.position, _swapRadius, transform.forward, hit, 10, _charaMask);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.TryGetComponent(out CharaController chara))
            {
                _isActive = false;
                chara._isActive = true;
                GameManager.MainGame.CharaController = chara;
                GameManager.MainGame.CameraController.UpdateTarget(chara.transform);
                break;
            }
        }
    }

    public void SetIsActive(bool isActive)
    {
        _isActive = isActive;
    }
}