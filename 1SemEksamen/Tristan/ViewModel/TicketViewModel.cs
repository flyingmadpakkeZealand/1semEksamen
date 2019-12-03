﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _1SemEksamen.Annotations;

namespace _1SemEksamen.Tristan.ViewModel
{
    class TicketViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;







        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
