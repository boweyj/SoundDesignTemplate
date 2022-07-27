using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private GameController controller;

    Vector2 mouse_position;

    bool mouse_down, mouse_up, mouse_drag;
    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void OnSelect()
    {
        mouse_down = true;
    }

    public void OnDeselect()
    {
        mouse_up = true;
    }

    public void OnMouseMove(InputValue value)
    {
        mouse_position = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector2 mouse_world = Camera.main.ScreenToWorldPoint(mouse_position);
        Vector2Int coord = Vector2Int.RoundToInt(mouse_world);

        if (mouse_down)
        {
            MainDown(coord);
            mouse_down = false;
            mouse_drag = true;
        }
        if (mouse_up)
        {
            MainUp(coord);
            mouse_up = false;
            mouse_drag = false;
        }
            
        if (mouse_drag)
            MainDrag(coord);

    }

    private void MainDown(Vector2Int c)
    {
        controller.MainDown(c);
    }

    private void MainUp(Vector2Int c)
    {

    }

    private void MainDrag(Vector2Int c)
    {

    }
}
