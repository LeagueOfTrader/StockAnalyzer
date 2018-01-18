using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalyzer.DataModel
{
    enum KLineType
    {
        LT_None = 0,
        LT_Yin = -1,
        LT_Yang = 1
    }

    enum KLineStrength
    {
        LS_Micro,
        LS_Small,
        LS_Medium,
        LS_Large
    }

    class KLineConstant
    {
        public const double FLUCTUATION_LV1 = 0.005;
        public const double FLUCTUATION_LV2 = 0.015;
        public const double FLUCTUATION_LV3 = 0.035;

        public const double SHADOW_SOLID_RATIO_THRESHOLD = 1.5;
        public const double SHADOW_PRICE_RATIO_THRESHOLD = 0.02;
        public const double SOLID_RATIO_THRESHOLD = 0.01;
        public const double LINE_LENGTH_THRESHOLD = 0.01;
        public const double LINE_LENGTH_RATIO_THRESHOLD = 0.1;
        
        public const double PRICE_DIFF_THRESHOLD = 0.01;
        public const double LINE_LENGTH_LIMIT_RATIO_THRESHOLD = 0.05;
    }

    class KLinePriceVolumeParam
    {
        public const int REFERENCE_DAYS = 20;
        public const double PRICE_CHG_PCT = 0.01;
        public const double PRICE_CHG_PCT_EXPLOSION = 0.05;
        public const double VOL_RATIO_UPTREND = 1.5;
        public const double VOL_RATIO_DOWNTREND = 0.9;
        public const double VOL_RATIO_EXPLOSION = 2.0;
    }
}
