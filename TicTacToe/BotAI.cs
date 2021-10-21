using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToe
{

    internal class BotAI
    {
        private Field _field;
        private Random _random = new Random();
        private char _symbolOlny;
        internal BotAI(Field field, char symbolOlny)
        {
            _symbolOlny = symbolOlny;
            _field = field;
            _field.TurnChanged += TurnChanged;
            TurnBot();
        }
        private void TurnChanged(object sender, EventArgs e)
        {
            TurnBot();
        }
        private void TurnBot()
        {
            if (_field.CurrentSymbol() != _symbolOlny)
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
                    _field.ButtonsTicTacToe[xCord, yCord].Content = _symbolOlny;
                    if (_field.IsBotWin(xCord, yCord))
                    {
                        MessageBox.Show($"Игра окончена, бот фракии {_symbolOlny} тебя обыграл (", "Конец игры");
                    }
                    _field.IsTurnCross = !_field.IsTurnCross;
                    return;
                }
            }
        }


    }
}
