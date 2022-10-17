using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform[] rayStartPoints;
    private GameManager gameManager;
    [SerializeField] float mobileSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

    }

    void Update()
    {
        if (!gameManager.GetLevelFinished)
        {
            TakeInput();
        }

        //  rb.velocity = new Vector3(mobileSpeed * Time.deltaTime, rb.velocity.y, 0f);
        rb.velocity = new Vector3(Mathf.Clamp((mobileSpeed * 100) * Time.deltaTime, -5f, 5f), rb.velocity.y, 0f);
    }
   private void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp((jumpPower * 100f) * Time.deltaTime, 0f, 15f), 0f);

        }


        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(Mathf.Clamp((speed * 100) * Time.deltaTime, 0f, 5f), rb.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, -89.99f, 0f), turnSpeed * Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(Mathf.Clamp((-speed * 100) * Time.deltaTime, -5f, 0f), rb.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 89.99f, 0f), turnSpeed * Time.deltaTime);

        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
    private bool OnGroundCheck()
    {
        bool hit = false;
        for (int i = 0; i < rayStartPoints.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoints[i].position, -rayStartPoints[i].transform.up, 0.50f);
            Debug.DrawRay(rayStartPoints[i].position, -rayStartPoints[i].transform.up * 0.50f, Color.red);
        }

        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void MobileMoveControl(int index)
    {
        switch (index)
        {
            case 0:
                if (OnGroundCheck())
                {
                    rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp((jumpPower * 100f) * Time.deltaTime, 0f, 15f), 0f);
                }

                break;
            case 1:
               
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, -89.99f, 0f), turnSpeed * Time.deltaTime);
                
                break;

            case 2:

               
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 89.99f, 0f), turnSpeed * Time.deltaTime);
                

                break;
            
             
        }
      
    }

    public void MoveRight()
    {
        mobileSpeed = -700f;
        
    }
    
    public void MoveLeft()
    {
        mobileSpeed = 700f;
        
    }
     public void MoveStop()
    {
        mobileSpeed = 0f;
    }

}
