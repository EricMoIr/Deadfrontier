using Newtonsoft.Json;
using Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RequestHelper
{
    public class TradeSearch
    {
        public enum Tradezone
        {
            NWest = 1,
            North = 2,
            NEast = 3,
            Nastyas = 4,
            Central = 5,
            East = 6,
            SWest = 7,
            South = 8,
            SEast = 9,
            Doggs = 10,
            P13 = 11,
            FP = 12,
            SB = 13,
        }
        private static string MARKET_URL = "http://meaty.dfprofiler.com/browsemarketplace.php?function=browseMarket";
        private static readonly HttpClient client = new HttpClient();
        private string response;

        public string Response
        {
            get
            {
                if (string.IsNullOrEmpty(response))
                    throw new ArgumentException("Query was not yet made");
                else
                    return response;
            }
            set
            {
                response = value;
            }
        }
        public async Task<Offer[]> BrowseMarket(string item, string category, Tradezone tradezone)
        {
            List<int> aux = new List<int>();
            var values = new Dictionary<string, string>
            {
               { "tradezone", ""+(int)tradezone },
               { "search", item },
               {"category", category }
            };
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(MARKET_URL, content);

            var responseString = await response.Content.ReadAsStringAsync();
            object[] results = JsonConvert.DeserializeObject<object[]>(responseString);
            bool correct = (bool)results[0];
            Offer[] offers = new Offer[results.Length - 1];
            if (correct)
            {
                for (int i = 1; i < results.Length; i++)
                {
                    offers[i - 1] = JsonConvert.DeserializeObject<Offer>(results[i].ToString());
                }
                return offers;
            }
            else
                return new Offer[0];
        }
        /*public async Task<List<SellingOffer>> BrowseMarket(string item, string category, Tradezone tradezone)
        {
            var values = new Dictionary<string, string>
            {
               { "tradezone", ""+(int)tradezone },
               { "searchname", item },
            };
            string searchType = "buyinglist";
            if (isService(category))
            {
                values.Add("profession", category);
                values.Add("search", "services");
                values.Add("searchtype", searchType);
            }
            else
            {
                values.Add("category", category);
                values.Add("search", "trades");
                if (!category.Equals(""))
                    searchType += "category";
                if (!item.Equals(""))
                    searchType += "itemname";
                values.Add("searchtype", searchType);
            }
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(URL, content);

            var responseString = await response.Content.ReadAsStringAsync();
            Response = responseString;
            Regex regexResults = new Regex(@"tradelist_maxresults=(\d+)");
            int maxResults = int.Parse(regexResults.Match(responseString).Groups[1].Value);

            List<SellingOffer> offers = new List<SellingOffer>();

            for (int i = 0; i < maxResults; i++)
            {
                Regex regexSellerId = new Regex(@"tradelist_" + i + @"_id_member=(\d+)");
                long sellerId = long.Parse(regexSellerId.Match(responseString).Groups[1].Value);
                Regex regexSeller = new Regex(@"tradelist_" + i + @"_member_name=(.*?)&");
                string sellerName = regexSeller.Match(responseString).Groups[1].Value;
                Regex regexItem = new Regex(@"tradelist_" + i + @"_item=(.*?)&");
                string[] itemValues = regexItem.Match(responseString).Groups[1].Value.Split('_');
                string colour = "";
                string stats = "";
                string rename = "";
                foreach (string value in itemValues)
                {
                    if (value.Contains("colour"))
                        colour = value.Split(new[] { "colour" }, StringSplitOptions.None)[1];
                    else if (value.Contains("stats"))
                        stats = value.Split(new[] { "stats" }, StringSplitOptions.None)[1];
                    else if (value.Contains("name"))
                        rename = value.Split(new[] { "name" }, StringSplitOptions.None)[1];
                }

                string itemName;
                if ("".Equals(rename))
                {
                    Regex regexItemName = new Regex(@"tradelist_" + i + @"_itemname=(.*?)&");
                    itemName = regexItemName.Match(responseString).Groups[1].Value;
                }
                else
                {
                    itemName = itemValues[0];
                }

                string itemQuantity = Regex.Match(responseString, "tradelist_" + i + @"_quantity=(.*?)\&").Groups[1].Value;

                Regex regexPrice = new Regex(@"tradelist_" + i + @"_price=(\d+)");
                long price = long.Parse(regexPrice.Match(responseString).Groups[1].Value);

                SellingOffer offer = new SellingOffer(
                    sellerId,
                    sellerName,
                    itemName,
                    rename,
                    colour,
                    stats,
                    price,
                    itemQuantity);
                offers.Add(offer);
            }

            return offers;
        }*/

        private static bool isService(string category)
        {
            return category.Contains("Engineer") ||
                category.Contains("Doctor") ||
                category.Contains("Chef");
        }
    }
}
