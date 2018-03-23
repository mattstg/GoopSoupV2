using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    bool updateProccessed = false;
    InputInfo inputInfo;

    public void InitializeManager()
    {

    }

    //THIS SHOULD BE CALLED BEFORE ANY GetInputInfo is called!!!! else will give last updates info
    public void UpdateManager(float dt)
    {
        updateProccessed = false;
    }

    /// <summary>
    /// Returns this updates inputInfo, generates one if first call this update
    /// </summary>
    /// <returns>This updates inputInfo</returns>
    public InputInfo GetInputInfo()
    {
        if (!updateProccessed)
        {
            inputInfo = CreateInputInfo();
            updateProccessed = true;
        }
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
        bool throwPressed = Input.GetMouseButtonDown(0);
        return new InputInfo(dirPressed, pickDropPressed, throwPressed);
    }


    public class InputInfo
    {
        public Vector2 dirPressed;
        public bool pickDropPressed;
        public bool throwPressed;

        public InputInfo(Vector2 _dirPressed, bool _pickDropPressed, bool _throwPressed)
        {
            dirPressed = _dirPressed;
            pickDropPressed = _pickDropPressed;
            throwPressed = _throwPressed;
        }
    }
}
