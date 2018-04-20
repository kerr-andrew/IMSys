using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSys
{
    public class Notifiable<T> : INotifyPropertyChanged
    {
        private T _value;
        public T Value
        {
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
            get
            {
                return _value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public static implicit operator T(Notifiable<T> notif)
        {
            return notif.Value;
        }
        public static explicit operator Notifiable<T>(T val)
        {
            return new Notifiable<T>() { Value = val };
        }
    }
}
