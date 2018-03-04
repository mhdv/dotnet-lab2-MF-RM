using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        const int gridHeight = 30;
        const int gridWidth = 30;
        Rectangle[,] rectangles = new Rectangle[gridHeight, gridWidth];

        private void timer1_Tick(object sender, EventArgs e)
        {
            gameLogic();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            for(int i=0; i<gridHeight; ++i)
            {
                for(int j=0; j<gridWidth; ++j)
                {
                    Rectangle r = new Rectangle();
                    r.Height = playGrid.Height / gridHeight - 2.0;
                    r.Width = playGrid.Width / gridWidth - 2.0;
                    r.Fill = Brushes.Gray;
                    playGrid.Children.Add(r);
                    Canvas.SetLeft(r, j * playGrid.ActualWidth / gridWidth);
                    Canvas.SetTop(r, i * playGrid.Width / gridWidth);
                    r.MouseDown += R_MouseDown;
                    rectangles[i, j] = r;
                }
            }
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(((Rectangle)sender).Fill == Brushes.Gray)
            {
                ((Rectangle)sender).Fill = Brushes.Red;
            }
            else
            {
                ((Rectangle)sender).Fill = Brushes.Gray;
            }
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            gameLogic();
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        private void timerBtn_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 45);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            gameLogic();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer != null) dispatcherTimer.Stop();
        }

        int[,] lifeGrid = new int[gridHeight, gridWidth];
        int aliveBy = 0;

        public void gameLogic()
        {
            for (int i = 0; i < gridHeight; ++i)
            {
                for (int j = 0; j < gridWidth; ++j)
                {
                    if (rectangles[i, j] == null) return;
                    int iU = i - 1;
                    int iD = i + 1;
                    int jL = j - 1;
                    int jR = j + 1;
                    if (iU < 0) iU = gridHeight - 1;
                    if (iD >= gridHeight) iD = 0;
                    if (jL < 0) jL = gridWidth - 1;
                    if (jR >= gridWidth) jR = 0;

                    if (rectangles[iU, jL].Fill == Brushes.Red) ++aliveBy;
                    if (rectangles[iU, j].Fill == Brushes.Red) ++aliveBy;
                    if (rectangles[iU, jR].Fill == Brushes.Red) ++aliveBy;
                    if (rectangles[i, jL].Fill == Brushes.Red) ++aliveBy;
                    if (rectangles[i, jR].Fill == Brushes.Red) ++aliveBy;
                    if (rectangles[iD, jL].Fill == Brushes.Red) ++aliveBy;
                    if (rectangles[iD, j].Fill == Brushes.Red) ++aliveBy;
                    if (rectangles[iD, jR].Fill == Brushes.Red) ++aliveBy;

                    lifeGrid[i, j] = aliveBy;
                    aliveBy = 0;
                }
            }
            for(int i = 0; i<gridHeight; ++i)
            {
                for(int j = 0; j<gridWidth; ++j)
                {
                    if (lifeGrid[i, j] == 3) rectangles[i, j].Fill = Brushes.Red;
                    if ((lifeGrid[i, j] != 2) && (lifeGrid[i, j] != 3)) rectangles[i, j].Fill = Brushes.Gray;
                }
            }
        }

    }
}
