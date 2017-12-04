using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPSC481_Prototype
{
    public class Section : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        // The name of the section (E.g. "Lec 01")
        public string Name { get; set; }

        // The time of the section (E.g. "MWF 2:00 - 4:00")
        public string Time { get; set; }

        public string LectureNameTime { get { return Name + " - " + Time + " - Instructor: TBA" ; } }


        public bool? IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }
        private bool? _IsChecked = false;

        // Whether or not the section is selectable
        public bool Selectable
        {
            get
            {
                return _Selectable;
            }
            set
            {
                _Selectable = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Selectable"));
            }
        }
        private bool _Selectable = true;

        public ICommand Section_Selected { get { return Select_Command; } }

        public ICommand Select_Command;
    }
}
