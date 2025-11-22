using UnityEngine;

public class InputHandler
{
    private readonly KeyCode keyLeft;
    private readonly KeyCode keyRight;
    private readonly KeyCode keyUp;
    private readonly KeyCode keyDown;

    public InputHandler(KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        keyLeft = left;
        keyRight = right;
        keyUp = up;
        keyDown = down;
    }

    public (float x, float y) GetInputs()
    {
        float inputX = 0;
        float inputY = 0;

        if (Input.GetKey(keyLeft)) inputX = -1;
        if (Input.GetKey(keyRight)) inputX = 1;
        if (Input.GetKey(keyUp)) inputY = 1;
        if (Input.GetKey(keyDown)) inputY = -1;

        return (inputX, inputY);
    }
}
