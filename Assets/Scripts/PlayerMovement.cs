using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyBox;

public class PlayerMovement : MonoBehaviour
{
    //Ускорение и максимальная скорость
    public float velocity, maxSpeed;

    //Сила прыжка
    public float jumpForce;

    //--------------------------------------//
    // Переменные для более красивого прыжка//

    //Усиляет гравитацию при падении
    public float fallMultiplier;

    //Насколько выше мы будем прыгать, если будем удерживать пробел
    public float lowJumpMultiplier;
    //--------------------------------------//

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Прыжок по пробелу
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        //Получаем пользовательский ввод 
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Движение        
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        //Увеличиваем скорость игрока, только если она и так не слишком быстрая
        if (rb.velocity.x < maxSpeed)
        {
            rb.AddForce(movement * velocity * Time.fixedDeltaTime);

            //Если пытаемся отойти от передвигаемого объекта, отпускаем его
            if (movingRb.velocity.x < maxSpeed)
            {
                if (this.transform.position.x - movingRb.gameObject.transform.position.x < 0)
                {
                    if (moveHorizontal < 0)
                    {
                        movingRb.mass = tempMass;
                        movingRb.drag = tempDrag;
                        velocity = temp;
                        movingObject = null;
                    }
                }
                else
                {
                    if (moveHorizontal > 0)
                    {
                        movingRb.mass = tempMass;
                        movingRb.drag = tempDrag;
                        velocity = temp;
                        movingObject = null;
                    }
                } 
                movingRb.AddForce(movement * velocity * Time.fixedDeltaTime);
            }
        }

        //Это тоже прыжок
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime);
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);
        }

        //Если игрок стоит на обьекте, то он его не перемещает, чтобы игрок не мог превращать кубики в жигули
        if (this.GetComponent<BoxCollider2D>().bounds.min.y > movingObject.gameObject.GetComponent<BoxCollider2D>().bounds.max.y)
        {
            print("Goddammit");
            movingRb.mass = tempMass;
            movingRb.drag = tempDrag;
            velocity = temp;
            movingObject = null;
        }
    }


    /// <summary>
    /// Проверяем, является ли объект передвигаемым,
    /// И если является, то двигаем его вместе с игроком.
    /// Чтобы пометить объект как передвигаемый, 
    /// Нужно добавить на него скрипт "MovableObject"
    /// </summary>
    private MovableObject movingObject;
    private Rigidbody2D movingRb;
    private float temp;
    private float tempMass;
    private float tempDrag;

    //Замедление игрока при движении обьекта
    [Min(1)]
    public float movingObjectSlowdown;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<MovableObject>(out movingObject))
        {
            //Запоминаем физические характеристики объекта для дальнейшего возвращения
            temp = velocity;
            movingRb = collision.gameObject.GetComponent<Rigidbody2D>();
            tempMass = movingRb.mass;
            tempDrag = movingRb.drag;

            //Меняем несколько переменных для корректной работы с физикой
            movingRb.mass = 0.03f;
            movingRb.drag = 3;

            velocity /= tempMass * (tempDrag + 1) * movingObjectSlowdown;
            if (velocity / movingRb.mass < velocity)
            {
                velocity /= movingRb.mass;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Возвращаем характеристики объекта
        movingRb.mass = tempMass;
        movingRb.drag = tempDrag;
        velocity = temp;
        movingObject = null;
        collision = null;
    }
}