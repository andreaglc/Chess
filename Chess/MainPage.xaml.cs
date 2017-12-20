using Chess.Models;
using Microsoft.AspNet.SignalR.Client;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using Newtonsoft.Json;
using Windows.Data.Json;
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;
using Chess.Controls;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace Chess
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SolidColorBrush blueColor;
        SolidColorBrush whiteColor;
        SolidColorBrush hoverColor;
        Piece queenW;
        Piece kingW;
        Piece rookW;
        Piece rookW2;
        Piece bishopW;
        Piece bishopW2;
        Piece knightW;
        Piece knightW2;
        Piece pawnW;
        Piece pawnW2;
        Piece pawnW3;
        Piece pawnW4;
        Piece pawnW5;
        Piece pawnW6;
        Piece pawnW7;
        Piece pawnW8;
        Piece queenB;
        Piece kingB;
        Piece rookB;
        Piece rookB2;
        Piece bishopB;
        Piece bishopB2;
        Piece knightB;
        Piece knightB2;
        Piece pawnB;
        Piece pawnB2;
        Piece pawnB3;
        Piece pawnB4;
        Piece pawnB5;
        Piece pawnB6;
        Piece pawnB7;
        Piece pawnB8;
        Square[,] squares;
        Square currentSquare;
        GameState gameState;
        int[] invCol = new int[8];
        int[] invRow = new int[8];
        int myId = 8207;
        private IHubProxy _hub;

        public MainPage()
        {
            this.InitializeComponent();
            gameState = GameState.BlackPlaying;
            InitializePieces();
            InitializeChessBoard();
            PutPieces();
            InitializeColRowArrays();
            ConfigureHub();
        }

        public void InitializePieces()
        {

            queenW = new Queen(Color.White);
            kingW = new King(Color.White);
            rookW = new Rook(Color.White);
            rookW2 = new Rook(Color.White);
            bishopW = new Bishop(Color.White);
            bishopW2 = new Bishop(Color.White);
            knightW = new Knight(Color.White);
            knightW2 = new Knight(Color.White);
            pawnW = new Pawn(Color.White);
            pawnW2 = new Pawn(Color.White);
            pawnW3 = new Pawn(Color.White);
            pawnW4 = new Pawn(Color.White);
            pawnW5 = new Pawn(Color.White);
            pawnW6 = new Pawn(Color.White);
            pawnW7 = new Pawn(Color.White);
            pawnW8 = new Pawn(Color.White);
            queenB = new Queen(Color.Black);
            kingB = new King(Color.Black);
            rookB = new Rook(Color.Black);
            rookB2 = new Rook(Color.Black);
            bishopB = new Bishop(Color.Black);
            bishopB2 = new Bishop(Color.Black);
            knightB = new Knight(Color.Black);
            knightB2 = new Knight(Color.Black);
            pawnB = new Pawn(Color.Black);
            pawnB2 = new Pawn(Color.Black);
            pawnB3 = new Pawn(Color.Black);
            pawnB4 = new Pawn(Color.Black);
            pawnB5 = new Pawn(Color.Black);
            pawnB6 = new Pawn(Color.Black);
            pawnB7 = new Pawn(Color.Black);
            pawnB8 = new Pawn(Color.Black);

        }

        public void InitializeChessBoard()
        {
            squares = new Square[8, 8];
            blueColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 37, 118, 174));
            whiteColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 239, 247, 255));
            hoverColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 232, 219, 125));
            LayoutRoot.BorderBrush = blueColor;
            LayoutRoot.MinHeight = 500;
            LayoutRoot.MinWidth = 500;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    Square sqr;

                    if (i % 2 == 0 && j % 2 == 0 || i % 2 != 0 && j % 2 != 0)
                    {
                        sqr = new Square(whiteColor, i, j);

                    }
                    else
                    {
                        sqr = new Square(blueColor, i, j);
                    }

                    sqr.Click += new RoutedEventHandler(SelectSquare);
                    LayoutRoot.Children.Add(sqr);
                    Grid.SetColumn(sqr, i);
                    Grid.SetRow(sqr, j);

                    squares[i, j] = sqr;
                }
            }
        }

        public void PutPieces()
        {
            //kings
            squares[4, 7].Content = kingW.Image;
            squares[4, 7].CurrentPiece = kingW;
            squares[4, 0].Content = kingB.Image;
            squares[4, 0].CurrentPiece = kingB;

            //queens
            squares[3, 7].Content = queenW.Image;
            squares[3, 7].CurrentPiece = queenW;
            squares[3, 0].Content = queenB.Image;
            squares[3, 0].CurrentPiece = queenB;

            //rooks
            squares[0, 7].Content = rookW.Image;
            squares[0, 7].CurrentPiece = rookW;
            squares[7, 7].Content = rookW2.Image;
            squares[7, 7].CurrentPiece = rookW2;
            squares[0, 0].Content = rookB.Image;
            squares[0, 0].CurrentPiece = rookB;
            squares[7, 0].Content = rookB2.Image;
            squares[7, 0].CurrentPiece = rookB2;

            //bishops
            squares[2, 7].Content = bishopW.Image;
            squares[2, 7].CurrentPiece = bishopW;
            squares[5, 7].Content = bishopW2.Image;
            squares[5, 7].CurrentPiece = bishopW2;
            squares[2, 0].Content = bishopB.Image;
            squares[2, 0].CurrentPiece = bishopB;
            squares[5, 0].Content = bishopB2.Image;
            squares[5, 0].CurrentPiece = bishopB2;

            //knights
            squares[1, 7].Content = knightW.Image;
            squares[1, 7].CurrentPiece = knightW;
            squares[6, 7].Content = knightW2.Image;
            squares[6, 7].CurrentPiece = knightW2;
            squares[1, 0].Content = knightB.Image;
            squares[1, 0].CurrentPiece = knightB;
            squares[6, 0].Content = knightB2.Image;
            squares[6, 0].CurrentPiece = knightB2;

            //pawns
            squares[0, 6].Content = pawnW.Image;
            squares[0, 6].CurrentPiece = pawnW;
            squares[1, 6].Content = pawnW2.Image;
            squares[1, 6].CurrentPiece = pawnW2;
            squares[2, 6].Content = pawnW3.Image;
            squares[2, 6].CurrentPiece = pawnW3;
            squares[3, 6].Content = pawnW4.Image;
            squares[3, 6].CurrentPiece = pawnW4;
            squares[4, 6].Content = pawnW5.Image;
            squares[4, 6].CurrentPiece = pawnW5;
            squares[5, 6].Content = pawnW6.Image;
            squares[5, 6].CurrentPiece = pawnW6;
            squares[6, 6].Content = pawnW7.Image;
            squares[6, 6].CurrentPiece = pawnW7;
            squares[7, 6].Content = pawnW8.Image;
            squares[7, 6].CurrentPiece = pawnW8;
            squares[0, 1].Content = pawnB.Image;
            squares[0, 1].CurrentPiece = pawnB;
            squares[1, 1].Content = pawnB2.Image;
            squares[1, 1].CurrentPiece = pawnB2;
            squares[2, 1].Content = pawnB3.Image;
            squares[2, 1].CurrentPiece = pawnB3;
            squares[3, 1].Content = pawnB4.Image;
            squares[3, 1].CurrentPiece = pawnB4;
            squares[4, 1].Content = pawnB5.Image;
            squares[4, 1].CurrentPiece = pawnB5;
            squares[5, 1].Content = pawnB6.Image;
            squares[5, 1].CurrentPiece = pawnB6;
            squares[6, 1].Content = pawnB7.Image;
            squares[6, 1].CurrentPiece = pawnB7;
            squares[7, 1].Content = pawnB8.Image;
            squares[7, 1].CurrentPiece = pawnB8;

        }

        void SelectSquare(object sender, RoutedEventArgs e)
        {

            Square sqr = (Square)sender;
            var row = Grid.GetRow(sqr);
            var column = Grid.GetColumn(sqr);

            if (sqr.CurrentPiece != null && sqr.CurrentPiece.Color != Color.Black && gameState != GameState.BlackPlaying)
            {
                SetDefaultBackgrounds();
                if (sqr.CurrentPiece.Type == 2)
                {
                    PaintMoves(sqr, sqr.CurrentPiece, 3, column, row);
                    PaintMoves(sqr, sqr.CurrentPiece, 4, column, row);
                }
                else
                {
                    PaintMoves(sqr, sqr.CurrentPiece, sqr.CurrentPiece.Type, column, row);
                }
            }
            else if (currentSquare != null && sqr.Background == hoverColor)
            {
                MovePiece(currentSquare.Column, currentSquare.Row, column, row);

            }

            Debug.WriteLine(column + "," + row);

        }

        private void InitializeColRowArrays()
        {
            int c = 0;
            
            int c2 = 7;
            for (int i = 0; i < 7; i++)
            {
                c2 = 7;
                for (int j = 0; j < 7; j++)
                {
                    invCol[i] = i;
                    invRow[j] = c2;
                    c2--;
                }
            }

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Debug.WriteLine("normal: "+ i + "," + j);
                    Debug.WriteLine(invCol[i] + "," + invRow[j]);
                }
            }
            


            Debug.WriteLine(" ");
            Debug.WriteLine(invCol[2] + "," + invRow[3]);

        }

        public void MovePiece(int prevCol, int prevRow, int nextCol, int nextRow)
        {
            try
            {
                Square prevSqr = squares[prevCol, prevRow];
                Square nextSqr = squares[nextCol, nextRow];
                Piece piece = prevSqr.CurrentPiece;

                if (prevSqr.CurrentPiece.Type == 6)
                {
                    Pawn pawn = (Pawn)prevSqr.CurrentPiece;
                    pawn.IsFirstMove = false;
                    prevSqr.CurrentPiece = pawn;
                }

                Image image = prevSqr.CurrentPiece.Image;

                if(squares[nextCol, nextRow].CurrentPiece != null)
                {
                    if (squares[nextCol, nextRow].CurrentPiece.Type == 1)
                    {
                        ShowDialog("Ha terminado el juego");
                    }
                }
                

                squares[nextCol, nextRow].CurrentPiece = prevSqr.CurrentPiece;
                squares[prevCol, prevRow].Content = null;
                squares[nextCol, nextRow].Content = image;
                squares[prevCol, prevRow].CurrentPiece = null;

                if(gameState == GameState.BlackPlaying)
                {
                    gameState = GameState.WhitePlaying;
                }
                else
                {
                    gameState = GameState.BlackPlaying;
                }

                

                SendMessage("{'id':"+myId + ",'type':1,'prevCol':" + invCol[prevCol] + ",'prevRow':" + invRow[prevRow] + ",'nextCol':" + invCol[nextCol] + ",'nextRow':" + invRow[nextRow] + "}");

                string movement = "from: " + prevCol +","+nextCol+" to: " +nextCol+"," + nextCol;

                //AddMovement(movement);
                movements.Items.Add(movement);
                SetDefaultBackgrounds();
            }
            catch (System.IndexOutOfRangeException)
            {
                Debug.WriteLine("A incorrected movement was detected");
            }


        }

        /*public void AddMovement(string movement)
        {
            Movements movements = 
            movementss.AddMovement(movement);
        }*/

        public void crearMovements()
        {

        }

        public void PaintMoves(Square sqr, Piece piece, int type, int column, int row)
        {
            switch (type)
            {
                case 1:
                    PaintRookOrBishopMoves(column + 1, row + 1);
                    PaintRookOrBishopMoves(column + 1, row - 1);
                    PaintRookOrBishopMoves(column + 1, row);
                    PaintRookOrBishopMoves(column, row + 1);
                    PaintRookOrBishopMoves(column, row - 1);
                    PaintRookOrBishopMoves(column - 1, row);
                    PaintRookOrBishopMoves(column - 1, row + 1);
                    PaintRookOrBishopMoves(column - 1, row - 1);
                    break;
                case 3:
                    for (int i = row + 1; i <= 7; i++)
                    {
                        if (PaintRookOrBishopMoves(column, i))
                        {
                            break;
                        }
                    }
                    for (int i = row - 1; i >= 0; i--)
                    {
                        if (PaintRookOrBishopMoves(column, i))
                        {
                            break;
                        }
                    }
                    for (int i = column - 1; i >= 0; i--)
                    {
                        if (PaintRookOrBishopMoves(i, row))
                        {
                            break;
                        }
                    }
                    for (int i = column + 1; i <= 7; i++)
                    {
                        if (PaintRookOrBishopMoves(i, row))
                        {
                            break;
                        }
                    }
                    break;
                case 4:
                    int column2;
                    column2 = column + 1;

                    for (int i = row - 1; (i >= 0) && (column2 <= 7); i--)
                    {
                        if (PaintRookOrBishopMoves(column2, i))
                        {
                            break;
                        }
                        column2++;

                    }
                    column2 = column + 1;
                    for (int i = row + 1; (i <= 7) && (column2 <= 7); i++)
                    {
                        if (PaintRookOrBishopMoves(column2, i))
                        {
                            break;
                        }
                        column2++;
                    }
                    column2 = column - 1;
                    for (int i = row - 1; (i >= 0) && (column2 >= 0); i--)
                    {
                        if (PaintRookOrBishopMoves(column2, i))
                        {
                            break;
                        }
                        column2--;
                    }
                    column2 = column - 1;
                    for (int i = row + 1; (i <= 7) && (column2 >= 0); i++)
                    {
                        if (PaintRookOrBishopMoves(column2, i))
                        {
                            break;
                        }
                        column2--;
                    }
                    break;
                case 5:
                    PaintKnightMoves(column - 1, row - 2);
                    PaintKnightMoves(column + 1, row + 2);
                    PaintKnightMoves(column - 1, row + 2);
                    PaintKnightMoves(column - 2, row + 1);
                    PaintKnightMoves(column - 2, row - 1);
                    PaintKnightMoves(column + 2, row - 1);
                    PaintKnightMoves(column + 2, row + 1);
                    PaintKnightMoves(column + 1, row - 2);
                    break;
                case 6:
                    Pawn pawn = (Pawn)piece;

                    if (row - 1 >= 0)
                    {
                        if (pawn.IsFirstMove)
                        {
                            squares[column, row - 1].Background = hoverColor;
                            squares[column, row - 2].Background = hoverColor;
                        }
                        else
                        {
                            if (squares[column, row - 1].CurrentPiece == null)
                            {
                                squares[column, row - 1].Background = hoverColor;
                            }


                            if (column - 1 >= 0)
                            {
                                if (squares[column - 1, row - 1].CurrentPiece != null)
                                {
                                    if (squares[column - 1, row - 1].CurrentPiece.Color == Color.Black)
                                    {
                                        squares[column - 1, row - 1].Background = hoverColor;
                                    }
                                }
                            }

                            if (column + 1 <= 7)
                            {
                                if (squares[column + 1, row - 1].CurrentPiece != null)
                                {
                                    if (squares[column + 1, row - 1].CurrentPiece.Color == Color.Black)
                                    {
                                        squares[column + 1, row - 1].Background = hoverColor;
                                    }
                                }
                            }

                        }
                    }
                    break;
            }
            currentSquare = sqr;
        }

        public void PaintKnightMoves(int column, int row)
        {
            try
            {
                if (squares[column, row].CurrentPiece == null)
                {
                    squares[column, row].Background = hoverColor;
                }
                else if (squares[column, row].CurrentPiece.Color == Color.Black)
                {
                    squares[column, row].Background = hoverColor;
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Debug.WriteLine("A incorrected movement was detected");
            };


        }

        public bool PaintRookOrBishopMoves(int column, int row)
        {
            try
            {
                if (squares[column, row].CurrentPiece != null)
                {
                    if (squares[column, row].CurrentPiece.Color == Color.White)
                    {
                        return true;
                    }
                    else
                    {
                        squares[column, row].Background = hoverColor;
                        return true;
                    }
                }
                else
                {
                    squares[column, row].Background = hoverColor;
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Debug.WriteLine("A incorrected movement was detected");
            }


            return false;
        }

        public void SetDefaultBackgrounds()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squares[i, j].Background = squares[i, j].DefaultBrush;
                }
            }

        }

        private void SendMessage(string msg)
        {
            _hub.Invoke("SendAction", msg);
        }

        private void ShowDialog(string resultado)
        {
            var dialog = new MessageDialog(resultado);
            var result = dialog.ShowAsync();
        }

        public void ConfigureHub()
        {
            string url = @"http://xpesdcompany-001-site3.ctempurl.com/apis/ChessNotificationsService/signalr/hubs";
            var connection = new HubConnection(url);
            _hub = connection.CreateHubProxy("chesshub");

            _hub.On("ActionRequested", result =>
            {
                var x = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    try
                    {
                        var message = JObject.Parse(result);
                        CheckMessage(message);
                    }
                    catch (JsonReaderException)
                    {
                        Debug.Write("A incorrect message was detected \n");
                    }

                });
            });
            connection.Start().Wait();
        }

        public void CheckMessage(dynamic message)
        {
            int messageType;

            if (message["type"] != null && message["id"]!=myId)
            {
                messageType = message["type"];
                if (messageType == 1 && message["prevCol"] != null && message["prevRow"] != null && message["nextCol"] != null && message["nextRow"] != null)
                {
                    int prevCol = message["prevCol"];
                    int prevRow = message["prevRow"];
                    int nextCol = message["nextCol"];
                    int nextRow = message["nextRow"];

                    if (prevCol < 8 && prevCol >= 0 && prevCol < 8 && prevCol >= 0 && squares[prevCol, prevRow].CurrentPiece != null)
                    {
                        MovePiece(prevCol, prevRow, nextCol, nextRow);
                        
                    }
                }
            }
            else
            {
                Debug.WriteLine("A unrecognizable message was detected or you moved");
            }
        }
    }
}