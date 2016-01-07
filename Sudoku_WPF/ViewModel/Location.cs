using SudokuBasis;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Sudoku_WPF.ViewModel {
    public class Location : INotifyPropertyChanged
    {
        private SudokuGame _game;
        private Position _position;

        public event PropertyChangedEventHandler PropertyChanged;

        public Location(SudokuGame game, Position position)
        {
            _game = game;
            _position = position;
        }
        public short Value
        {
            get { return _position.Value; }
            set {
                //Validate value
                if (value > 9 || value < 1) {
                    MessageBox.Show("Vul een waarde van 1 t/m 9 in.", "Ongeldige waarde", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                //Value has been validated, set the property
                _position.Value = value;
                //Update class library value
                _game.SetValue(_position);
                //Check if game has been finished
                if (_game.IsCompleted())
                {
                    MessageBox.Show("De Sudoku is opgelost", "Sudoku opgelost", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                OnPropertyChanged();
            }
        }

        public short X {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public short Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public bool IsEditable {
            get { return _position.IsEditable; }
            set { _position.IsEditable = value; }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
