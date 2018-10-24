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
    public class CurrencyBox:TextBox
    {
        #region Dependency Properties


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(decimal),
            typeof(CurrencyBox),
            new FrameworkPropertyMetadata(0M, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(ValuePropertyChanged)));

        public decimal Value
        {
            get
            {
                return (decimal)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CurrencyBox ctb)
            {
                //Update IsNegative
                ctb.SetValue(IsNegativeProperty, ctb.Value < 0);

                //Launch event
                ctb.RaiseValueChangedEvent();
            }
        }

        public static readonly DependencyProperty IsNegativeProperty =
            DependencyProperty.Register(nameof(IsNegative), typeof(bool), typeof(CurrencyBox), new PropertyMetadata(false));

        public bool IsNegative => (bool)GetValue(IsNegativeProperty);

        #endregion

        #region Constructor
        static CurrencyBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CurrencyBox),
                new FrameworkPropertyMetadata(typeof(CurrencyBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Bind Text to Number with the specified StringFormat
            var textBinding = new Binding();
            textBinding.Path = new PropertyPath("Value");
            textBinding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
            textBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            textBinding.StringFormat = "C";
            textBinding.ConverterCulture = CultureInfo.CurrentCulture;

            BindingOperations.SetBinding(this, TextBox.TextProperty, textBinding);

            // Disable copy/paste
            DataObject.AddCopyingHandler(this, PastingEventHandler);
            DataObject.AddPastingHandler(this, PastingEventHandler);

            this.CaretIndex = this.Text.Length;
            this.PreviewKeyDown += TextBox_PreviewKeyDown;
            this.PreviewMouseDown += TextBox_PreviewMouseDown;
            this.PreviewMouseUp += TextBox_PreviewMouseUp;
            this.TextChanged += TextBox_TextChanged;
            this.ContextMenu = null;
        }
        #endregion

        #region Events
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;

            //if (Value < 0 && tb.GetBindingExpression(TextBox.TextProperty).ParentBinding.StringFormat == "C")
            //{
                // If a negative number and a StringFormat of "C" is used, then
                // place the caret before the closing paren.
              //  tb.CaretIndex = tb.Text.Length - 1;
            //}
            //else
            //{
                // Keep the caret at the end
                tb.CaretIndex = tb.Text.Length;
            //}
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Prevent changing the caret index
            e.Handled = true;
            (sender as TextBox).Focus();
        }

        void TextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Prevent changing the caret index
            e.Handled = true;
            (sender as TextBox).Focus();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsNumericKey(e.Key))
            {
                e.Handled = true;

                // Push the new number from the right
                if (Value < 0)
                {
                    Value = (Value * 10M) - (GetDigitFromKey(e.Key) / 100M);
                }
                else
                {
                    Value = (Value * 10M) + (GetDigitFromKey(e.Key) / 100M);
                }
            }
            else if (e.Key == Key.Back)
            {
                e.Handled = true;

                // Remove the right-most digit
                Value = (Value - (Value % 0.1M)) / 10M;
            }
            else if (e.Key == Key.Delete)
            {
                e.Handled = true;

                Value = 0M;
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                e.Handled = true;

                Value *= -1;
            }
            else if (IsIgnoredKey(e.Key))
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
            // Prevent copy/paste
            e.CancelCommand();
        }
        #endregion

        #region Private Methods
        private decimal GetDigitFromKey(Key key)
        {
            switch (key)
            {
                case Key.D0:
                case Key.NumPad0: return 0M;
                case Key.D1:
                case Key.NumPad1: return 1M;
                case Key.D2:
                case Key.NumPad2: return 2M;
                case Key.D3:
                case Key.NumPad3: return 3M;
                case Key.D4:
                case Key.NumPad4: return 4M;
                case Key.D5:
                case Key.NumPad5: return 5M;
                case Key.D6:
                case Key.NumPad6: return 6M;
                case Key.D7:
                case Key.NumPad7: return 7M;
                case Key.D8:
                case Key.NumPad8: return 8M;
                case Key.D9:
                case Key.NumPad9: return 9M;
                default: throw new ArgumentOutOfRangeException("Invalid key: " + key.ToString());
            }
        }

        private bool IsNumericKey(Key key)
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

        private bool IsBackspaceKey(Key key)
        {
            return key == Key.Back;
        }

        private bool IsIgnoredKey(Key key)
        {
            return key == Key.Up ||
                key == Key.Down ||
                key == Key.Tab ||
                key == Key.Enter;
        }
        #endregion

        #region Routed Events

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
        "ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CurrencyBox));

        // Provide CLR accessors for the event
        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        // This method raises the Tap event
        public void RaiseValueChangedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CurrencyBox.ValueChangedEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion
    }
}
