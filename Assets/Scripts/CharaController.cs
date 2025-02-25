using UnityEngine;

public class CharaController : MonoBehaviour
{

    private RaycastHit[] hit;

    [Header("Basic values")]
    [SerializeField] protected float _speed;
    [SerializeField] protected float _speedRotation;

    protected Vector3 _translation;
    protected Quaternion _targetRotation;

    [Header("Swap character")]
    [SerializeField] protected KeyCode _swapKey;
    [SerializeField] protected LayerMask _charaMask;
    [SerializeField] protected float _swapRadius;

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
            _translation.x = Input.GetAxisRaw("Horizontal");
            _translation.z = Input.GetAxisRaw("Vertical");

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
            if (_translation != Vector3.zero)
            {
                Movement();
                Rotation();
            }

            if (_swapKeyPressed)
            {
                _swapKeyPressed = false;
                SwapCharacter();
            }
        }
    }


    protected void Movement()
    {
        transform.Translate(_translation * _speed * Time.deltaTime, Space.World);

    }
    protected void Rotation()
    {

        _targetRotation = Quaternion.LookRotation(_translation);
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, _targetRotation.eulerAngles.y, _speedRotation * Time.deltaTime);
        transform.eulerAngles = Vector3.up * angle;
    }

    public virtual void SwapCharacter()
    {

        int totalHit = Physics.SphereCastNonAlloc(transform.position, _swapRadius, transform.forward, hit, 0, _charaMask);
        if (totalHit > 0)
        {
            RaycastHit currentHit = hit[0];
            for (int i = 0; i < totalHit - 1; i++)
            {
                if (currentHit.distance > hit[i].distance)
                {
                    currentHit = hit[i];
                }
            }
            currentHit.transform.gameObject.TryGetComponent(out CharaController chara);
            Debug.Log(chara);
            _isActive = false;
            chara._isActive = true;
            GameManager.MainGame.CharaController = chara;
            GameManager.MainGame.CameraController.UpdateTarget(chara.transform);
        }
    }

    public void SetIsActive(bool isActive)
    {
        _isActive = isActive;
    }
}