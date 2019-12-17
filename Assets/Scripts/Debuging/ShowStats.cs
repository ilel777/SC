using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   ShowStats
/// </summary>
public class ShowStats : MonoBehaviour
{
    // show player speed support
    [SerializeField]
    private Text playerSpeedText;
    private Rigidbody playerRb;
    private Vector3 playerOldPos;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerOldPos = playerRb.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float playerSpeed = (playerRb.transform.position - playerOldPos).magnitude / Time.deltaTime;
        playerSpeedText.text = "Player Speed: " + playerSpeed;
        playerOldPos = playerRb.transform.position;
    }
}
