using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Upstox_Service.Dtos;
using Upstox_Service.Services;

namespace Upstox_Test.Monitors
{
   public class ProcessBarChatMonitor : IMonitor
    {
        private System.Timers.Timer aTimer;
        private readonly ILogger _logger;
        private readonly IOptions<Settings> _settings;
        private int _pollingInterval = 0;
        private static int bar_number = 1;
        private static IEnumerable<BarChart> barChartsResults;
        public ProcessBarChatMonitor(ILogger logger,  IOptions<Settings> settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public void StartMonitoring()
        {
            _logger.Information("ProcessBarChatMonitor Started");
            ProcessBarChartApplication();
            _logger.Information("ProcessBarChatMonitor Ended");
        }

        public void StopMonitoring()
        {
            aTimer.Enabled = false;
        }

        public bool ProcessBarChartApplication()
        {
            try
            {
                _pollingInterval = Convert.ToInt32(_settings.Value.PollingInterval);
                ThreadStart thread = new ThreadStart(GetDataFromJsonFile);
                Thread myThread = new Thread(thread);

                myThread.Start();
                Thread.Sleep(_pollingInterval);

                thread = new ThreadStart(FSM);
                Thread mysecondThread = new Thread(thread);
                mysecondThread.Start();


                var startTimeSpan = TimeSpan.Zero;
                var periodTimeSpan = TimeSpan.FromSeconds(1);

                var timer = new System.Threading.Timer((e) =>
                {
                    LogsOHLC();
                }, null, startTimeSpan, periodTimeSpan);



                return true;
            }
           catch(Exception ex)
            {
                _logger.Error(ex, "Error Occurs while reading process charts");
                return false;
            }
        }

        private void GetDataFromJsonFile()
        {
            try
            {
                var jsonpath = AppDomain.CurrentDomain.BaseDirectory;
                using (StreamReader r = new StreamReader(jsonpath + @"\\SampleJson\applicationstructure.json"))
                {
                    string json = r.ReadToEnd();
                    barChartsResults = JsonConvert.DeserializeObject<IEnumerable<BarChart>>(json);
                    _logger.Information("Response from File:  {@result}", barChartsResults);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error Occurs while reading the files");
            }
          
        }
        private void FSM()
        {
            try
            {
                aTimer = new System.Timers.Timer();
                aTimer.Interval = _pollingInterval;

                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
               
                Console.WriteLine("Start the FSM Process ");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error Occurs while reading the files");
            }

        }

        private  void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var barcodesResults = new List<BarChart>();
                foreach (var item in barChartsResults)
                {
                    item.bar_number = bar_number;
                    barcodesResults.Add(item);
                }
                var finalResults = JsonConvert.SerializeObject(barcodesResults);
                Console.WriteLine(" Send the OHLC ONLY when the bar closes results{0}", finalResults);
                Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime, bar_number++);
               
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on OnTimedEvent ");
            }

        }
        private void LogsOHLC()
        {
            try
            {
                var barcodeOHCL = new List<OHLC>();
                foreach (var item in barChartsResults)
                {
                    var objOHLC = new OHLC();
                    objOHLC.o = 6538.8f;
                    objOHLC.h = 6538.8f;
                    objOHLC.l = 6537.9f;
                    objOHLC.c = 0;
                    objOHLC.events = "ohlc_notify";
                    objOHLC.symbol = item.sym;

                    objOHLC.bar_num = bar_number;
                    barcodeOHCL.Add(objOHLC);
                }
                var finalResults = JsonConvert.SerializeObject(barcodeOHCL);
                Console.WriteLine("OHLC Logs {0}", finalResults);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on LogsOHLC ");
            }

        }

        }
}
