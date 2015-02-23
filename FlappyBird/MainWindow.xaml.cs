using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer pipeAnimationTimer = null;
        DispatcherTimer birdAnimationTimer = null;

        Rectangle[] pipe = new Rectangle[3];
        Rectangle[] hole = new Rectangle[3];
        bool[] holeDown = { true, true, false };

        int AddHeight, score;
        Random randomHole = new Random();

        public MainWindow()
        {
            InitializeComponent();

            birdAnimationTimer = new DispatcherTimer(DispatcherPriority.Render);
            birdAnimationTimer.Interval = TimeSpan.FromMilliseconds(300);
            birdAnimationTimer.Tick += birdAnimationTimer_Tick;
            birdAnimationTimer.Start();
        }

        void birdAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (FlappyBird.Source != null && FlappyBird.Source.ToString()[FlappyBird.Source.ToString().Length - 5] == '1')
            {
                FlappyBird.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\bird2.png"));
            }
            else
            {
                FlappyBird.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Resources\bird1.png"));
            }
        }

        private void InitializeGame()
        {
            AddHeight = 0;
            score = 0;

            pipe[0] = Pipe1;
            pipe[1] = Pipe2;
            pipe[2] = Pipe3;

            hole[0] = Hole1;
            hole[1] = Hole2;
            hole[2] = Hole3;

            Instruction.Visibility = System.Windows.Visibility.Hidden;
            Brand.Visibility = System.Windows.Visibility.Hidden;

            pipeAnimationTimer = new DispatcherTimer(DispatcherPriority.Send);
            pipeAnimationTimer.Interval = TimeSpan.FromMilliseconds(30);
            pipeAnimationTimer.Tick += pipeAnimationTimer_Tick;
            pipeAnimationTimer.Start();
        }

        void pipeAnimationTimer_Tick(object sender, EventArgs e)
        {
            var flappyBirdMargin = FlappyBird.Margin;
            if (AddHeight > 0)
            {
                AddHeight--;
                flappyBirdMargin.Top += 4;
            }
            else
            {
                flappyBirdMargin.Top -= 3;
            }

            if (flappyBirdMargin.Top <= 0)
            {
                Result.Content = "Game Over!!" + "\n\nScore : " + score.ToString("000");
                Result.Visibility = System.Windows.Visibility.Visible;
                pipeAnimationTimer.Stop();
                if (BackgroundSound.IsEnabled == true)
                {
                    BackgroundSound.Stop();
                }
                BackgroundSound.Source = new Uri(@"Resources\hit.mp3", UriKind.Relative);
                BackgroundSound.Play();
            }

            FlappyBird.Margin = flappyBirdMargin;
            for (int i = 0; i < 3; i++)
            {
                var pipeMargin = pipe[i].Margin;
                pipeMargin.Left = (pipeMargin.Left + 2.5) % 525;

                pipe[i].Margin = pipeMargin;
                pipeMargin.Top = hole[i].Margin.Top;
                if (pipeMargin.Left == 0)
                {
                    pipe[i].Visibility = System.Windows.Visibility.Visible;
                    pipeMargin.Top = randomHole.Next(70, 240);
                }

                if(score > 5)
                {
                    if(holeDown[i] == true)
                    {
                        if (pipeMargin.Top > 160)
                        {
                            pipeMargin.Top -= 1;
                        }
                        else
                        {
                            holeDown[i] = false;
                        }
                    }
                    else
                    {
                        if (pipeMargin.Top < 240)
                        {
                            pipeMargin.Top += 1;
                        }
                        else
                        {
                            holeDown[i] = true;
                        }
                    }
                }

                hole[i].Margin = pipeMargin;

                if (pipe[i].Visibility == System.Windows.Visibility.Visible)
                {
                    if (pipeMargin.Left < FlappyBird.Margin.Left + 30 && pipeMargin.Left + 55 > FlappyBird.Margin.Left && !(pipeMargin.Top <= FlappyBird.Margin.Top + 2 && pipeMargin.Top + 80 >= FlappyBird.Margin.Top + 30))
                    {
                        Result.Content = "Game Over!!" + "\n\nScore : " + score.ToString("000");
                        Result.Visibility = System.Windows.Visibility.Visible;
                        pipeAnimationTimer.Stop();
                        if (BackgroundSound.IsEnabled == true)
                        {
                            BackgroundSound.Stop();
                        }
                        BackgroundSound.Source = new Uri(@"Resources\hit.mp3", UriKind.Relative);
                        BackgroundSound.Play();
                    }
                    else
                    {
                        if (pipeMargin.Left == 205)
                        {
                            Score.Content = "Score : " + (++score).ToString("000");
                        }
                    }
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if (FlappyBird.Margin.Top < 370)
                    {
                        AddHeight = 10;
                        if (pipeAnimationTimer == null)
                        {
                            InitializeGame();
                        }
                        if (pipeAnimationTimer.IsEnabled == true)
                        {
                            if (BackgroundSound.IsEnabled == true)
                            {
                                BackgroundSound.Stop();
                            }
                            BackgroundSound.Source = new Uri(@"Resources\Camera_Shutter.wav", UriKind.Relative);
                            BackgroundSound.Play();
                        }
                    }
                    break;
                case Key.N:
                    new MainWindow().Show();
                    this.Close();
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "New Game":
                    new MainWindow().Show();
                    this.Close();
                    break;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FlappyBird.Margin.Top < 370)
            {
                AddHeight = 10;
            }
            if (pipeAnimationTimer == null)
            {
                InitializeGame();
            }
            if (pipeAnimationTimer.IsEnabled == true)
            {
                if (BackgroundSound.IsEnabled == true)
                {
                    BackgroundSound.Stop();
                }
                BackgroundSound.Source = new Uri(@"Resources\Camera_Shutter.wav", UriKind.Relative);
                BackgroundSound.Play();
            }
        }
    }
}
