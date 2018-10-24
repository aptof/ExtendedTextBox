using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTextBoxDemo
{
    public class MainWondowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _number = "123";
        public string Number
        {
            get => _number;
            set
            {
                if(value != _number)
                {
                    _number = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal _amount = 10.05M;
        public decimal Amount
        {
            get => _amount;
            set
            {
                if (value != _amount)
                {
                    _amount = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
