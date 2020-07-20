using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Script : MonoBehaviour
{
    private List<Button> main_menu_buttons;
    public Button button_play;
    public Button button_settings;
    public Button button_quit;

    private List<Button> difficulty_buttons;
    public Button diff1;
    public Button diff2;
    public Button diff3;
    public Button diff4;
    public Button diff5;
    public Button diff6;
    public Button diff7;
    public Button diff8;
    public Button diff9;
    public Button diff10;
    
    public Button button_link;

    private List<Button> settings_bottom_buttons;
    public Button button_back;
    public Button button_highscore;
    public Button button_credits;

    private PointerEventData pointer;
    private bool released_key = true;
    private int menu_selected = 0;
    private int button_selected = 0;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        main_menu_buttons = new List<Button>
        {
            button_play,
            button_settings,
            button_quit
        };

        difficulty_buttons = new List<Button>
        {
            diff1,
            diff2,
            diff3,
            diff4,
            diff5,
            diff6,
            diff7,
            diff8,
            diff9,
            diff10
        };
        if (StaticInfo.Game_Difficulty == false)
        {
            StaticInfo.Game_Difficulty = true;
            StaticInfo.Game_Difficulty_Int = 1;
        }


        settings_bottom_buttons = new List<Button>
        {
            button_back,
            button_highscore,
            button_credits
        };

        pointer = new PointerEventData(EventSystem.current);
        anim = GameObject.FindObjectOfType<Canvas>().GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
        {
            switch (menu_selected)
            {
                case 0:
                    ExecuteEvents.Execute(main_menu_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                    ExecuteEvents.Execute(main_menu_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    break;

                case 1:
                    for (int i = 0; i < difficulty_buttons.Count; i++)
                    {
                        ColorBlock colour = difficulty_buttons[i].colors;
                        if (i <= StaticInfo.Game_Difficulty_Int-1)
                        {
                            colour.normalColor = Color.blue;
                            colour.highlightedColor = Color.magenta;
                            colour.pressedColor = Color.magenta;
                        }
                        else
                        {
                            colour.normalColor = Color.red;
                            colour.highlightedColor = Color.magenta;
                            colour.pressedColor = Color.magenta;
                        }
                        difficulty_buttons[i].colors = colour;
                    }
                    ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                    ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    break;
            }
        }
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (released_key)
        {

            switch (menu_selected)
            {
                case 0:
                    if (input.x > 0f && button_selected != main_menu_buttons.Count-1)
                    {
                        released_key = false;
                        int button_unselected = button_selected;
                        button_selected += 1;
                        ExecuteEvents.Execute(main_menu_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(main_menu_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(main_menu_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (input.x < 0f && button_selected != 0)
                    {
                        released_key = false;
                        int button_unselected = button_selected;
                        button_selected -= 1;
                        ExecuteEvents.Execute(main_menu_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(main_menu_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(main_menu_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (Input.GetButtonDown("Submit"))
                    {
                        switch (button_selected)
                        {
                            case 0:
                                SceneManager.LoadScene(sceneName: "Mode_Classic");
                                break;
                            case 1:
                                anim.SetTrigger("TriggerTransition");
                                menu_selected = 1;
                                button_selected = 0;
                                break;
                            case 2:
                                Application.Quit();
                                break;
                        }
                    }
                    break;

                case 1:
                    if (input.x > 0f && button_selected != difficulty_buttons.Count-1)
                    {
                        released_key = false;
                        int button_unselected = button_selected;
                        button_selected += 1;
                        ExecuteEvents.Execute(difficulty_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (input.x < 0f && button_selected != 0)
                    {
                        released_key = false;
                        int button_unselected = button_selected;
                        button_selected -= 1;
                        ExecuteEvents.Execute(difficulty_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (input.y < 0f)
                    {
                        released_key = false;
                        menu_selected = 2;
                        int button_unselected = button_selected;
                        button_selected = 0;
                        ExecuteEvents.Execute(difficulty_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (Input.GetButtonDown("Submit"))
                    {
                        for (int i = 0; i < difficulty_buttons.Count; i++)
                        {
                            ColorBlock colour = difficulty_buttons[i].colors;
                            if (i <= button_selected)
                            {
                                colour.normalColor = Color.blue;
                                colour.highlightedColor = Color.magenta;
                                colour.pressedColor = Color.magenta;
                            }
                            else
                            {
                                colour.normalColor = Color.red;
                                colour.highlightedColor = Color.magenta;
                                colour.pressedColor = Color.magenta;
                            }
                            difficulty_buttons[i].colors = colour;
                        }
                        StaticInfo.Game_Difficulty_Int = button_selected + 1;
                    }
                    break;

                case 2:
                    if (input.y < 0f)
                    {
                        released_key = false;
                        menu_selected = 3;
                        button_selected = 0;
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (input.y > 0f)
                    {
                        released_key = false;
                        menu_selected = 1;
                        button_selected = 0;
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(difficulty_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (Input.GetButtonDown("Submit"))
                    {
                        // AWS Link Account
                    }
                    break;

                case 3:
                    if (input.x > 0f && button_selected != settings_bottom_buttons.Count-1)
                    {
                        released_key = false;
                        int button_unselected = button_selected;
                        button_selected += 1;
                        ExecuteEvents.Execute(settings_bottom_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (input.x < 0f && button_selected != 0)
                    {
                        released_key = false;
                        int button_unselected = button_selected;
                        button_selected -= 1;
                        ExecuteEvents.Execute(settings_bottom_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_selected].gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (input.y > 0f)
                    {
                        released_key = false;
                        menu_selected = 2;
                        int button_unselected = button_selected;
                        button_selected = 0;
                        ExecuteEvents.Execute(settings_bottom_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerUpHandler);
                        ExecuteEvents.Execute(settings_bottom_buttons[button_unselected].gameObject, pointer, ExecuteEvents.pointerExitHandler);
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(button_link.gameObject, pointer, ExecuteEvents.pointerDownHandler);
                    }
                    else if (Input.GetButtonDown("Submit"))
                    {
                        switch (button_selected)
                        {
                            case 0:
                                anim.SetTrigger("TriggerTransition");
                                menu_selected = 0;
                                button_selected = 0;
                                break;

                            case 1:
                                // AWS Leaderboard
                                break;

                            case 2:
                                // Credits
                                break;
                        }
                    }
                    break;
            }
        }
        else if (input == Vector2.zero)
        {
            released_key = true;
        }
    }
}
