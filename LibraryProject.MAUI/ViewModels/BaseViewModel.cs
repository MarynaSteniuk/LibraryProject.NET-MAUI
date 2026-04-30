using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryProject.MAUI.ViewModels;
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy == value) return;
            _isBusy = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotBusy)); 
        }
    }
    public bool IsNotBusy => !IsBusy;
    public bool IsAdmin => Microsoft.Maui.Storage.Preferences.Default.Get("is_admin", false);
    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set
        {
            if (_title == value) return;
            _title = value;
            OnPropertyChanged();
        }
    }
}
