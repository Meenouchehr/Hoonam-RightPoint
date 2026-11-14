using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class Kernel
    {
        public Kernel()
        {
            PositionList = new List<RPC.Position>();
            LocationList = new List<Location>();
        }

        public long Program { set; get; }
        public String ProgamTitle { set; get; }
        public String ProgamEnglishTitle { set; get; }
        public int Viewer { set; get; }
        public int AllViewer { set; get; }

        public int Reach { set; get; }

        public int Count { set; get; }
        public long RatePoint
        {
            get
            {
                if (AllViewer == 0)
                    return 0;

                long f = (long)Math.Ceiling(((double)Viewer / (AllViewer)) * 10000);
                //double ff = (f / 100.00);

                double k = (double)f / 100;
                long ff = (long)Math.Round(k);
                return ff;
            }
        }

        public long MinRatePoint { set; get; }


        public int Month { set; get; }

        public byte media { set; get; }
        public long group { set; get; }
        public long Contract { set; get; }

        public long Province { set; get; }

        public double ProvinceRedundant { get; set; }
        public byte ProvinceType { get; set; }


        public long Series { set; get; }

        public double SeriesRedundant { get; set; }
        public byte SeriesType { get; set; }



        public long Frequency { set; get; }
        public long PremiumFrequency { set; get; }
        public long MaxFequency { set; get; }
        public int Limit { set; get; }
        public double CPS { set; get; }
        public double CPSWD { set; get; }
        public double PPS { set; get; }

        public int ClipTime { set; get; }

        public double Extra { set; get; }
        public String ClassNo { set; get; }

        public string ChannelName { set; get; }

        public String PlayTime { set; get; }

        public bool IsDeleted { set; get; }
        public bool IsAdded { set; get; }

        public bool IsUserSelect { set; get; }

        public bool IsExtra { set; get; }

        public String Location { set; get; }

        public String Position { set; get; }


        public String MonthName { set; get; }
        public double Sum { get { return Frequency * CPS; } }
        public double PP { get { return CPSWD; } }
        public double CP { get { return CPS; } }
        public double CPR { get { return (Viewer == 0) ? 9999999999999999999999999999999.00 : CPS / RatePoint; } }

        public Nullable<bool> Before { get; set; }
        public Nullable<bool> Middle { get; set; }
        public Nullable<bool> After { get; set; }


        public long PositionID { set; get; }
        public long LocationID { set; get; }

        public String Class { get { return (Extra > 0) ? String.Format("{0}+{1}%", ClassNo, Extra) : ClassNo; } }
        public List<RPC.Position> PositionList { get; set; }
        public List<RPC.Location> LocationList { get; set; }
    }
}
