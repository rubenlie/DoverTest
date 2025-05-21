using MicorWave.Interfaces;

namespace MicorWave.controllers;

public class Controller(IMicrowaveOvenHW _microwave )
{
    private Timer? _timer;



    public int TimerValue { get; private set; } 
    public void Init()
    {
        _microwave.DoorOpenChanged += OnDoorOpenChanged;
        _microwave.StartButtonPressed += OnStartButtonPressed;
    }

    private void OnDoorOpenChanged(bool isOpen)
    {
        if (isOpen  )
        {
           _microwave.TurnOnLicht();
            if (_timer != null)
            {
                _microwave.TurnOffHeater(); // Safety precaution
            }
        }
        else
        {
            _microwave.TurnOffLicht();
        }
    }


    private void OnStartButtonPressed(object? sender, EventArgs e)
    {
        if (_microwave.DoorOpen) return;
        if(_timer == null) {
            _microwave.TurnOnHeater();
            TimerValue = 60;
            StartTimer();
        }
        else
        {
            AddTime(60);
        }
    }
    
    private void StartTimer()
    {
        _timer?.Dispose(); // Clean up any previous timer
        _timer = new Timer(OnTimerTick, null, 1000, 1000); // Start timer with 1-second interval
    }
    
    private void StopTimer()
    {
        _timer?.Dispose();
        _timer = null;
        
    }

    private void OnTimerTick(object? state)
    {
        if (TimerValue <= 0) return;
        TimerValue--;
        if (TimerValue != 0) return;
        _microwave.TurnOffHeater();
        StopTimer();
    }
    
    private void AddTime(int additionalTimeInSeconds)
    {
        TimerValue += additionalTimeInSeconds;

        // Restart the timer with the updated value
        // This resets the due time and keeps the period the same (1 second interval)
        _timer?.Change(1000, 1000);  // 1 second interval
    }
}