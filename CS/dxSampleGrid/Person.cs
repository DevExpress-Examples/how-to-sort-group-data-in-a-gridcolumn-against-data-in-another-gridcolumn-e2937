using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dxSampleGrid {

    public partial class Person {
        public Person() {

        }
        public Person(int i) {
            FirstName = "FirstName" + i;
            LastName = "LastName" + i;
            Age = i * 10;
        }

        string _firstName;
        public string LastName { get; set; }
        int _age;

        public string FirstName {
            get { return _firstName; }
            set {
                _firstName = value;

            }
        }

        public int Age {
            get { return _age; }
            set { _age = value; }
        }
        public int CustomSortNumber { get; set; }
    }


}
