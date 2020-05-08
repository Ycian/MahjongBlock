using System;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;
using System.Reflection;
using System.IO;

namespace DragNDrop
{
    public partial class GamePage : ContentPage
    {
        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        // game board
        static string[,] board = new string[9, 9];
        static int totalPoints = 0;

        public GamePage()
        {
            InitializeComponent();
        }

        //private void Load(string file)
        //{

        //}

        string randomMahjong()
        {
            var rand = new Random();
            int type = (int)(3 * rand.NextDouble());
            if (type == 0)
            {
                return "mahjong/dot" + (rand.Next(9) + 1);
            }
            else if (type == 1)
            {
                return "mahjong/bamboo" + (rand.Next(9) + 1);
            }
            else
            {
                return "mahjong/char" + (rand.Next(9) + 1);
            }

        }

        // if one row has 3 same mahjong, call it "9 kong"
        bool isRowPong(int i, int j)
        {
            string mid = board[i, j];
            string prev = board[i, j - 1];
            string next = board[i, j + 1];

            string tempMid = mid.Substring(mid.Length - 2, 1);
            string tempPrev = prev.Substring(prev.Length - 2, 1);
            string tempNext = next.Substring(next.Length - 2, 1);

            // if same type
            if (tempMid.Equals(tempPrev) && tempMid.Equals(tempNext))
            {
                mid = mid.Substring(mid.Length - 1);
                prev = prev.Substring(prev.Length - 1);
                next = next.Substring(next.Length - 1);

                if (Int32.Parse(mid) == Int32.Parse(prev) &&
                    Int32.Parse(mid) == Int32.Parse(next))
                {
                    string file = "pong.m4a";
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    String ns = "DragNDrop";
                    Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
                    player.Load(audioStream);
                    player.Play();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        // if one column has 9 same mahjong, call it "9 kong"
        bool isColPong(int i, int j)
        {
            string mid = board[i, j];
            string top = board[i - 1, j];
            string bot = board[i + 1, j];

            string tempMid = mid.Substring(mid.Length - 2, 1);
            string tempTop = top.Substring(top.Length - 2, 1);
            string tempBot = bot.Substring(bot.Length - 2, 1);

            // if same type
            if (tempMid.Equals(tempTop) && tempMid.Equals(tempBot))
            {
                mid = mid.Substring(mid.Length - 1);
                top = top.Substring(top.Length - 1);
                bot = bot.Substring(bot.Length - 1);

                if (Int32.Parse(mid) == Int32.Parse(top) &&
                    Int32.Parse(mid) == Int32.Parse(bot))
                {
                    string file = "pong.m4a";
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    String ns = "DragNDrop";
                    Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
                    player.Load(audioStream);
                    player.Play();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // check if meets chow in row
        bool isRowChow(int i, int j)
        {
            string mid = board[i, j];
            string prev = board[i, j - 1];
            string next = board[i, j + 1];

            string tempMid = mid.Substring(mid.Length - 2, 1);
            string tempPrev = prev.Substring(prev.Length - 2, 1);
            string tempNext = next.Substring(next.Length - 2, 1);

            // if same type
            if (tempMid.Equals(tempPrev) && tempMid.Equals(tempNext))
            {
                mid = mid.Substring(mid.Length - 1);
                prev = prev.Substring(prev.Length - 1);
                next = next.Substring(next.Length - 1);

                if (Int32.Parse(mid) == Int32.Parse(prev) + 1 &&
                    Int32.Parse(mid) == Int32.Parse(next) - 1)
                {
                    string file = "chow.m4a";
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    String ns = "DragNDrop";
                    Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
                    player.Load(audioStream);
                    player.Play();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // check if meets chow in column
        bool isColChow(int i, int j)
        {
            string mid = board[i, j];
            string top = board[i - 1, j];
            string bot = board[i + 1, j];

            string tempMid = mid.Substring(mid.Length - 2, 1);
            string tempTop = top.Substring(top.Length - 2, 1);
            string tempBot = bot.Substring(bot.Length - 2, 1);

            // if same type
            if (tempMid.Equals(tempTop) && tempMid.Equals(tempBot))
            {
                mid = mid.Substring(mid.Length - 1);
                top = top.Substring(top.Length - 1);
                bot = bot.Substring(bot.Length - 1);

                if (Int32.Parse(mid) == Int32.Parse(top) + 1 &&
                    Int32.Parse(mid) == Int32.Parse(bot) - 1)
                {
                    string file = "chow.m4a";
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    String ns = "DragNDrop";
                    Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
                    player.Load(audioStream);
                    player.Play();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // clear the row in screen and the row of board
        void clearRowPong(int i, int j)
        {
            var target = (Image)grid.FindByName("b" + i + j);
            target.Source = "";
            target = (Image)grid.FindByName("b" + i + (j - 1));
            target.Source = "";
            target = (Image)grid.FindByName("b" + i + (j + 1));
            target.Source = "";

            // clear board
            board[i, j] = "";
            board[i, j - 1] = "";
            board[i, j + 1] = "";

            totalPoints += 30;
            score.Text = "Current score: " + totalPoints;
        }

        // clear the column in screen and the column of board
        void clearColPong(int i, int j)
        {
            var target = (Image)grid.FindByName("b" + i + j);
            target.Source = "";
            target = (Image)grid.FindByName("b" + (i - 1) + j);
            target.Source = "";
            target = (Image)grid.FindByName("b" + (i + 1) + j);
            target.Source = "";

            // clear board
            board[i, j] = "";
            board[i - 1, j] = "";
            board[i + 1, j] = "";

            totalPoints += 30;
            score.Text = "Current score: " + totalPoints;
        }

        // clear the row for chow
        void clearRowChow(int i, int j)
        {
            var target = (Image)grid.FindByName("b" + i + j);
            target.Source = "";
            target = (Image)grid.FindByName("b" + i + (j - 1));
            target.Source = "";
            target = (Image)grid.FindByName("b" + i + (j + 1));
            target.Source = "";

            // clear board
            board[i, j] = "";
            board[i, j - 1] = "";
            board[i, j + 1] = "";

            totalPoints += 30;
            score.Text = "Current score: " + totalPoints;
        }

        // clear the column for chow
        void clearColChow(int i, int j)
        {
            var target = (Image)grid.FindByName("b" + i + j);
            target.Source = "";
            target = (Image)grid.FindByName("b" + (i - 1) + j);
            target.Source = "";
            target = (Image)grid.FindByName("b" + (i + 1) + j);
            target.Source = "";

            // clear board
            board[i, j] = "";
            board[i - 1, j] = "";
            board[i + 1, j] = "";

            totalPoints += 30;
            score.Text = "Current score: " + totalPoints;
        }

        void checkBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != "")
                    {
                        // chech row
                        if (j != 0 && j != 8 && board[i, j - 1] != "" && board[i, j + 1] != "" &&
                                 board[i, j - 1] != null && board[i, j + 1] != null &&
                                 board[i, j] != null)
                        {
                            if (isRowChow(i, j))
                            {
                                clearRowChow(i, j);
                            }
                            else if (isRowPong(i, j))
                            {
                                clearRowPong(i, j);
                            }
                        }
                        // check column
                        else if (i != 0 && i != 8 && board[i - 1, j] != "" && board[i + 1, j] != "" &&
                                 board[i - 1, j] != null && board[i + 1, j] != null &&
                                 board[i, j] != null)
                        {
                            if (isColChow(i, j))
                            {
                                clearColChow(i, j);
                            }
                            else if (isColPong(i, j))
                            {
                                clearColPong(i, j);
                            }
                        }
                    }
                }
            }
        }

        void clear_clicked(System.Object sender, System.EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var target = (Image)grid.FindByName("b" + i + j);
                    target.Source = "";
                }
            }
            board = new string[9, 9];
            totalPoints = 0;
            score.Text = "Current score: " + totalPoints;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            double botLeftX = -200.0;
            double botLeftY = -137.0;
            double boxSize = 42.0;
            int row;
            int col;
            //(botLeftX + boxSize * (col + 0.5) + label1.TranslationX) <= 10.0 &&
            //(botLeftY - boxSize * (8.5 - row) - label1.TranslationY) <= 10.0 )
            col = (int)((5.0 + mahjong.TranslationX - botLeftX) / boxSize - 0.5);
            row = (int)((5.0 + mahjong.TranslationY - botLeftY) / boxSize + 8.5);

            if (col >= 0 && col <= 8 && row >= 0 && row <= 8)
            {
                // find the location
                var target = (Image)grid.FindByName("b" + row + col);
                // save the location to board
                board[row, col] = mahjong.Source.ToString();

                if (target.Source.ToString().Length <= 6)
                {
                    target.Source = mahjong.Source;

                    mahjong.TranslationX = -TranslationX;
                    mahjong.TranslationY = -TranslationY;

                    string newMahjong = randomMahjong();
                    //String newMahjong = "mahjong/dot1";

                    mahjong.Source = newMahjong;
                }
            }

            checkBoard();
        }
    }
}

