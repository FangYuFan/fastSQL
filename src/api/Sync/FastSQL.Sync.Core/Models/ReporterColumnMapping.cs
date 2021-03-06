﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FastSQL.Sync.Core.Models
{
    public class ReporterColumnMapping: INotifyPropertyChanged
    {
        private string _sourceName;
        private string _mappingName;
        private bool _isValue;
        private bool _isKey;

        public string SourceName {
            get => _sourceName;
            set
            {
                _sourceName = value;
                OnPropertyChanged(nameof(SourceName));
            }
        }
        public string MappingName {
            get => _mappingName;
            set
            {
                _mappingName = value;
                OnPropertyChanged(nameof(MappingName));
            }
        }

        public bool Value
        {
            get => _isValue;
            set
            {
                _isValue = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public bool Key
        {
            get => _isKey;
            set
            {
                _isKey = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
