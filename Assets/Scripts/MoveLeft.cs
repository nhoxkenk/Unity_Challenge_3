using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 20.0f;
    private PlayerController controller;
    private float leftBound = -15f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!controller.gameOver)
        {
            //thực hiện việc đẩy nhanh tiến độ cho môi trường xung quanh, bao gồm background cùng với các obstacles.
            if (controller.isSprinting)  // && gameObject.CompareTag("BackGround") (có thể bỏ vào trong if để chỉ gia tốc background).
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 1.5f);
            }else
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        }
        
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
