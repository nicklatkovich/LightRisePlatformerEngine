using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightRise.BaseClasses {
    class NegativeValueException : Exception {

        public NegativeValueException() : base("This variable can not be negative") {

        }

    }
}
