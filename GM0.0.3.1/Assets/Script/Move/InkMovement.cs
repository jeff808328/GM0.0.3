using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkMovement : MonoBehaviour
{
    public CharacterController controller;
    
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    
    public float speed = 6f;
    public float gravity = -9.8f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3f;
    
    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        
        
        //獲取水平(X軸)與垂直(Y軸)輸入
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //取得玩家向量，moveDirection提供X軸參數，faceDirection提供X、Z軸參數
        Vector2 moveDirection = new Vector2(horizontal, 0f).normalized;
        Vector3 faceDirection = new Vector3(horizontal, 0f, vertical).normalized;

        //向量大於0.1時，移動物件
        if(moveDirection.magnitude >= 0.1f)
        {
            //取得角度(Rad)後，轉換為Degree，並旋轉
            float targetAngle = Mathf.Atan2(faceDirection.x, faceDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        //重力加速度
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        //地面檢測，在檢測位置放置一個檢測球體
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
         
        //若檢測到地面，重置下落速度
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        //跳躍
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    }
}
