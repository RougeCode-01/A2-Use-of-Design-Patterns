using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractionCommand _currentCommand;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentCommand?.Execute();
        }
    }

    public void SetInteractionCommand(IInteractionCommand command)
    {
        _currentCommand = command;
    }
}