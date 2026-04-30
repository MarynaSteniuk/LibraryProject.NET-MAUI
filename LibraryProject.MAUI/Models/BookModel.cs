using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryProject.MAUI.Models;
public class BookModel : INotifyPropertyChanged
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Isbn { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    private bool _isFavorite;
    public bool IsFavorite
    {
        get => _isFavorite;
        set
        {
            _isFavorite = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(HeartIcon)); 
            OnPropertyChanged(nameof(HeartColor)); 
        }
    }

    public string HeartIcon => IsFavorite ? "♥" : "♡";
    public string HeartColor => IsFavorite ? "#E5989B" : "#A3918E";

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}