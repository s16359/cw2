using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    class Studies
    {
        public string name { get; set; }
        public string mode { get; set; }
        public Studies(string name, string mode)
        {
            this.name = name;
            this.mode = mode;
        }
        public Studies()
        {

        }
    }
}
