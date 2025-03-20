using UnityEngine;

public class CharaController : MonoBehaviour
{

    private Collider[] _col;

    [Header("Basic values")]
    [SerializeField] protected float _speed;
    [SerializeField] protected float _speedRotation;
    [SerializeField] private float _rotation;

    protected Vector3 _forward;
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
        _forward = new Vector3(0, 0, 1);
        if (Vector3.Angle(transform.forward, _forward) <= Vector3.Angle(_forward, transform.forward))
        {
            _rotation = Vector3.Angle(transform.forward, _forward);
        }
        else
        {
            _rotation = Vector3.Angle(_forward, transform.forward);
        }
        _isActive = true;
        _col = new Collider[3];
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _swapRadius);
    }

    protected void Movement()
    {
        transform.Translate(_translation * _speed * Time.deltaTime, Space.World);

    }
    protected void Rotation()
    {

        // change l'angle (rotation)
        _targetRotation = Quaternion.LookRotation(_translation);
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, _targetRotation.eulerAngles.y, _speedRotation * Time.deltaTime);
        transform.eulerAngles = Vector3.up * angle;
    }

    public virtual void SwapCharacter()
    {

        int totalCol = Physics.OverlapSphereNonAlloc(transform.position, _swapRadius, _col, _charaMask);
        Debug.Log(totalCol);

        if (totalCol > 0)
        {
            Collider currentCol = _col[0];
            float dist1 = Mathf.Sqrt(Mathf.Pow(currentCol.transform.position.x - transform.position.x,2) + 
                Mathf.Pow(currentCol.transform.position.x - transform.position.x, 2) + 
                Mathf.Pow(currentCol.transform.position.x - transform.position.x, 2));
            if (totalCol > 1)
            {
                for (int i = 1; i < totalCol; i++)
                {
                    float dist2 = Mathf.Sqrt(Mathf.Pow(_col[i].transform.position.x - transform.position.x, 2) +
                        Mathf.Pow(_col[i].transform.position.x - transform.position.x, 2) +
                        Mathf.Pow(_col[i].transform.position.x - transform.position.x, 2));
                    //Debug.Log(_col[i].gameObject);
                    if (dist1 > dist2)
                    {
                        currentCol = _col[i];
                        dist1 = dist2;
                    }
                }

            }
            currentCol.gameObject.TryGetComponent(out CharaController chara);
            //Debug.Log(chara);

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