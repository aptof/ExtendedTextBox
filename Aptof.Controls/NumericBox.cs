using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Aptof.Controls
{
    
    public class NumberBox : TextBox
    {
       
        #region Dependency Propereties
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
            "Number", typeof(string), typeof(NumberBox),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(NumberPropertyChanged)));

        public string Number
        {
            get => (string)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        private static void NumberPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NumberBox nb)
            {
                nb.RaiseNumberChangedEvent();
            }
        }

        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
            "Length", typeof(int), typeof(NumberBox),
            new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(LengthPropertyChanged)));

        public int Length
        {
            get => (int)GetValue(LengthProperty);
            set => SetValue(LengthProperty, value);
        }

        private static void LengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NumberBox nb)
            {
                if((int)e.NewValue < nb.Number.Length)
                {
                    nb.Number = nb.Number.Substring(0, (int)e.NewValue);
                }
            }
        }

        #endregion

        #region Constructors

        static NumberBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberBox), new FrameworkPropertyMetadata(typeof(NumberBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var textBinding = new Binding
            {
                Path = new PropertyPath("Number"),
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            BindingOperations.SetBinding(this, TextProperty, textBinding);

            // Disable copy/paste
            DataObject.AddCopyingHandler(this, PastingEventHandler);
            DataObject.AddPastingHandler(this, PastingEventHandler);

            this.CaretIndex = this.Text.Length;
            this.PreviewKeyDown += TextBox_PreviewKeyDown;
            this.ContextMenu = null;
        }

        #endregion

        #region Subscribed Events

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Number == null)
                return;
            if(IsNumberKey(e.Key))
            {
                if (Length < 0 || Length > Number.Length)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else if(IsControlKey(e.Key))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void PastingEventHandler(object sender, DataObjectEventArgs e)
        {
            e.CancelCommand();
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent NumberChangedEvent = EventManager.RegisterRoutedEvent(
        "NumberChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumberBox));

        // Provide CLR accessors for the event
        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(NumberChangedEvent, value); }
            remove { RemoveHandler(NumberChangedEvent, value); }
        }

        // This method raises the Tap event
        public void RaiseNumberChangedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(NumberBox.NumberChangedEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion


        #region Private Methods



        private bool IsNumberKey(Key key)
        {
            return key == Key.D0 ||
                key == Key.D1 ||
                key == Key.D2 ||
                key == Key.D3 ||
                key == Key.D4 ||
                key == Key.D5 ||
                key == Key.D6 ||
                key == Key.D7 ||
                key == Key.D8 ||
                key == Key.D9 ||
                key == Key.NumPad0 ||
                key == Key.NumPad1 ||
                key == Key.NumPad2 ||
                key == Key.NumPad3 ||
                key == Key.NumPad4 ||
                key == Key.NumPad5 ||
                key == Key.NumPad6 ||
                key == Key.NumPad7 ||
                key == Key.NumPad8 ||
                key == Key.NumPad9;
                
        }

        private bool IsControlKey(Key key)
        {
            return key == Key.Back ||
                key == Key.Left ||
                key == Key.Right ||
                key == Key.Delete;
        }
        #endregion
    }
}
