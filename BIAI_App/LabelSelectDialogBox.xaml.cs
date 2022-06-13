using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BIAI_App
{
    /// <summary>
    /// Interaction logic for LabelSelectDialogBox.xaml
    /// </summary>
    public partial class LabelSelectDialogBox : Window
    {
        public int SelectedIndex { get; private set; }
        public LabelSelectDialogBox()
        {
            InitializeComponent();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (DiseaseLabelCombobox.SelectedIndex == -1)
            {
                MessageBox.Show("Select valid item or cancel!");
                return;
            }
            SelectedIndex = DiseaseLabelCombobox.SelectedIndex;
            DialogResult = true;
            Close();
        }
    }
}
