using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Chess.Models
{
    class Piece : Button
    {
        /*1 King
          2 Queen
          3 Rook
          4 Bishop
          5 Horse
          6 Pawn*/

        public int Type { get; set; }
    }
}
