using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioToolsDemo;
internal class MainViewModel : ObservableObject, IDisposable
{
    private AudioController? _controller = new();

    private bool _playing = false;
    private bool _sourceSelected = false;
    private bool _disposedValue;
    private PeriodicTimer? _timer;
    private string _selectedDevice = String.Empty;

    public List<string> Devices => _controller?.Devices ?? new List<string>();
    public string SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            _selectedDevice = value;
            _controller?.SetDevice(value);
        }
    }
    public string AudioFilePath { get; set; } = "<select an audiofile>";
    public String AudioLength => _controller == null ? string.Empty : $"{_controller.AudioLength:hh\\:mm\\:ss}";
    public String AudioPosition => _controller == null ? string.Empty : $"{_controller.AudioPosition:hh\\:mm\\:ss}";
    public float Volume
    {
        get => _controller?.Volume ?? 0;
        set
        {
            if (_controller != null) _controller.Volume = value;
        }
    }
    public string RecordButtonCaption => _controller!.IsRecording ? "Stop Recording" : "Start Recording";

    public IRelayCommand OpenFileCommand { get; }
    public IRelayCommand PlayCommand { get; }
    public IRelayCommand PauseCommand { get; }
    public IRelayCommand RecordCommand { get; }

    public MainViewModel()
    {
        OpenFileCommand = new RelayCommand(OpenFile);
        PlayCommand = new RelayCommand(PlaySource, () => _sourceSelected && !_playing);
        PauseCommand = new RelayCommand(StopSource, () => _playing);
        RecordCommand = new RelayCommand(ToggleRecording, () => _playing);
        SelectedDevice = Devices[0];
    }

    private void OpenFile()
    {

        _sourceSelected = false;
        StopSource();
        _timer?.Dispose();
        string? path = GetSourcePath();
        try
        {
            if (!string.IsNullOrEmpty(path))
            {
                _controller?.SetSource(path);
                _sourceSelected = true;
                AudioFilePath = System.IO.Path.GetFileName(path);
                OnPropertyChanged(nameof(AudioFilePath));
                OnPropertyChanged(nameof(AudioLength));
                PlaySource();
            }
        }
        catch (ArgumentException e)
        {
            _controller?.Stop();
            _playing = false;
            _sourceSelected = false;
            UpdateUiCommandsState();
            AudioFilePath = e.Message;
            OnPropertyChanged(nameof(AudioFilePath));
        }
    }

    private static string GetSourcePath()
    {
        OpenFileDialog? dialog = new()
        {
            Filter = "Audio files (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*",
        };
        return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : string.Empty;
    }

    private void UpdateUiCommandsState()
    {
        PlayCommand.NotifyCanExecuteChanged();
        PauseCommand.NotifyCanExecuteChanged();
        RecordCommand.NotifyCanExecuteChanged();
        OnPropertyChanged(nameof(RecordButtonCaption));
    }

    private async void PlaySource()
    {
        _controller?.Start();
        _playing = true;
        UpdateUiCommandsState();
        _timer = new(TimeSpan.FromSeconds(1));
        while (_playing && await _timer.WaitForNextTickAsync())
        {
            OnPropertyChanged(nameof(AudioPosition));
            if (_controller!.AudioPosition >= _controller.AudioLength) StopSource();
        }
    }

    private void StopSource()
    {
        _timer?.Dispose();
        _controller?.Stop();
        _playing = false;
        UpdateUiCommandsState();
    }

    private void ToggleRecording()
    {
        if (_controller!.IsRecording)
        {
            _controller.StopRecording();
        }
        else
        {
            _controller.StartRecording();
        }
        OnPropertyChanged(nameof(RecordButtonCaption));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _controller?.Dispose();
            }
            _controller = null;
            _disposedValue = true;
        }
    }
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
