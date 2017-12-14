using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FormativeAssessment
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// red will be match color and positions, orange only position
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        //array of color we use 0 for white
        String[] colorsArray = { "White", "Blue", "Red", "Yellow", "Pink", "Purple", "Turquoise", "Viole" };
        public static ImageBrush s; /*s = new ImageBrush//()
        {
            ImageSource = new BitmapImage(new Uri(this.BaseUri, @"path.jpg")),
            Stretch= Stretch.Uniform
        };*/
        public MainPage()
        {
            this.InitializeComponent();

            Ellipse piece = new Ellipse();
            /* piece.Width = 100;
             piece.Height = 100;
             piece.Fill = new SolidColorBrush(Colors.Red);

             MainGrid.Children.Add(piece);

             piece.Tapped += Piece_Tapped;
             */
            CreateBoard();
            GenerateColors();

            s = new ImageBrush//()
            {
                ImageSource = new BitmapImage(new Uri(this.BaseUri, @"path.jpg")),
                Stretch = Stretch.Uniform
            };
            MainGrid.Background = s;

        }

        int[] winingColors = new int[4];

        private void GenerateColors()
        {
            //  int random = (int)(random() * 6 + 1);
            Random random = new Random();
            int randonInt = random.Next(0, 7);
            //Debug.WriteLine(randonInt);
            winingColors[0] = random.Next(0, 7);
            do
            {
                winingColors[1] = random.Next(0, 7);
            } while (winingColors[0] == winingColors[1]);
            do
            {
                winingColors[2] = random.Next(0, 7);
            } while (winingColors[0] == winingColors[2] || winingColors[1] == winingColors[2]);
            do
            {
                winingColors[3] = random.Next(0, 7);
            } while (winingColors[0] == winingColors[3] || winingColors[1] == winingColors[3] || winingColors[2] == winingColors[3]);



        }

        private void Piece_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("circle tapped");

        }

        private void CreateBoard()
        {
            StackPanel BackSP = new StackPanel();
            BackSP.Orientation = Orientation.Vertical;
            BackSP.Background = new SolidColorBrush(Colors.Brown);

            //top grdi
            Grid topGrid = new Grid();
            topGrid.Margin = new Thickness(5);
            topGrid.Background = new SolidColorBrush(Colors.Brown);
            topGrid.Width = 250;
            topGrid.Height = 50;
            for (int j = 0; j < 4; j++)
            {
                ColumnDefinition myColum = new ColumnDefinition();
                topGrid.ColumnDefinitions.Add(myColum);
            }

            for (int i = 1; i < 5; i++)
            {
                Ellipse piece = new Ellipse();
                piece.Width = 20;
                piece.Height = 20;

                piece.Fill = new SolidColorBrush(Colors.White);
                piece.Tapped += Piece_Tapped1;

                piece.SetValue(Grid.RowProperty, 0);
                piece.SetValue(Grid.ColumnProperty, i);

                topGrid.Children.Add(piece);
            }
            BackSP.Children.Add(topGrid);

            for (int k = 0; k < 10; k++)
            {
                //create grid for put piece
                Grid PieceHolders = new Grid();
                PieceHolders.Margin = new Thickness(5);
                PieceHolders.Background = new SolidColorBrush(Colors.Brown);
                PieceHolders.Width = 250;
                PieceHolders.Height = 50;


                PieceHolders.BorderBrush = new SolidColorBrush(Colors.Black);
                PieceHolders.BorderThickness = new Thickness(5);
                /*
                RowDefinition myRow = new RowDefinition();
                PieceHolders.RowDefinitions.Add(myRow);*/
                for (int j = 0; j < 5; j++)
                {
                    ColumnDefinition myColum = new ColumnDefinition();
                    PieceHolders.ColumnDefinitions.Add(myColum);
                }

                //create 2x2 grid for result
                Grid resultGrid = new Grid();
                resultGrid.BorderBrush = new SolidColorBrush(Colors.Black);

                resultGrid.BorderThickness = new Thickness(3);
                for (int i = 0; i < 2; i++)
                {
                    ColumnDefinition myColum = new ColumnDefinition();
                    resultGrid.ColumnDefinitions.Add(myColum);
                    RowDefinition myRow = new RowDefinition();
                    resultGrid.RowDefinitions.Add(myRow);
                }
                int showRelustPos = 0;
                for (int m = 0; m < 2; m++)
                {
                    for (int h = 0; h < 2; h++)
                    {
                        Ellipse showResultEllipse = new Ellipse();
                        showResultEllipse.Width = 5;
                        showResultEllipse.Height = 5;
                        showResultEllipse.Fill = new SolidColorBrush(Colors.Black);
                        showResultEllipse.Name = "showResult" + k + "_" + ++showRelustPos;
                        Debug.WriteLine(showResultEllipse.Name);
                        showResultEllipse.SetValue(Grid.RowProperty, h);
                        showResultEllipse.SetValue(Grid.ColumnProperty, m);
                        resultGrid.Children.Add(showResultEllipse);
                    }
                }
                resultGrid.SetValue(Grid.RowProperty, 0);
                resultGrid.SetValue(Grid.ColumnProperty, 0);
                PieceHolders.Children.Add(resultGrid);
                // add pieces to grid
                for (int i = 1; i < 5; i++)
                {
                    Ellipse piece = new Ellipse();
                    piece.Width = 20;
                    piece.Height = 20;
                    piece.Fill = new SolidColorBrush(Colors.White);
                    piece.Tapped += Piece_Tapped1;

                    piece.Name = "piece" + i + "_" + k;

                    piece.SetValue(Grid.RowProperty, k);

                    piece.SetValue(Grid.ColumnProperty, i);

                    piece.Tapped += Piece_Tapped2;

                    PieceHolders.Children.Add(piece);
                }
                BackSP.Children.Add(PieceHolders);
            }

            Button nextStep = new Button();
            nextStep.Content = "NEXT";
            nextStep.Tapped += NextStep_Tapped;
            nextStep.Background = new SolidColorBrush(Colors.Yellow);
            BackSP.Children.Add(nextStep);

            MainGrid.Children.Add(BackSP);
        }//create board

        private void Piece_Tapped1(object sender, TappedRoutedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void NextStep_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int ColorMatchCount = 0;
            int ExactMatchCount = 0;
            int NoExactMatchCount = 0;

            Debug.WriteLine("game:");
            //check if there is an exact match

            for (int i = 0; i < 4; i++)
            {   //same color same position
                if (clicksOnPiece[i] == winingColors[i])
                {

                    Debug.WriteLine("ExactMatach:");

                    ColorMatchCount++;
                    ExactMatchCount++;

                }
                else
                {
                    //only same color
                    for (int m = 0; m < 4; m++)
                    {
                        if (clicksOnPiece[i] == winingColors[m])
                        {
                            ColorMatchCount++;
                            NoExactMatchCount++;
                            ;
                        }
                    }
                }
            }

            //no extact much
            //show match in table
            int position = 1;
            if (ColorMatchCount > 0)
            {
                for (int i = 0; i < ExactMatchCount; i++)
                {
                    Ellipse resultEllipse = (Ellipse)FindName("showResult" + actualRow + "_" + position);
                    resultEllipse.Fill = new SolidColorBrush(Colors.Red);
                    position++;
                }
                for (int i = 0; i < NoExactMatchCount; i++)
                {
                    Ellipse resultEllipse = (Ellipse)FindName("showResult" + actualRow + "_" + position);
                    resultEllipse.Fill = new SolidColorBrush(Colors.Orange);
                    position++;
                }
            }


            actualRow--;
            clicksOnPiece[0] = 0;
            clicksOnPiece[1] = 0;
            clicksOnPiece[2] = 0;
            clicksOnPiece[3] = 0;

        }

        int[] clicksOnPiece = { 0, 0, 0, 0 };
        int actualRow = 9;
        private void Piece_Tapped2(object sender, TappedRoutedEventArgs e)
        {
            Ellipse actualPiece = (Ellipse)sender;
            // Debug.WriteLine(actualPiece.Name.ToString());

            //int actualRow = Name.Substring(6,1);
            // Debug.WriteLine(actualPiece.Name.Substring(7, 1));
            int rowPosition = Convert.ToInt32(actualPiece.Name.Substring(7, 1));
            int columPosition = Convert.ToInt32(actualPiece.Name.Substring(5, 1)) - 1;
            // Debug.WriteLine(columPosition);
            if (rowPosition == actualRow)
            {
                switch (clicksOnPiece[columPosition])
                {
                    case 0:
                        actualPiece.Fill = new SolidColorBrush(Colors.Blue);
                        clicksOnPiece[columPosition]++;
                        break;
                    case 1:
                        actualPiece.Fill = new SolidColorBrush(Colors.Purple);
                        clicksOnPiece[columPosition]++;
                        break;
                    case 2:
                        actualPiece.Fill = new SolidColorBrush(Colors.Green);
                        clicksOnPiece[columPosition]++;
                        break;
                    case 3:
                        actualPiece.Fill = new SolidColorBrush(Colors.Yellow);
                        clicksOnPiece[columPosition]++;
                        break;
                    case 4:
                        actualPiece.Fill = new SolidColorBrush(Colors.Pink);
                        clicksOnPiece[columPosition]++;
                        break;
                    case 5:
                        actualPiece.Fill = new SolidColorBrush(Colors.Magenta);
                        clicksOnPiece[columPosition]++;
                        break;
                    case 6:
                        actualPiece.Fill = new SolidColorBrush(Colors.Turquoise);
                        clicksOnPiece[columPosition] = 0;
                        break;

                }


            }
        }

       
    }
}
