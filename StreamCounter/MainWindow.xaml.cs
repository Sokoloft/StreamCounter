using System.Windows;
using System.IO;
using System;
using KeyBinding.Keys;
using System.Windows.Input;
using System.Media;


namespace StreamCounter
{

    public partial class MainWindow : Window
    {

        //OnLoad
        public MainWindow()
        {
            InitializeComponent();
            
            //Bindings
            HotkeysManager.SetupSystemHook();
            
            //Add
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.Add, () => { Add(); }));
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.OemPlus, () => { Add(); }));
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.D1, () => { Add(); }));
            //Subtract
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.Subtract, () => { Subtract(); }));
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.OemMinus, () => { Subtract(); }));
            HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.OemTilde, () => { Subtract(); }));
            //Reset
            //HotkeysManager.AddHotkey(new GlobalHotkey(ModifierKeys.Control, Key.R, () => { Reset(); }));

            //stay at front
            this.Topmost = true;
            
            //for OnExit
            this.Closed += new EventHandler(MainWindow_Closed);
            
            //try reads the text file & catch creates one if one doesn't exist
            try
            {
                tb.Text = File.ReadAllText("xyz.txt");
                _value = int.Parse(tb.Text);

            }
            catch
            {
                Write();
                tb.Text = "Welcome!";

            }

        }


        //Define _value
        private int _value;

        //Add func
        void Add()
        {
            _value += 1;
            Write();

        }

        //Subtract func
        void Subtract()
        {
            _value -= 1;
            Write();
            Check();

        }
        
        //Reset func
        void Reset()
        {
            MessageBoxResult result;

            result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _value = 0;
                Write();

            }

        }

        //Check func
        void Check()
        {

            if (_value < 0)
            {
                Write();
                Add();
                SystemSounds.Exclamation.Play();

            }
            else
            {
                Write();
                
            }

        }

        //Write func
        void Write()
        {
            StreamWriter txtFile = new StreamWriter("xyz.txt");
            txtFile.Write(_value);
            txtFile.Close();
            tb.Text = _value.ToString();

        }



        //Add btn
        private void btn01_Click(object sender, RoutedEventArgs e)
        {
            Add();

        }

        //Subtract btn
        private void btn02_Click(object sender, RoutedEventArgs e)
        {
            Subtract();

        }

        //Reset btn
        private void btn03_Click(object sender, RoutedEventArgs e)
        {
            Reset();

        }



        //OnExit
        void MainWindow_Closed(object sender, EventArgs e)
        {
            HotkeysManager.ShutdownSystemHook();

        }

    }

}
