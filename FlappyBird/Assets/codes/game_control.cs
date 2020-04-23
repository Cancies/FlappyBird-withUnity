using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_control : MonoBehaviour
{
    public GameObject sky1;
    public GameObject sky2;
    public float background_speed=-1.5f;

    Rigidbody2D physic1;
    Rigidbody2D physic2;

    float length=0;

    public GameObject barrier;
    public int how_many_barrier=5;
    GameObject []barriers;

    float time = 0;
    int counter=0;
    bool game_stop = true;

    void Start()
    {
        physic1 = sky1.GetComponent<Rigidbody2D>();
        physic2 = sky2.GetComponent<Rigidbody2D>();
        
        physic1.velocity = new Vector2(background_speed,0);
        physic2.velocity = new Vector2(background_speed,0);

        length = sky1.GetComponent<BoxCollider2D>().size.x;

        barriers = new GameObject[how_many_barrier];

        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i] = Instantiate(barrier,new Vector2(-20,-20),Quaternion.identity);
            Rigidbody2D physic_barrier = barriers[i].AddComponent<Rigidbody2D>();
            physic_barrier.gravityScale = 0;
            physic_barrier.velocity = new Vector2(background_speed,0);
        }
    }

    
    void Update()
    {
        if (game_stop)
        {
            if (sky1.transform.position.x <= -length)
            {
                sky1.transform.position += new Vector3(length * 2, 0);
            }
            if (sky2.transform.position.x <= -length)
            {
                sky2.transform.position += new Vector3(length * 2, 0);
            }

            //--------------------------------------------------------------------------------------

            time += Time.deltaTime;
            if (time > 2f)
            {
                time = 0;
                float y_axis = Random.Range(-0.90f, 1.10f);
                barriers[counter].transform.position = new Vector3(18, y_axis);
                counter++;
                if (counter >= barriers.Length)
                {
                    counter = 0;
                }
            }
        }
        else
        {

        }
        
    }

    public   void game_over()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].GetComponent<Rigidbody2D>().velocity=Vector2.zero ;
            physic1.velocity = Vector2.zero;
            physic2.velocity = Vector2.zero;  
        }
        game_stop = false;
    }

}
