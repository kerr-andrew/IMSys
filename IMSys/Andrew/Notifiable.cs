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
    public class DualValue<T> : INotifyPropertyChanged
    {
        public DualValue(T abs_base)
        {
            _value_abs = abs_base;
        }
        private T _value_abs;
        private T _value_chg;

        public T Absolute
        {
            get
            {
                return _value_abs;
            }
            set
            {
                if (!Equals(_value_abs, value))
                {
                    _value_chg += (dynamic)value - _value_abs;
                    _value_abs = value;
                    OnPropertyChanged("Absolute");
                    OnPropertyChanged("Change");
                }
            }
        }
        public T Change
        {
            get
            {
                return _value_chg;
            }
            set
            {
                if (!Equals(_value_chg, value))
                {
                    _value_abs += (dynamic)value - _value_chg;
                    _value_chg = value;
                    OnPropertyChanged("Absolute");
                    OnPropertyChanged("Change");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
