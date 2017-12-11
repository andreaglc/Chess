using Chess.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Diagnostics;
using Windows.UI;


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
        SolidColorBrush transparentColor;

        Piece queenW;
        Piece kingW;
        Piece rookW;
        Piece rookW2;
        Piece bishopW;
        Piece bishopW2;
        Piece horseW;
        Piece horseW2;
        Piece pawnW;
        Piece pawnW2;
        Piece pawnW3;
        Piece pawnW4;
        Piece pawnW5;
        Piece pawnW6;
        Piece pawnW7;
        Piece pawnW8;



        private IHubProxy _hub;

        public MainPage()
        {
            this.InitializeComponent();
            InitializeBrushes();
            InitializeChessBoard();
            putPieces();


            ConfigureHub();
        }

        public void InitializeBrushes()
        {
            blueColor = new SolidColorBrush(Color.FromArgb(255,37,118,174));
            whiteColor = new SolidColorBrush(Color.FromArgb(255,239,247,255));
            LayoutRoot.BorderBrush = blueColor;
            transparentColor = new SolidColorBrush(Colors.Transparent);
            Image queenImg = new Image();
            queenImg.Source = new BitmapImage(new Uri(BaseUri, "/Assets/queenW.png"));
            Image kingImg = new Image();
            kingImg.Source = new BitmapImage(new Uri(BaseUri, "/Assets/kingW.png"));
            Image rookWImg = new Image();
            rookWImg.Source = new BitmapImage(new Uri(BaseUri, "/Assets/rookW.png"));
            Image rookW2Img = new Image();
            rookW2Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/rookW.png"));
            Image bishopWImg = new Image();
            bishopWImg.Source = new BitmapImage(new Uri(BaseUri, "/Assets/bishopW.png"));
            Image bishopW2Img = new Image();
            bishopW2Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/bishopW.png"));
            Image horseWImg = new Image();
            horseWImg.Source = new BitmapImage(new Uri(BaseUri, "/Assets/horseW.png"));
            Image horseW2Img = new Image();
            horseW2Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/horseW.png"));
            Image pawnWImg = new Image();
            pawnWImg.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));
            Image pawnW2Img = new Image();
            pawnW2Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));
            Image pawnW3Img = new Image();
            pawnW3Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));
            Image pawnW4Img = new Image();
            pawnW4Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));
            Image pawnW5Img = new Image();
            pawnW5Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));
            Image pawnW6Img = new Image();
            pawnW6Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));
            Image pawnW7Img = new Image();
            pawnW7Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));
            Image pawnW8Img = new Image();
            pawnW8Img.Source = new BitmapImage(new Uri(BaseUri, "/Assets/pawnW.png"));

            queenW = new Piece
            {
                Background = transparentColor,
                Content = queenImg,
                Type = 2
            };

            kingW = new Piece
            {
                Background = transparentColor,
                Content = kingImg,
                Type = 1
            };

            rookW = new Piece
            {
                Background = transparentColor,
                Content = rookWImg,
                Type = 3
            };

            rookW2 = new Piece
            {
                Background = transparentColor,
                Content = rookW2Img,
                Type = 3

            };

            bishopW = new Piece
            {
                Background = transparentColor,
                Content = bishopWImg,
                Type = 4
            };

            bishopW2 = new Piece
            {
                Background = transparentColor,
                Content = bishopW2Img,
                Type = 4
            };

            horseW = new Piece
            {
                Background = transparentColor,
                Content = horseWImg,
                Type = 5
            };

            horseW2 = new Piece
            {
                Background = transparentColor,
                Content = horseW2Img,
                Type = 5
            };

            pawnW = new Piece
            {
                Background = transparentColor,
                Content = pawnWImg,
                Type = 6
            };
            pawnW2 = new Piece
            {
                Background = transparentColor,
                Content = pawnW2Img,
                Type = 6
            };
            pawnW3 = new Piece
            {
                Background = transparentColor,
                Content = pawnW3Img,
                Type = 6
            };
            pawnW4 = new Piece
            {
                Background = transparentColor,
                Content = pawnW4Img,
                Type = 6
            };
            pawnW5 = new Piece
            {
                Background = transparentColor,
                Content = pawnW5Img,
                Type = 6
            };
            pawnW6 = new Piece
            {
                Background = transparentColor,
                Content = pawnW6Img,
                Type = 6
            };
            pawnW7 = new Piece
            {
                Background = transparentColor,
                Content = pawnW7Img,
                Type = 6
            };
            pawnW8 = new Piece
            {
                Background = transparentColor,
                Content = pawnW8Img,
                Type = 6
            };
            queenW.Click += new RoutedEventHandler(MovePiece);
        }

        /*ImageBrush woodBrush;
        ImageBrush blackWoodBrush;
        Image woodImg;
        Image blackWoodImg;
          woodBrush = new ImageBrush();
            blackWoodBrush = new ImageBrush();
            woodImg = new Image();
            blackWoodImg = new Image();
            woodImg.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/wood.jpg"));
            blackWoodImg.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/woodb.jpg"));
            woodBrush.ImageSource = woodImg.Source;
            blackWoodBrush.ImageSource = blackWoodImg.Source;
            */

        public void InitializeChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var rec = new Button
                    {
                        Width = 100,
                        Height = 100,
                    };

                    if (i % 2 == 0 && j % 2 == 0 || i % 2 != 0 && j % 2 != 0)
                    {
                        rec.Background = whiteColor;

                    }
                    else
                    {
                        rec.Background = blueColor;
                    }
                    LayoutRoot.Children.Add(rec);
                    Grid.SetColumn(rec, i);
                    Grid.SetRow(rec, j);

                }
            }

            

        }

        public void putPieces()
        {
            //king
            LayoutRoot.Children.Add(kingW);
            Grid.SetColumn(kingW, 4);
            Grid.SetRow(kingW, 7);

            //queen
            LayoutRoot.Children.Add(queenW);
            Grid.SetColumn(queenW, 3);
            Grid.SetRow(queenW, 7);

            //rookies
            LayoutRoot.Children.Add(rookW);
            LayoutRoot.Children.Add(rookW2);
            Grid.SetColumn(rookW, 0);
            Grid.SetRow(rookW, 7);
            Grid.SetColumn(rookW2, 7);
            Grid.SetRow(rookW2, 7);

            //bishops
            LayoutRoot.Children.Add(bishopW);
            LayoutRoot.Children.Add(bishopW2);
            Grid.SetColumn(bishopW, 2);
            Grid.SetRow(bishopW, 7);
            Grid.SetColumn(bishopW2, 5);
            Grid.SetRow(bishopW2, 7);

            //horses
            LayoutRoot.Children.Add(horseW);
            LayoutRoot.Children.Add(horseW2);
            Grid.SetColumn(horseW, 1);
            Grid.SetRow(horseW, 7);
            Grid.SetColumn(horseW2, 6);
            Grid.SetRow(horseW2, 7);

            //pawns
            LayoutRoot.Children.Add(pawnW);
            LayoutRoot.Children.Add(pawnW2);
            LayoutRoot.Children.Add(pawnW3);
            LayoutRoot.Children.Add(pawnW4);
            LayoutRoot.Children.Add(pawnW5);
            LayoutRoot.Children.Add(pawnW6);
            LayoutRoot.Children.Add(pawnW7);
            LayoutRoot.Children.Add(pawnW8);
            Grid.SetColumn(pawnW, 0);
            Grid.SetRow(pawnW, 6);
            Grid.SetColumn(pawnW2, 1);
            Grid.SetRow(pawnW2, 6);
            Grid.SetColumn(pawnW3, 2);
            Grid.SetRow(pawnW3, 6);
            Grid.SetColumn(pawnW4, 3);
            Grid.SetRow(pawnW4, 6);
            Grid.SetColumn(pawnW5, 4);
            Grid.SetRow(pawnW5, 6);
            Grid.SetColumn(pawnW6, 5);
            Grid.SetRow(pawnW6, 6);
            Grid.SetColumn(pawnW7, 6);
            Grid.SetRow(pawnW7, 6);
            Grid.SetColumn(pawnW8, 7);
            Grid.SetRow(pawnW8, 6);
            


        }


        private void SendMsg(object sender, RoutedEventArgs e)
        {
            //SendMessage(message.Text.ToString());
        }

        void MovePiece(object sender, RoutedEventArgs e)
        {
            if(sender.Equals(queenW)){
                Debug.WriteLine("It's the queen");
            }
        }

        private void SendMessage(string msg)
        {
            _hub.Invoke("SendAction", msg);
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
                    //messages.Items.Add(result);
                });
            });
            connection.Start().Wait();
        }
    }
}