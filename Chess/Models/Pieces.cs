using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;

namespace Chess.Models
{
    public class Piece 
    {
        /*1 King
          2 Queen
          3 Rook
          4 Bishop
          5 Horse
          6 Pawn*/
        

        public Color Color { get; set; }
        public int Type { get; set; }
        public Image Image { get; set; }
    }

    class King : Piece
    {
        public King(Color color)
        {
            Color = color;
            Image = new Image();

            if (color == Color.White)
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/kingW.png", UriKind.Absolute));
            }
            else
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/king.png", UriKind.Absolute));
            }

            Type = 1;
        }
    }

    class Queen : Piece
    {
        public Queen(Color color)
        {
            Color = color;
            Image = new Image();

            if (color == Color.White)
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/queenW.png", UriKind.Absolute));
            }
            else
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/queen.png", UriKind.Absolute));
            }

            Type = 2;
        }
    }

    class Rook : Piece
    {
        public Rook(Color color)
        {
            Color = color;
            Image = new Image();

            if (color == Color.White)
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/rookW.png", UriKind.Absolute));
            }
            else
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/rook.png", UriKind.Absolute));
            }
            

            Type = 3;
        }
    }

    class Bishop : Piece
    {
        public Bishop(Color color)
        {
            Color = color;
            Image = new Image();


            if (color == Color.White)
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/bishopW.png", UriKind.Absolute));
            }
            else
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/bishop.png", UriKind.Absolute));
            }

            Type = 4;
        }
    }

    class Knight : Piece
    {
        public Knight(Color color)
        {
            Color = color;
            Image = new Image();

            if (color == Color.White)
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/horseW.png", UriKind.Absolute));
            }
            else
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/horse.png", UriKind.Absolute));
            }
            
            Type = 5;
        }
    }

    class Pawn : Piece
    {
        public Boolean IsFirstMove { get; set; }

        public Pawn(Color color)
        {
            IsFirstMove = true;
            Color = color;
            Image = new Image();

            if (color == Color.White)
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/pawnW.png", UriKind.Absolute));
            }
            else
            {
                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/pawn.png", UriKind.Absolute));
            }

            Type = 6;
        }
    }


}
