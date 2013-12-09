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

namespace ts2rexcel.gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RexcelConverter converter;

        public MainWindow()
        {
            InitializeComponent();
            
            converter = new RexcelConverter();

            //converter.FromTs()
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            var input = TxtInput.Text;

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Enter input text");
                return;
            }

            var lines = input.Split(new[] {System.Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();

            var result = converter.FromTs(lines);
            TxtOutput.Text = string.Join(System.Environment.NewLine, result);
        }
    }
}
