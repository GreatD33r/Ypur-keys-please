using UnityEngine;



public class CarsMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float horizontalSpeed;
    [SerializeField] public float verticalSpeed;
    private SpriteRenderer _spriteRend;
    [SerializeField] Sprite _backSp;
    [SerializeField] Collider2D _firstCollider;
    [SerializeField] Collider2D _secondCollider;
    [SerializeField] GameObject _firstParticles;
    [SerializeField] GameObject _secondParticles;
    [SerializeField] GameObject _driver;
    public bool _readyToTurn;
    public bool RoadBus;
    bool _plSit = false;
    private Vector2 _driverSpawnPoint;
    public bool onPark = false;

    public enum carsNumber : int
    {
        Car0,
        Car1,
        Car2,
        Car3,
        Car4,
        Car5,
        Car6,
        Car7,
        Car8,
        Car9,
        Car10,
        Car11,
        Car12,
        Car13,
        Car14,
        Car15,
        Car16,
        Car17,
        Car18,
        car19
    }

    public carsNumber carNumber;


    void Start()
    {
        
        _spriteRend = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _driverSpawnPoint = new Vector2(transform.position.x + 2, transform.position.y);
        _plSit = CameraMng._plSit;
        _readyToTurn = CheckpointSorter._onTrigger;
        RoadBus = RoadBusy._isBusy;
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);

        /*if (_plSit & Input.GetKeyDown(KeyCode.W))
        {
            horizontalSpeed = 0;
            verticalSpeed = 4;
        }*/
    }

    private void CarMove()
    {
        //gameObject.transform.Rotate( 0, 0, -90);
        //rb.MovePosition(transform.position + transform.up * _speedCar * Time.fixedDeltaTime);
        horizontalSpeed = 0;
        verticalSpeed = 4;
        _spriteRend.sprite = _backSp;
        _firstCollider.isTrigger = true;
        _secondCollider.isTrigger = false;
        _firstParticles.SetActive(false);
        _secondParticles.SetActive(true);
    }
    
    private void Stop()
    {
        horizontalSpeed = 0;
        verticalSpeed = 0;
        //rb.velocity = new Vector2(0, 0);
        Instantiate(_driver, _driverSpawnPoint, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Point"))
        {

            if (onPark == true)
            {
                if (collision.CompareTag("Right") && _readyToTurn && !RoadBus) 
                {
                    rb.MoveRotation(0);
                    CarMove();
                    
                }

                else if (collision.CompareTag("StopPoint"))
                {
                    Stop();
                }
            }
        }
    }
}