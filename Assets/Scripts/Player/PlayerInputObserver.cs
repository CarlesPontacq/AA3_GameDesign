using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputObserver : MonoBehaviour
{
    public enum ActionMap { Player }

    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInputRef;
    public Vector2 movement { get; private set; } = Vector2.zero;
    public bool IsPressingAttack { get; private set; } = false;

    public Action onAttack;
    public Action onPause;

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        movement = new Vector2(Mathf.RoundToInt(movement.x), Mathf.RoundToInt(movement.y));
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onAttack?.Invoke();
    }

    public void SwitchActionMap(ActionMap actionMap)
    {
        playerInputRef.SwitchCurrentActionMap(GetActionMapString(actionMap));
    }

    private string GetActionMapString(ActionMap actionMap)
    {
        string actionMapString = "";
        switch (actionMap)
        {
            case ActionMap.Player:
                actionMapString = "Player";
                break;
        }
        return actionMapString;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            onPause?.Invoke();
    }
}
