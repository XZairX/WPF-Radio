using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RadioApp;

namespace WPFRadio
{
    public partial class MainWindow : Window
    {
        private readonly Radio _radio = new Radio();

        public MainWindow()
        {
            InitializeComponent();
            UpdateText();            
        }

        private void UpdateText()
        {
            TextOutput.Text = $"{ _radio.Play()}\nVolume: {_radio.Volume}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Content.ToString();
            if (int.TryParse(content, out int result))
            {
                _radio.SwitchToChannel(result);
            }
            else
            {
                switch(content.ToUpper())
                {
                    case "PLAY":
                    case "ON":
                        _radio.TurnOn();
                        break;
                    case "OFF":
                        _radio.TurnOff();
                        break;
                    case "MUTE":
                        _radio.Mute();
                        break;
                    case "PREV":
                        _radio.SwitchToPreviousChannel();
                        break;
                    case "NEXT":
                        _radio.SwitchToNextChannel();
                        break;
                    case "SHUFFLE":
                        button.Background = _radio.ToggleShuffle() ? Brushes.Red : Brushes.LightGray;
                        break;
                }
            }
            UpdateText();
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            RepeatButton repeatButton = (RepeatButton)sender;
            string content = repeatButton.Content.ToString();
            switch(content.ToUpper())
            {
                case "VOL -":
                    _radio.VolumeDown();
                    break;
                case "VOL +":
                    _radio.VolumeUp();
                    break;
            }
            UpdateText();
        }

        private void MouseButton_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Button button = (Button)sender;
            string content = button.Content.ToString();
            switch (content.ToUpper())
            {
                case "VOL\nMIN":
                    _radio.VolumeMin();
                    break;
                case "VOL\nMAX":
                    _radio.VolumeMax();
                    break;
            }
            UpdateText();
        }
    }
}
