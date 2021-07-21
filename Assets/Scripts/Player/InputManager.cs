using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    #region singleton
    private static InputManager instance;

    private InputManager() { }

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InputManager();
            }
            return instance;
        }
    }
    #endregion


    InputInfo inputInfo;

    

    //This class puts all input into an easy to read InputClass
    public void Initialize()
    {
        inputInfo = new InputInfo(Vector2.zero,false,false,false);
    }

    //THIS SHOULD BE CALLED BEFORE ANY GetInputInfo is called!!!! else will give last updates info
    public void Update()
    {
        inputInfo = CreateInputInfo();
    }

    /// <summary>
    /// Returns this updates inputInfo
    /// </summary>
    /// <returns>This gets inputInfo</returns>
    public InputInfo GetInputInfo()
    {
        return inputInfo;
    }

    private InputInfo CreateInputInfo()
    {
        Vector2 dirPressed = new Vector2();
        if (Input.GetKey(KeyCode.A))
            dirPressed.x -= 1;
        if (Input.GetKey(KeyCode.D))
            dirPressed.x += 1;
        if (Input.GetKey(KeyCode.W))
            dirPressed.y += 1;
        if (Input.GetKey(KeyCode.S))
            dirPressed.y -= 1;

        bool pickDropPressed = Input.GetKeyDown(KeyCode.F);
        bool makePotionPressed = Input.GetKeyDown(KeyCode.E);
        bool throwPressed = Input.GetMouseButtonDown(0);
        return new InputInfo(dirPressed, pickDropPressed, throwPressed, makePotionPressed);
    }


    public class InputInfo
    {
        public Vector2 dirPressed;
        public bool pickDropPressed;
        public bool throwPressed;
        public bool makePotionPressed;

        public InputInfo(Vector2 _dirPressed, bool _pickDropPressed, bool _throwPressed, bool _makePotionPressed)
        {
            dirPressed = _dirPressed;
            pickDropPressed = _pickDropPressed;
            throwPressed = _throwPressed;
            makePotionPressed = _makePotionPressed;
        }
    }
}
