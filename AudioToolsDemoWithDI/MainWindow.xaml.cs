using System.Windows;

namespace AudioToolsDemo;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
        Closing += (s, a) => (DataContext as MainViewModel)!.Dispose();
    }
}
