using Persistence;
using Persistence.Domain;
using RequestHelper;
using System;
using System.Collections.Generic;
using System.Timers;

namespace DFHacks
{
    class Program
    {
        const double HALF_AN_HOUR = 1000 * 60 * 30;
        static void Main(string[] args)
        {
            BrowseAllTheItems();
            Timer checkForTime = new Timer(HALF_AN_HOUR);
            checkForTime.Elapsed += new ElapsedEventHandler(BrowseAllTheItemsEvent);
            checkForTime.Enabled = true;
            Console.ReadKey();
        }

        private static void BrowseRoutine(string item, string category, TradeSearch.Tradezone tradezone)
        {
            TradeSearch searcher = new TradeSearch();

            //Console.WriteLine(string.Format("Browsing the market for {item}..."));
            DateTime now = DateTime.Now;
            Offer[] results = searcher.BrowseMarket(item, category, tradezone).Result;
            //Console.WriteLine("Done.");
            //Console.WriteLine("Adding results to the database...");
            for (int i = 0; i < 5; i++)
            {
                Offer result = results[i];
                result.Time = now;
                RecordOffer(result);
            }
            Console.WriteLine("Done with "+ item);
        }
        private static void BrowseAllTheItems()
        {
            string[] items =
            {
                "9mm r",
                "12.7",
                "14mm",
                ".55 ",
                "heavy gren"
            };
            for (int i = 0; i < items.Length; i++)
            {
                BrowseRoutine(items[i], "", TradeSearch.Tradezone.SB);
            }
        }
        private static void BrowseAllTheItemsEvent(object sender, ElapsedEventArgs e)
        {
            BrowseAllTheItems();
        }

        private static void RecordOffer(Offer result)
        {

            DFUnitOfWork uow = new DFUnitOfWork();
            DFRepository<Offer> offers = uow.Offers;
            offers.Insert(result);
            DateTime oldestTimePermitted = DateTime.Now.Subtract(new TimeSpan(14, 0, 0, 0));
            IEnumerable<Offer> oldOffers = offers.Get(offer => offer.Time <= oldestTimePermitted);
            foreach (Offer offer in oldOffers)
            {
                offers.Delete(offer.Id);
            }
            uow.Save();
        }
    }
}
