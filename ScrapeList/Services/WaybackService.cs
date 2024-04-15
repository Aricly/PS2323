using Humanizer;
using Microsoft.EntityFrameworkCore;
using ScrapeList.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScrapeList.Services
{
    public class WaybackService
    {
        private readonly ScrapeListContext _context;
        public WaybackService(ScrapeListContext context)
        {
            _context = context;
        }
        public class Date
        {
            private string Dyear { get; set; } = null!;
            private string Dmonth { get; set; } = null!;
            private string Dday { get; set; } = null!;
            public int Year
            {
                get { return Convert.ToInt32(Dyear); }
                set { Dyear = Convert.ToString(value); }
            }
            public int Month
            {
                get { return Convert.ToInt32(Dmonth); }
                set { Dmonth = value.ToString("D2"); }
            }
            public int Day
            {
                get { return Convert.ToInt32(Dday); }
                set { Dday = value.ToString("D2"); }
            }

            public string Date_FullString
            {
                get { return Dyear + Dmonth + Dday; }
            }

            public Date() { }
        }

        private static bool Date_To_DateTime(Date a, List<DateTime> b) // Checks if Date object exists inside of List<DateTime>
        {
            foreach (var bi in b)
            {
                if (a.Year == bi.Year && a.Month == bi.Month && a.Day == bi.Day) { return true; }
            }
            return false;
        }

        public async Task<List<string>> Check_wayback(string url, string materialName)
        {
            var timeframe = DateTime.Today.Year - 5;
            var date = new Date
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month,
                Day = 01
            };

            var existing_dates = await _context.PriceRecords
                .Where(pr => pr.Material.MaterialName == materialName && _context.SourceWebsites.Any(sw => sw.URL == url))
                .Select(pr => pr.Date).ToListAsync();

            var results = new List<string>();

            var wayback_start = "http://archive.org/wayback/available?url=";
            var wayback_end = "&timestamp=";
            var wayback = wayback_start + url + wayback_end;


            do
            {
                do
                {
                    if (Date_To_DateTime(date, existing_dates)) { break; }

                    var wayback_url = wayback + date.Date_FullString;
                    string doc = "";
#pragma warning disable SYSLIB0014 // Type or member is obsolete
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        doc = client.DownloadString(wayback_url);
                    }
#pragma warning restore SYSLIB0014 // Type or member is obsolete
                    var splits1 = doc.Split("\"url\": ");
                    //
                    if (splits1.Length > 2) // http://web.archive.org/web/20200919032717/https://www.bunnings.com.au/bastion-10mm-blue-metal-20kg_p0106948
                    {
                        var split_string1 = splits1[2]; // "http://web.archive.org/web/20230311203449/https://www.blacktownbuildingsupplies.com.au/product/blue-metal-10mm-aggregate-20kg/", "timestamp": "20230311203449"}}, "timestamp": "20230801"}
                        var splits2 = split_string1.Split("}}, ");
                        var split_string2 = splits2[0]; // "http://web.archive.org/web/20230311203449/https://www.blacktownbuildingsupplies.com.au/product/blue-metal-10mm-aggregate-20kg/", "timestamp": "20230311203449"
                        var splits3 = split_string2.Split("\"timestamp\": ");
                        var split_url = splits3[0].Split("\"");
                        var split_date = splits3[1].Split("\"");
                        var final_string = split_url[1] + ", " + split_date[1].Substring(0, 8);
                        // http://web.archive.org/web/20221109215956/https://www.blacktownbuildingsupplies.com.au/product/blue-metal-10mm-aggregate-20kg/, 20221109

                        if (!results.Contains(final_string))
                        {
                            results.Add(final_string);
                        }
                    }

                    date.Month--;   //Iterator
                } while (date.Month > 0);
                date.Month = 12;    //Reset every new year
                date.Year--;        //Iterator
            } while ( date.Year > timeframe );

            return results;
        }

    }
}
