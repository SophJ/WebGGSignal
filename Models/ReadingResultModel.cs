using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGGSignal.Models
{
   
    public class ReadingResultModel
    {

        public float MCCB1 { get; set; } = 0;
        public float MCCB2 { get; set; } = 0;
        public float MCCB3 { get; set; } = 0;
        public float MCCB4 { get; set; } = 0;

        public float T1 { get; set; } = 0;
        public float T2 { get; set; } = 0;
        public float T3 { get; set; } = 0;
        public float T4 { get; set; } = 0;
        public float T5 { get; set; } = 0;

        public float TotalKWhCH1 { get; set; } = 0;

        //public float TotalKWhCH2 { get; set; }

        //public float TotalKWhCH3 { get; set; }

        //public float TotalKWhCH4 { get; set; }

        public float Ia1 { get; set; } = 0;
        public float Ib1 { get; set; } = 0;
        public float Ic1 { get; set; } = 0;

        public float Va1 { get; set; } = 0;
        public float Vb1 { get; set; } = 0;
        public float Vc1 { get; set; } = 0;

        public float Ia2 { get; set; } = 0;
        public float Ib2 { get; set; } = 0;
        public float Ic2 { get; set; } = 0;

        public float Va2 { get; set; } = 0;
        public float Vb2 { get; set; } = 0;
        public float Vc2 { get; set; } = 0;

        // public float Ia3 { get; set; }
        //public float Ib3 { get; set; }
        //public float Ic3 { get; set; }

        //public float Va3 { get; set; }
        //public float Vb3 { get; set; }
        //public float Vc3 { get; set; }

        //public float Ia4 { get; set; }
        //public float Ib4 { get; set; }
        //public float Ic4 { get; set; }

        //public float Va4 { get; set; }
        //public float Vb4 { get; set; }
        //public float Vc4 { get; set; }

        //public float RealPower { get; set; }
        public int Status { get; set; }

    }
}
