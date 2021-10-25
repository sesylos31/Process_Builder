using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process_Builder
{
    class Order : System.Object
    {
        private string ordID = "";

        public Order(string orderid)
        {
            this.ordID = orderid;
        }

        public string OrderID
        {
            get { return this.ordID; }
            set { this.ordID = value; }
        }
    }
}
