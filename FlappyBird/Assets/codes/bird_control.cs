using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bird_control : MonoBehaviour
{
    public Sprite[] bird_sprite;
    SpriteRenderer spriterenderer;
    bool forward_back_ctrl = true;
    int bird_counter = 0;
    float bird_animation_time = 0;

    Rigidbody2D physic;

    int score = 0;

    public Text score_text;

    bool game_over = true;

    game_control game_control;

    AudioSource []sounds;

    int high_score = 0;


    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        physic = GetComponent<Rigidbody2D>();
        game_control = GameObject.FindGameObjectWithTag("game_control_TAG").GetComponent<game_control>();
        sounds = GetComponents<AudioSource>();
        high_score = PlayerPrefs.GetInt("save");
    }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && game_over)
        {
            physic.velocity = new Vector2(0, 0);     //speed was zero           
            physic.AddForce(new Vector2(0, 200));    //Then we applied force.
            sounds[0].Play();
        }
        if (physic.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
        animation_bird();
    }


    void animation_bird()
    {
        bird_animation_time += Time.deltaTime;
        if (bird_animation_time > 0.2f)
        {
            bird_animation_time = 0;
            if (forward_back_ctrl)
            {
                spriterenderer.sprite = bird_sprite[bird_counter];
                bird_counter++;
                if (bird_counter == bird_sprite.Length)
                {
                    bird_counter--;
                    forward_back_ctrl = false;
                }
            }
            else
            {
                bird_counter--;
                spriterenderer.sprite = bird_sprite[bird_counter];
                if (bird_counter == 0)
                {
                    bird_counter++;
                    forward_back_ctrl = true;
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="score_TAG")
        {
            score++;
            score_text.text = "score = " + score;
            sounds[1].Play();
            Debug.Log(score);
        }
        if (col.gameObject.tag=="barrier_TAG")
        {
            game_over = false;
            sounds[2].Play();
            game_control.game_over();
            GetComponent<CircleCollider2D>().enabled = false;

            if (score>high_score)
            {
                high_score = score;
                PlayerPrefs.SetInt("save", high_score);
            }
            Invoke("back_menu",2);
        }
    }

    void back_menu()
    {
        PlayerPrefs.SetInt("score_save", score);
        SceneManager.LoadScene("menu");
    }
}
