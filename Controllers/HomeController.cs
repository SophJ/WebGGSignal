using InfluxDB.Net;
using InfluxDB.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebGGSignal.Models;
using System.Timers;
using System;
using System.Globalization;

namespace WebGGSignal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private InfluxDb _client;

        ReadingResultModel reading = new ReadingResultModel();

    //private static Timer aTimer;

    public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {

            // Query Device ID
            // SHOW TAG VALUES ON "MCCBSensors" FROM "Reading" WITH KEY = "DeviceId"
            var query0 = "SHOW TAG VALUES ON \"MCCBSensors\" FROM \"Reading\" WITH KEY = \"DeviceId\"";

            _client = new InfluxDb("http://52.163.189.223:8086/", "API", "API1234");
            List<Serie> results0 = await _client.QueryAsync("MCCBSensors", query0);

            var iDeviceNum = results0.Count;
            //iDeviceNum = results0.Num
            // Create Device List
            //while(iDeviceNum > 0)
            //{ Device List [0] = result0[0], [1] = [1]
            //   iDeviceNum--;
            //}

            Console.WriteLine(results0[0].Values[iDeviceNum-1][1]);

            List<ReadingResultModel> list1 = new List<ReadingResultModel>();
            list1.Append(reading);

            // [Current Time - 5 min] to [Current Time]
            string startDate = "2020-06-16";
            string endDate = "2020-06-17";

            // 1677-09-21 00:12:43.145224194
            // 2017-11-09T00:00:00.000Z
            // DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;
            Console.WriteLine("======================");
            Console.WriteLine(utcDate.ToString());

            //startDate = [Current Time - 5 min]
            //endDate = [Current Time]
            startDate = utcDate.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss.000Z");
            endDate = utcDate.ToString("yyyy-MM-ddTHH:mm:ss.000Z");

            // Console.WriteLine(utcDate.ToString("yyyy-MM-ddTHH:mm:ss.000Z"));
            // Console.WriteLine(utcDate.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss.000Z"));
            Console.WriteLine(startDate);
            Console.WriteLine(endDate);
            Console.WriteLine("======================");

            _client = new InfluxDb("http://52.163.189.223:8086/", "API", "API1234");



            // var query1 = string.Format("select (\"MCCB1\") from basic.Reading WHERE DeviceId = iCurrentDeviceId.Tostring + "AND time >= '" + startDate + "' AND time <='" + endDate + "'");

            //MCCB1
            var query1 = string.Format("select last(\"MCCB1\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time <='" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results1 = await _client.QueryAsync("MCCBSensors", query1);
         
            //_client = new InfluxDb("http://sdbinflux.southeastasia.cloudapp.azure.com:8086/", "API", "API2212");
            //var query = string.Format("select SUM(\"RealPower\") from HourlyReadings WHERE DeviceId = '8520'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            //List<Serie> results = await _client.QueryAsync("SDB", query);

            if (results1.Count == 0)
            {
                reading.MCCB1 = 0;
                reading.Status = 1;
            }

            //ReadingResultModel reading = new ReadingResultModel();

            if (results1.Count >0 && results1[0].Values.Count() > 0)
            {
                var temp_index = results1[0].Values.Count();
                //reading.RealPower = float.Parse(results[0].Values[0][1].ToString());
                reading.MCCB1 = float.Parse(results1[0].Values[temp_index-1][1].ToString());   // 
                Console.WriteLine("+++++++++++++++++++++++");
                Console.WriteLine(results1[0].Values[temp_index-1][0]);
                Console.WriteLine(results1[0].Values[temp_index-1][1]);
                Console.WriteLine("+++++++++++++++++++++++");
                reading.Status = 1;

            }

            //MCCB2
            var query2 = string.Format("select last(\"MCCB2\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results2 = await _client.QueryAsync("MCCBSensors", query2);

    

            if (results2.Count > 0 && results2[0].Values.Count() > 0)
            {
                var temp_index = results2[0].Values.Count();
                reading.MCCB2 = float.Parse(results2[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //MCCB3
            var query3 = string.Format("select last(\"MCCB3\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results3 = await _client.QueryAsync("MCCBSensors", query3);


            if (results3.Count > 0 && results3[0].Values.Count() > 0)
            {
                var temp_index = results3[0].Values.Count();
                reading.MCCB3 = float.Parse(results3[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //MCCB4
            var query4 = string.Format("select last(\"MCCB3\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results4 = await _client.QueryAsync("MCCBSensors", query4);

      

            if (results4.Count > 0 && results4[0].Values.Count() > 0)
            {
                var temp_index = results4[0].Values.Count();
                reading.MCCB4 = float.Parse(results4[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //T1
            var query5 = string.Format("select last(\"T1\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results5 = await _client.QueryAsync("MCCBSensors", query5);

         

            if (results5.Count > 0 && results5[0].Values.Count() > 0)
            {
                var temp_index = results5[0].Values.Count();
                reading.T1 = float.Parse(results5[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //T2
            var query6 = string.Format("select last(\"T2\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results6 = await _client.QueryAsync("MCCBSensors", query6);


            if (results6.Count > 0 && results6[0].Values.Count() > 0)
            {
                var temp_index = results6[0].Values.Count();
                reading.T2 = float.Parse(results6[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //T3
            var query7 = string.Format("select last(\"T3\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results7 = await _client.QueryAsync("MCCBSensors", query7);

            if (results7.Count > 0 && results7[0].Values.Count() > 0)
            {
                var temp_index = results7[0].Values.Count();
                reading.T3 = float.Parse(results7[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //T4
            var query8 = string.Format("select last(\"T4\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results8 = await _client.QueryAsync("MCCBSensors", query8);


            if (results8.Count > 0 && results8[0].Values.Count() > 0)
            {
                var temp_index = results8[0].Values.Count();
                reading.T4 = float.Parse(results8[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //T5
            var query9 = string.Format("select last(\"T5\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results9 = await _client.QueryAsync("MCCBSensors", query9);

            if (results9.Count > 0 && results9[0].Values.Count() > 0)
            {
                var temp_index = results9[0].Values.Count();
                reading.T5 = float.Parse(results9[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //Channel1-TotalKWh
            var query10 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results10 = await _client.QueryAsync("MCCBSensors", query10);

            if (results10.Count > 0 && results10[0].Values.Count() > 0)
            {
                var temp_index = results10[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results10[0].Values[temp_index -1][1].ToString());
                reading.Status = 1;

            }

            //Channel2-TotalKWh
            var query11 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results11 = await _client.QueryAsync("MCCBSensors", query11);

            if (results11.Count > 0 && results11[0].Values.Count() > 0)
            {
                var temp_index = results11[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results11[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //Channel3-TotalKWh
            var query12 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results12 = await _client.QueryAsync("MCCBSensors", query12);

            if (results12.Count > 0 && results12[0].Values.Count() > 0)
            {
                var temp_index = results12[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results12[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //Channel4-TotalKWh
            var query13 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results13 = await _client.QueryAsync("MCCBSensors", query13);

            if (results13.Count > 0 && results13[0].Values.Count() > 0)
            {
                var temp_index = results13[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results13[0].Values[temp_index -1][1].ToString());
                reading.Status = 1;

            }

            //ViewData["ReceivedBids"] = _context.Bid.Where(s => s.BidderId == user.Id).ToList();
            //return view();
            return View(reading);

        }

        public async Task<IActionResult> MCCBAsync()
        {
            string startDate = "2020-05-18";
            string endDate = "2020-05-19";

            DateTime utcDate = DateTime.UtcNow;
            Console.WriteLine("======================");
            Console.WriteLine(utcDate.ToString());

            //startDate = [Current Time - 5 min]
            //endDate = [Current Time]
            startDate = utcDate.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss.000Z");
            endDate = utcDate.ToString("yyyy-MM-ddTHH:mm:ss.000Z");

            _client = new InfluxDb("http://52.163.189.223:8086/", "API", "API1234");

            //MCCB1
            var query1 = string.Format("select last(\"MCCB1\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results1 = await _client.QueryAsync("MCCBSensors", query1);

            if (results1.Count > 0 && results1[0].Values.Count() > 0)
            {
                var temp_index = results1[0].Values.Count();
                reading.MCCB1 = float.Parse(results1[0].Values[temp_index -1][1].ToString());
                reading.Va1T = DateTime.Parse(results1[0].Values[temp_index - 1][0].ToString());
                Console.WriteLine("testets");
                Console.WriteLine(reading.Va1T);

                CultureInfo enUK = new CultureInfo("en-UK");
                //CultureInfo enSG = new CultureInfo("en-SG");

                #if RELEASE
                TimeZoneInfo sgtZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Singapore");
                #endif
                #if DEBUG
                TimeZoneInfo sgtZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
                #endif

                string lstdateString, format;

                DateTime result;
                DateTime sgtTime;
                CultureInfo provider = CultureInfo.InvariantCulture;
                lstdateString = reading.Va1T.ToString();
                format = "MM/dd/yyyy HH:mm:ss tt";
                try
                {
                    result = DateTime.ParseExact(lstdateString, "M/dd/yyyy h:mm:ss tt", enUK, DateTimeStyles.None);
                    sgtTime = TimeZoneInfo.ConvertTimeFromUtc(result, sgtZone);
                    Console.WriteLine("{0} converts to {1}.", lstdateString, result.ToString());
                    Console.WriteLine("SGTime: {0}.", sgtTime.ToString());
                    reading.Va1TString = sgtTime.ToString();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not in the correct format.", lstdateString);
                    reading.Va1TString = lstdateString.ToString();
                }
                reading.Status = 1;
            }
            else
            {
                Console.WriteLine("No data");
            }

            //MCCB2
            var query2 = string.Format("select last(\"MCCB2\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results2 = await _client.QueryAsync("MCCBSensors", query2);

            if (results2.Count > 0 && results2[0].Values.Count() > 0)
            {
                var temp_index = results2[0].Values.Count();
                reading.MCCB2 = float.Parse(results2[0].Values[temp_index -1][1].ToString());
                reading.Status = 1;

            }

            //MCCB3
            var query3 = string.Format("select last(\"MCCB3\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results3 = await _client.QueryAsync("MCCBSensors", query3);

            if (results3.Count > 0 && results3[0].Values.Count() > 0)
            {
                var temp_index = results3[0].Values.Count();
                reading.MCCB3 = float.Parse(results3[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //MCCB4
            var query4 = string.Format("select last(\"MCCB4\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results4 = await _client.QueryAsync("MCCBSensors", query4);

            if (results4.Count > 0 && results4[0].Values.Count() > 0)
            {
                var temp_index = results4[0].Values.Count();
                reading.MCCB4 = float.Parse(results4[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //Last Trip MCCB1LT
            var query100 = string.Format("select last(\"MCCB1\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4' AND Value = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results100 = await _client.QueryAsync("MCCBSensors", query100);

            if (results100.Count > 0 && results100[0].Values.Count() > 0)
            {
                //DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
               
                var temp_index = results100[0].Values.Count();
                reading.unixStart = DateTime.Parse(results100[0].Values[temp_index - 1][0].ToString());
                reading.Status = 1;

            }

            //Last Trip MCCB2LT
            var query101 = string.Format("select last(\"MCCB2\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4' AND Value = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results101 = await _client.QueryAsync("MCCBSensors", query101);

            if (results101.Count > 0 && results101[0].Values.Count() > 0)
            {
                var temp_index = results101[0].Values.Count();
                reading.unixStart = DateTime.Parse(results101[0].Values[temp_index - 1][0].ToString());
                reading.Status = 1;

            }

            //Last Trip MCCB3LT
            var query102 = string.Format("select last(\"MCCB3\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4' AND Value = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results102 = await _client.QueryAsync("MCCBSensors", query102);

            if (results102.Count > 0 && results101[0].Values.Count() > 0)
            {
                var temp_index = results102[0].Values.Count();
                reading.unixStart = DateTime.Parse(results102[0].Values[temp_index - 1][0].ToString());
                reading.Status = 1;

            }

            //Last Trip MCCB4LT
            var query103 = string.Format("select last(\"MCCB4\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4' AND Value = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results103 = await _client.QueryAsync("MCCBSensors", query103);

            if (results103.Count > 0 && results103[0].Values.Count() > 0)
            {
                var temp_index = results103[0].Values.Count();
                reading.unixStart = DateTime.Parse(results103[0].Values[temp_index - 1][0].ToString());
                reading.Status = 1;

            }

            //            List<Bid> bidList = _context.Bid.Where(b => b.Id != id && b.CompanyId == companyId).ToList<Bid>();

            return View(reading);
        }

        public async Task<IActionResult> PowerMeterAsync()
        {
            string startDate = "2020-05-18";
            string endDate = "2020-05-19";

            DateTime utcDate = DateTime.UtcNow;
            Console.WriteLine("===========Power Meter===========");
            Console.WriteLine(utcDate.ToString());

            //startDate = [Current Time - 5 min]
            //endDate = [Current Time]
            startDate = utcDate.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss.000Z");
            endDate = utcDate.ToString("yyyy-MM-ddTHH:mm:ss.000Z");

            _client = new InfluxDb("http://52.163.189.223:8086/", "API", "API1234");

            //Channel1-TotalKWh
            var query10 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results10 = await _client.QueryAsync("MCCBSensors", query10);

            if (results10.Count > 0 && results10[0].Values.Count() > 0)
            {
                var temp_index = results10[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results10[0].Values[temp_index - 1][1].ToString());
                reading.Va1T = DateTime.Parse(results10[0].Values[temp_index - 1][0].ToString());
                Console.WriteLine("testets");
                Console.WriteLine(reading.Va1T);

                CultureInfo enUK = new CultureInfo("en-UK");
                //CultureInfo enSG = new CultureInfo("en-SG");
                TimeZoneInfo sgtZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

                string lstdateString, format;

                DateTime result;
                DateTime sgtTime;
                CultureInfo provider = CultureInfo.InvariantCulture;
                lstdateString = reading.Va1T.ToString();
                format = "MM/dd/yyyy HH:mm:ss tt";
                try
                {
                    result = DateTime.ParseExact(lstdateString, "M/dd/yyyy h:mm:ss tt", enUK, DateTimeStyles.None);
                    sgtTime = TimeZoneInfo.ConvertTimeFromUtc(result, sgtZone);
                    Console.WriteLine("{0} converts to {1}.", lstdateString, result.ToString());
                    Console.WriteLine("SGTime: {0}.", sgtTime.ToString());
                    reading.Va1TString = sgtTime.ToString();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not in the correct format.", lstdateString);
                    reading.Va1TString = lstdateString.ToString();
                }
                reading.Status = 1;
            }
            else
            {
                Console.WriteLine("No data");
            }

            //VA1
            var query14 = string.Format("select last(\"Va\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results14 = await _client.QueryAsync("MCCBSensors", query14);

            if (results14.Count > 0 && results14[0].Values.Count() > 0)
            {
                var temp_index = results14[0].Values.Count();
                reading.Va1 = float.Parse(results14[0].Values[temp_index - 1][1].ToString());
                reading.Va1T = DateTime.Parse(results14[0].Values[temp_index - 1][0].ToString());
                Console.WriteLine("testets");
                Console.WriteLine(reading.Va1T);

                CultureInfo enUK = new CultureInfo("en-UK");
                //CultureInfo enSG = new CultureInfo("en-SG");
                TimeZoneInfo sgtZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
                
                string lstdateString, format;

                DateTime result;
                DateTime sgtTime;
                CultureInfo provider = CultureInfo.InvariantCulture;
                lstdateString = reading.Va1T.ToString();
                format = "MM/dd/yyyy HH:mm:ss tt";
                try
                {
                    result = DateTime.ParseExact(lstdateString, "M/dd/yyyy h:mm:ss tt", enUK, DateTimeStyles.None);
                    sgtTime = TimeZoneInfo.ConvertTimeFromUtc(result, sgtZone);
                    Console.WriteLine("{0} converts to {1}.", lstdateString, result.ToString());
                    Console.WriteLine("SGTime: {0}.", sgtTime.ToString());
                    reading.Va1TString = sgtTime.ToString();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not in the correct format.", lstdateString);
                }
                reading.Status = 1;
            }
            else
            {
                Console.WriteLine("No data");
            }

            //VB1
            var query15 = string.Format("select last(\"Vb\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results15 = await _client.QueryAsync("MCCBSensors", query15);

            if (results15.Count > 0 && results15[0].Values.Count() > 0)
            {
                var temp_index = results15[0].Values.Count();
                reading.Vb1 = float.Parse(results15[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VC1
            var query16 = string.Format("select last(\"Vc\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results16 = await _client.QueryAsync("MCCBSensors", query16);

            if (results16.Count > 0 && results16[0].Values.Count() > 0)
            {
                var temp_index = results16[0].Values.Count();
                reading.Vc1 = float.Parse(results16[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IA1
            var query17 = string.Format("select last(\"Ia\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results17 = await _client.QueryAsync("MCCBSensors", query17);

            if (results17.Count > 0 && results17[0].Values.Count() > 0)
            {
                var temp_index = results17[0].Values.Count();
                reading.Ia1 = float.Parse(results17[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IB1
            var query18 = string.Format("select last(\"Ib\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results18 = await _client.QueryAsync("MCCBSensors", query18);

            if (results18.Count > 0 && results18[0].Values.Count() > 0)
            {
                var temp_index = results18[0].Values.Count();
                reading.Ib1 = float.Parse(results18[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IC1
            var query19 = string.Format("select last(\"Ic\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '1'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results19 = await _client.QueryAsync("MCCBSensors", query19);

            if (results19.Count > 0 && results19[0].Values.Count() > 0)
            {
                var temp_index = results19[0].Values.Count();
                reading.Ic1 = float.Parse(results19[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //Channel2-TotalKWh
            var query11 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results11 = await _client.QueryAsync("MCCBSensors", query11);

            if (results11.Count > 0 && results11[0].Values.Count() > 0)
            {
                var temp_index = results11[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results11[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VA2
            var query20 = string.Format("select last(\"Va\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results20 = await _client.QueryAsync("MCCBSensors", query20);

            if (results20.Count > 0 && results20[0].Values.Count() > 0)
            {
                var temp_index = results20[0].Values.Count();
                reading.Va2 = float.Parse(results20[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VB2
            var query21 = string.Format("select last(\"Vb\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results21 = await _client.QueryAsync("MCCBSensors", query21);

            if (results21.Count > 0 && results21[0].Values.Count() > 0)
            {
                var temp_index = results21[0].Values.Count();
                reading.Vb2 = float.Parse(results21[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VC2
            var query22 = string.Format("select last(\"Vc\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results22 = await _client.QueryAsync("MCCBSensors", query22);

            if (results22.Count > 0 && results22[0].Values.Count() > 0)
            {
                var temp_index = results22[0].Values.Count();
                reading.Vc2 = float.Parse(results22[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IA2
            var query23 = string.Format("select last(\"Ia\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results23 = await _client.QueryAsync("MCCBSensors", query23);

            if (results23.Count > 0 && results23[0].Values.Count() > 0)
            {
                var temp_index = results23[0].Values.Count();
                reading.Ia2 = float.Parse(results23[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IB2
            var query24 = string.Format("select last(\"Ib\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results24 = await _client.QueryAsync("MCCBSensors", query24);

            if (results24.Count > 0 && results24[0].Values.Count() > 0)
            {
                var temp_index = results24[0].Values.Count();
                reading.Ib2 = float.Parse(results24[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IC2
            var query25 = string.Format("select last(\"Ic\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '2'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results25 = await _client.QueryAsync("MCCBSensors", query25);

            if (results25.Count > 0 && results25[0].Values.Count() > 0)
            {
                var temp_index = results25[0].Values.Count();
                reading.Ic2 = float.Parse(results25[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }


            //Channel3-TotalKWh
            var query12 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results12 = await _client.QueryAsync("MCCBSensors", query12);

            if (results12.Count > 0 && results12[0].Values.Count() > 0)
            {
                var temp_index = results12[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results12[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VA3
            var query26 = string.Format("select last(\"Va\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results26 = await _client.QueryAsync("MCCBSensors", query26);

            if (results26.Count > 0 && results26[0].Values.Count() > 0)
            {
                var temp_index = results26[0].Values.Count();
                reading.Va1 = float.Parse(results26[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VB3
            var query27 = string.Format("select last(\"Vb\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results27 = await _client.QueryAsync("MCCBSensors", query27);

            if (results27.Count > 0 && results27[0].Values.Count() > 0)
            {
                var temp_index = results27[0].Values.Count();
                reading.Vb1 = float.Parse(results27[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VC3
            var query28 = string.Format("select last(\"Vc\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results28 = await _client.QueryAsync("MCCBSensors", query28);

            if (results28.Count > 0 && results28[0].Values.Count() > 0)
            {
                var temp_index = results28[0].Values.Count();
                reading.Vc1 = float.Parse(results28[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IA3
            var query29 = string.Format("select last(\"Ia\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results29 = await _client.QueryAsync("MCCBSensors", query29);

            if (results29.Count > 0 && results29[0].Values.Count() > 0)
            {
                var temp_index = results29[0].Values.Count();
                reading.Ia1 = float.Parse(results29[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IB3
            var query30 = string.Format("select last(\"Ib\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results30 = await _client.QueryAsync("MCCBSensors", query30);

            if (results30.Count > 0 && results30[0].Values.Count() > 0)
            {
                var temp_index = results30[0].Values.Count();
                reading.Ib1 = float.Parse(results30[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IC3
            var query31 = string.Format("select last(\"Ic\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '3'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results31 = await _client.QueryAsync("MCCBSensors", query31);

            if (results31.Count > 0 && results31[0].Values.Count() > 0)
            {
                var temp_index = results31[0].Values.Count();
                reading.Ic1 = float.Parse(results31[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }


            //Channel4-TotalKWh
            var query13 = string.Format("select last(\"TotalKWh\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results13 = await _client.QueryAsync("MCCBSensors", query13);

            if (results13.Count > 0 && results13[0].Values.Count() > 0)
            {
                var temp_index = results13[0].Values.Count();
                reading.TotalKWhCH1 = float.Parse(results13[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VA4
            var query32 = string.Format("select last(\"Va\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results32 = await _client.QueryAsync("MCCBSensors", query32);

            if (results32.Count > 0 && results32[0].Values.Count() > 0)
            {
                var temp_index = results32[0].Values.Count();
                reading.Va1 = float.Parse(results32[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VB4
            var query33 = string.Format("select last(\"Vb\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results33 = await _client.QueryAsync("MCCBSensors", query33);

            if (results27.Count > 0 && results33[0].Values.Count() > 0)
            {
                var temp_index = results33[0].Values.Count();
                reading.Vb1 = float.Parse(results33[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //VC4
            var query34 = string.Format("select last(\"Vc\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results34 = await _client.QueryAsync("MCCBSensors", query34);

            if (results34.Count > 0 && results34[0].Values.Count() > 0)
            {
                var temp_index = results34[0].Values.Count();
                reading.Vc1 = float.Parse(results34[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IA4
            var query35 = string.Format("select last(\"Ia\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results35 = await _client.QueryAsync("MCCBSensors", query35);

            if (results35.Count > 0 && results35[0].Values.Count() > 0)
            {
                var temp_index = results35[0].Values.Count();
                reading.Ia1 = float.Parse(results35[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IB4
            var query36 = string.Format("select last(\"Ib\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results36 = await _client.QueryAsync("MCCBSensors", query36);

            if (results36.Count > 0 && results36[0].Values.Count() > 0)
            {
                var temp_index = results36[0].Values.Count();
                reading.Ib1 = float.Parse(results36[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            //IC4
            var query37 = string.Format("select last(\"Ic\") from basic.Reading WHERE DeviceId = '59001' AND ChannelId = '4'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results37 = await _client.QueryAsync("MCCBSensors", query37);

            if (results37.Count > 0 && results37[0].Values.Count() > 0)
            {
                var temp_index = results37[0].Values.Count();
                reading.Ic1 = float.Parse(results37[0].Values[temp_index - 1][1].ToString());
                reading.Status = 1;

            }

            return View(reading);
        }

        public async Task<IActionResult> TemperatureAsync()
        {
            string startDate = "2020-05-18";
            string endDate = "2020-05-19";

            DateTime utcDate = DateTime.UtcNow;
            Console.WriteLine("======================");
            Console.WriteLine(utcDate.ToString());

            //startDate = [Current Time - 5 min]
            //endDate = [Current Time]
            startDate = utcDate.AddMinutes(-5).ToString("yyyy-MM-ddTHH:mm:ss.000Z");
            endDate = utcDate.ToString("yyyy-MM-ddTHH:mm:ss.000Z");

            _client = new InfluxDb("http://52.163.189.223:8086/", "API", "API1234");

            //T1
            var query5 = string.Format("select last(\"T1\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results5 = await _client.QueryAsync("MCCBSensors", query5);

            if (results5.Count > 0 && results5[0].Values.Count() > 0)
            {
                var temp_index = results5[0].Values.Count();
                reading.T1 = float.Parse(results5[0].Values[temp_index - 1][1].ToString());
                reading.Va1T = DateTime.Parse(results5[0].Values[temp_index - 1][0].ToString());
                Console.WriteLine("testets");
                Console.WriteLine(reading.Va1T);

                CultureInfo enUK = new CultureInfo("en-UK");
                //CultureInfo enSG = new CultureInfo("en-SG");
                TimeZoneInfo sgtZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

                string lstdateString, format;

                DateTime result;
                DateTime sgtTime;
                CultureInfo provider = CultureInfo.InvariantCulture;
                lstdateString = reading.Va1T.ToString();
                format = "MM/dd/yyyy HH:mm:ss tt";
                try
                {
                    result = DateTime.ParseExact(lstdateString, "M/dd/yyyy h:mm:ss tt", enUK, DateTimeStyles.None);
                    sgtTime = TimeZoneInfo.ConvertTimeFromUtc(result, sgtZone);
                    Console.WriteLine("{0} converts to {1}.", lstdateString, result.ToString());
                    Console.WriteLine("SGTime: {0}.", sgtTime.ToString());
                    reading.Va1TString = sgtTime.ToString();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not in the correct format.", lstdateString);
                    reading.Va1TString = lstdateString.ToString();
                }
                reading.Status = 1;
            }
            else
            {
                Console.WriteLine("No data");
            }

            //T2
            var query6 = string.Format("select last(\"T2\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results6 = await _client.QueryAsync("MCCBSensors", query6);

            if (results6.Count > 0 && results6[0].Values.Count() > 0)
            {
                var temp_index = results6[0].Values.Count();
                reading.T2 = float.Parse(results6[0].Values[temp_index -1][1].ToString());
                reading.Status = 1;

            }

            //T3
            var query7 = string.Format("select last(\"T3\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results7 = await _client.QueryAsync("MCCBSensors", query7);

            if (results7.Count > 0 && results7[0].Values.Count() > 0)
            {
                var temp_index = results7[0].Values.Count();
                reading.T3 = float.Parse(results7[0].Values[temp_index -1][1].ToString());
                reading.Status = 1;

            }

            //T4
            var query8 = string.Format("select last(\"T4\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results8 = await _client.QueryAsync("MCCBSensors", query8);

            if (results8.Count > 0 && results8[0].Values.Count() > 0)
            {
                var temp_index = results8[0].Values.Count();
                reading.T4 = float.Parse(results8[0].Values[temp_index -1][1].ToString());
                reading.Status = 1;

            }

            //T5
            var query9 = string.Format("select last(\"T5\") from basic.Reading WHERE DeviceId = '59001'" + "AND time >= '" + startDate + "' AND time < '" + endDate + "'"); //Get latest timestamp for specific device
            List<Serie> results9 = await _client.QueryAsync("MCCBSensors", query9);

            if (results9.Count > 0 && results9[0].Values.Count() > 0)
            {
                var temp_index = results9[0].Values.Count();
                reading.T5 = float.Parse(results9[0].Values[temp_index -1][1].ToString());
                reading.Status = 1;

            }

            return View(reading);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Handle the Elapsed event.

    }
}
