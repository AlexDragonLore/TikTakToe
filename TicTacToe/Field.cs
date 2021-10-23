using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    internal struct Cords
    {
        public int XCord, YCord;
    }
    internal class Field
    {
        public const char Cross = '✖';
        public const char Zero = '◯';
        public event EventHandler TurnChanged;
        public Button[,] ButtonsTicTacToe;
        public short[,] СheckToWin = { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
        public int ConditionalToWin;
        public bool IsEndGame = false;
        private bool _isTurnCross = true;
        private int _turn = 0;
        
        

        public int Length { get; }
        public int MaxTurns { get; }
        public int Turn
        {
            get
            {
                return _turn;
            }
        }
        public char CurrentSymbol
        {
            get
            {
                return IsTurnCross ? Cross : Zero;
            }
        }
        public bool IsTurnCross
        {
            get
            {
                return _isTurnCross;
            }
            set
            {
                _isTurnCross = value;
                TurnChanged(this, EventArgs.Empty);
            }
        }



        public Field(Grid GameField, int size)
        {
            TurnChanged += Field_TurnChanged;
            MaxTurns = size * size;
            Length = size;
            ConditionalToWin = size < 6 ? size : 5;
            GameField.Children.Clear();
            ButtonsTicTacToe = new Button[size, size];
            GameField.RowDefinitions.Clear();
            GameField.ColumnDefinitions.Clear();
            for (int i = 0; i < size; i++)
            {
                GameField.RowDefinitions.Add(new RowDefinition());
                GameField.ColumnDefinitions.Add(new ColumnDefinition());
            }
            var minSuzeBorder = GameField.ActualWidth > GameField.ActualHeight ? GameField.ActualHeight : GameField.ActualWidth;

            var marginSize = Convert.ToInt32(minSuzeBorder / size * 0.13);
            var fontSize = Convert.ToInt32(minSuzeBorder / size * 0.85);
            var margin = new Thickness(1, 1, 1, 1);
            var padding = new Thickness(-marginSize, -marginSize - 3, -3, -3);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var button = new Button { FontSize = fontSize, Margin = margin, Padding = padding, Content = "" };
                    button.DataContext = new Cords{ XCord = i, YCord = j};
                    button.Click += PersonTurnClick;
                    ButtonsTicTacToe[i, j] = button;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    GameField.Children.Add(ButtonsTicTacToe[i, j]);
                }
            }
        }
        private void Field_TurnChanged(object sender, EventArgs e)
        {
            if (++_turn == MaxTurns)
            {
                IsEndGame = true;
                MessageBox.Show($"Игра окончена, Закончились клетки", "Конец игры");
            }
        }

        private void PersonTurnClick(object sender, RoutedEventArgs e)
        {
            if (IsEndGame)
            {
                return;
            }
            var button = e.Source as Button;
            if (button.Content != "")
            {
                return;
            }
            if (IsTurnCross)
            {
                button.Content = Cross;
            }
            else
            {
                button.Content = Zero;
            }
            if (IsEndOfGame(((Cords)button.DataContext).XCord, ((Cords)button.DataContext).YCord))
            {
                MessageBox.Show($"Игра окончена, победила фракция {CurrentSymbol}, поздравляю:D", "Конец игры");
            }
            IsTurnCross = !IsTurnCross;
        }

        public bool IsBotWin(int xCord, int yCord)
        {
           return IsEndOfGame(xCord, yCord);
        }
        private bool IsEndOfGame(int xCord, int yCord)
        {
            int vertical = 1;
            int rightVericalTop = 1;
            int horisontal = 1;
            int leftVericalTop = 1;



            for (int lineNumber = 0; lineNumber < 8; lineNumber++)
            {
                for (int i = 1; i < ConditionalToWin; i++)
                {
                    if (IsFillCell(xCord + СheckToWin[lineNumber, 0] * i, yCord + СheckToWin[lineNumber, 1] * i, CurrentSymbol))
                    {
                        vertical += lineNumber % 4 == 0 ? 1 : 0;
                        rightVericalTop += lineNumber % 4 == 1 ? 1 : 0;
                        horisontal += lineNumber % 4  == 2? 1 : 0;
                        leftVericalTop += lineNumber % 4 == 3 ? 1 : 0;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (vertical >= ConditionalToWin || rightVericalTop >= ConditionalToWin || horisontal >= ConditionalToWin || leftVericalTop >= ConditionalToWin)
            {
                IsEndGame = true;
                return true;
            }
            return false ;
        }
        private bool IsFillCell(int xCord, int yCord, char syblol)
        {
            if (xCord < Length && xCord >= 0 && yCord < Length && yCord >= 0)
            {
                if (ButtonsTicTacToe[xCord, yCord].Content.ToString() == syblol.ToString())
                {
                    return true;
                }
            }
            return false;
        }
    }

}
