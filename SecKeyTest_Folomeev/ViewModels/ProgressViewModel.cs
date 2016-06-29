using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace SecKeyTest_Folomeev.ViewModels
{
    class ProgressViewModel : INotifyPropertyChanged
    {

        private double _maximumValue;
        public double maximumValue
        {
            get { return _maximumValue; }
            set
            {
                if (value != _maximumValue)
                {
                    _maximumValue = value;
                    NotifyPropertyChanged("maximumValue");
                }
            }
        }


        private double _processed;
        public double processed
        {
            get { return _processed; }
            set
            {
                if (value != _processed)
                {
                    _processed = value*100/maximumValue;                    
                    NotifyPropertyChanged("processed");
                }
            }
        }

        public ProgressViewModel()
        {
            
        }

        protected virtual void NotifyPropertyChanged(string propertyName)
        {

            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
