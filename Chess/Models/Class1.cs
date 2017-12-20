using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
namespace Chess.Models
{

    public enum Color
    {
        White,
        Black
    }

    public enum GameState
    {
        WhitePlaying,
        BlackPlaying,
    }

    public class Square : Button
    {
        public Square(Brush defaultbrush, int column, int row )
        {
            Width = 60;
            Height = 60;
            Background = defaultbrush;
            DefaultBrush = defaultbrush;
            Column = column;
            Row = row;
        }

        public int Column { get; set; }
        public int Row { get; set; }
        public Piece CurrentPiece { get; set; }
        public Brush DefaultBrush { get; set; }
        
    }

    

}
