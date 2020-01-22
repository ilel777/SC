using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    #region Fields

    private Rigidbody _rb;
    private ISize _size;
    private PlayerShipConfig _config;

    #endregion

    void Awake()
    {
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        _size = GetComponent<ISize>();
        _config = GetComponent<IConfig>().DefaultConfig as PlayerShipConfig;

        yield return new WaitUntil(() => _config != null);
        ConfigureMovement();
    }

    protected override void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // horizontalInput = Mathf.Sign(horizontalInput) * (Mathf.Ceil(Mathf.Abs(horizontalInput)));
        // verticalInput = Mathf.Sign(verticalInput) * (Mathf.Ceil(Mathf.Abs(verticalInput)));

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (direction == Vector3.zero) _rb.drag = 10;
        else _rb.drag = 1;

        _rb.AddForce(Speed * direction * _rb.mass, ForceMode.Force);
        KeepInsideScreen();
    }

    /// <summary>
    ///   Keep Player Ship inside screen Boundaries
    /// </summary>
    void KeepInsideScreen()
    {
        _rb.position = new Vector3(
            Mathf.Clamp(_rb.position.x, ScreenUtils.ScreenLeft + (_size.GetWidth() / 2), ScreenUtils.ScreenRight - (_size.GetWidth() / 2)),
            _rb.position.y,
            Mathf.Clamp(_rb.position.z, ScreenUtils.ScreenBottom + (_size.GetHeight() / 2), ScreenUtils.ScreenTop - (_size.GetHeight() / 2))
            );
    }

    protected override void ConfigureMovement()
    {
        // configure movement component
        if (_config != null)
        {
            Speed = _config.movement.speed;
        }
    }
}
