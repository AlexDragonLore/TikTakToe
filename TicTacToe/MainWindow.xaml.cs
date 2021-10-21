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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
       

        private void CreateFieldButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(textBoxSizeField.Text, out int size))
            {
                if (size > 2 && size < 21)
                {
                    var field = new Field(GameField, size);
                    if (isCrosBot.IsChecked == true)
                    {
                        new BotAI(field, Field.Cross);
                    }
                    if (isZeroBot.IsChecked == true)
                    {
                        new BotAI(field, Field.Zero);
                    }
                    
                }
            }
        }
    }
}
