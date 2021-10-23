using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{

    internal class BotAI
    {
        private Field _field;
        private Random _random = new Random();
        private char _symbolBot;
        private char _enemyOlny;
        internal BotAI(Field field, char symbolOlny)
        {
            
            _symbolBot = symbolOlny;
            _enemyOlny = symbolOlny == Field.Cross ? Field.Zero : Field.Cross;
            _field = field;
            _field.TurnChanged += TurnChanged;
            TurnSmartBot(_field.Length);
        }


        private void TurnChanged(object sender, EventArgs e)
        {
            TurnSmartBot(_field.Length);
        }
        private void TurnBot()
        {
            if (_field.CurrentSymbol != _symbolBot)
            {
                return;
            }
            if (_field.MaxTurns == _field.Turn)
            {
                return;
            }
            for (; ; )
            {
                var xCord = _random.Next(0, _field.Length);
                var yCord = _random.Next(0, _field.Length);
                if (string.IsNullOrEmpty(_field.ButtonsTicTacToe[xCord, yCord].Content.ToString()))
                {
                    _field.ButtonsTicTacToe[xCord, yCord].Content = _symbolBot;
                    if (_field.IsBotWin(xCord, yCord))
                    {
                        MessageBox.Show($"Игра окончена, бот фракии {_symbolBot} тебя обыграл (", "Конец игры");
                    }
                    _field.IsTurnCross = !_field.IsTurnCross;
                    return;
                }
            }
        }
        private void TurnSmartBot(int length)
        {

            if (_field.IsEndGame || _field.CurrentSymbol != _symbolBot || _field.MaxTurns == _field.Turn)
            {
                return;
            }
            int maxPoints = 0;
            Cords Maxcords = (Cords)_field.ButtonsTicTacToe[0, 0].DataContext;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (_field.ButtonsTicTacToe[i, j].Content.ToString() == "")
                    {
                        Cords cords = (Cords)_field.ButtonsTicTacToe[i, j].DataContext;
                        int newPoinrs = GetPointsForCell(cords.XCord, cords.YCord);
                        if (newPoinrs > maxPoints)
                        {
                            maxPoints = newPoinrs;
                            Maxcords = cords;
                        }
                    }
                }
            }
            _field.ButtonsTicTacToe[Maxcords.XCord, Maxcords.YCord].Content = _symbolBot;
            if (_field.IsBotWin(Maxcords.XCord, Maxcords.YCord))
            {
                MessageBox.Show($"Игра окончена, бот фракии {_symbolBot} тебя обыграл (", "Конец игры");
            }
            _field.IsTurnCross = !_field.IsTurnCross;
        }
        private int GetPointsForCell(int xCord, int yCord)
        {
            int vertical = 0;
            int rightVericalTop = 0;
            int horisontal = 0;
            int leftVericalTop = 0;


            object symbolCur = null;
            for (int lineNumber = 0; lineNumber < 8; lineNumber++)
            {
                if (xCord + _field.СheckToWin[lineNumber, 0] < _field.Length && xCord + _field.СheckToWin[lineNumber, 0] >= 0 && yCord + _field.СheckToWin[lineNumber, 1] < _field.Length && yCord + _field.СheckToWin[lineNumber, 1] >= 0)
                {
                    symbolCur = _field.ButtonsTicTacToe[xCord + _field.СheckToWin[lineNumber, 0], yCord + _field.СheckToWin[lineNumber, 1]].Content;
                }
                for (int i = 1; i < _field.ConditionalToWin; i++)
                {
                    int points = IsFillCell(xCord + _field.СheckToWin[lineNumber, 0] * i, yCord + _field.СheckToWin[lineNumber, 1] * i);
                    if (points != 0)
                    {
                        if (symbolCur.ToString() != _field.ButtonsTicTacToe[xCord + _field.СheckToWin[lineNumber, 0] * i, yCord + _field.СheckToWin[lineNumber, 1] * i].Content.ToString())
                        {
                            break;
                        }
                        vertical += lineNumber % 4 == 0 ? points * i * i : 0;
                        rightVericalTop += lineNumber % 4 == 1 ? points * i * i : 0;
                        horisontal += lineNumber % 4 == 2 ? points * i * i : 0;
                        leftVericalTop += lineNumber % 4 ==3 ?  points * i * i : 0;
                    }
                    else
                    {
                        break;
                    }
                    if(points == 1 && i == 3)
                    {
                        break;
                    }
                }
            }
            return vertical + rightVericalTop + horisontal + leftVericalTop;
        }
        private int IsFillCell(int xCord, int yCord)
        {
            if (xCord < _field.Length && xCord >= 0 && yCord < _field.Length && yCord >= 0)
            {
                if (_field.ButtonsTicTacToe[xCord, yCord].Content.ToString() == _symbolBot.ToString())
                {
                    return 11;
                }
                if (_field.ButtonsTicTacToe[xCord, yCord].Content.ToString() == _enemyOlny.ToString())
                {
                    return 10;
                }
                if (_field.ButtonsTicTacToe[xCord, yCord].Content.ToString() == "")
                {
                    return 1;
                }
            }
            return 0;
        }

    }
}
