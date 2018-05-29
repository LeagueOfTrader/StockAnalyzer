using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace StockAnalyzer.DataSource
{
    class StockMarketDataUpdater
    {
        private string m_stockID = "";
        private Timer m_timer = null;

        public StockMarketDataUpdater(string stockID)
        {
            m_stockID = stockID;
        }

        public void start()
        {
            m_timer = new Timer(500);
            m_timer.Elapsed += new ElapsedEventHandler(update);
            m_timer.Start();
        }

        public void update(object sender, ElapsedEventArgs e)
        {
            //if (m_timer != null)
            //{
            //    m_timer.Stop();
            //}

            queryMarketData();

            //if (m_timer != null)
            //{
            //    m_timer.Start();
            //}
        }

        public void stop()
        {
            if (m_timer != null)
            {
                m_timer.Stop();
            }
        }

        private void queryMarketData()
        {
            try
            {
                StockMarketData md = StockDataCenter.getInstance().queryMarketData(m_stockID);
                if (md != null)
                {
                    StockDataCenter.getInstance().assignMarketData(m_stockID, md);
                }
            }
            catch (Exception e)
            {
                //
            }
        }
    }
}
