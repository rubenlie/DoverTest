using MicorWave.Interfaces;

namespace MicorWave.Services;

public class MicroWaveOvenHW: IMicrowaveOvenHW
{
    private bool _doorOpen;

    public bool DoorOpen => _doorOpen;

    public event Action<bool> DoorOpenChanged;
    public event EventHandler StartButtonPressed;
    public void TurnOnLicht()
    {
        Console.WriteLine("Light OFF");
    }

    public void TurnOffLicht()
    {
        Console.WriteLine("Light ON");
    }

    public void TurnOnHeater()
    {
        Console.WriteLine("Heater ON");
    }

    public void TurnOffHeater()
    {
        Console.WriteLine("Heater OFF");
    }

    // Simulate a door being opened/closed
    public void SetDoorState(bool isOpen)
    {
        if (_doorOpen != isOpen)
        {
            _doorOpen = isOpen;
            DoorOpenChanged?.Invoke(_doorOpen);
        }
    }

    // Simulate pressing the start button
    public void PressStartButton()
    {
        StartButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}