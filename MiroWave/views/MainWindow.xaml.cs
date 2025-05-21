using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MicorWave.controllers;
using MicorWave.Interfaces;
using MicorWave.Services;

namespace MicorWave;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow: Window
{
    private readonly Controller _controller;
    private readonly MicroWaveOvenHW _microwave;
    public MainWindow()
    {
        InitializeComponent();
        _microwave = new MicroWaveOvenHW();
        _controller = new Controller(_microwave);
        _controller.Init();

    }
    
    
    private void OpenMicroWave(object sender, RoutedEventArgs e)
    {
        // Simulate usage
        _microwave.SetDoorState(!_microwave.DoorOpen); 
    }

    private void StartMicroWave(object sender, RoutedEventArgs e)
    {
        _microwave.PressStartButton();     }
}