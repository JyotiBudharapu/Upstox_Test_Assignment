using Microsoft.Extensions.Options;
using Moq;
using Serilog;
using System;
using Upstox_Service.Dtos;
using Upstox_Test.Monitors;
using Xunit;

namespace Upstox_Test_Cases.Test_Cases
{
    public class ProcessBarChatMonitorTestCases : IDisposable
    {
        public ProcessBarChatMonitor SUT { get; set; }
     
        private Mock<ILogger> _loggerMock = new Mock<ILogger>();
        private Mock<IOptions<Settings>> _settings = new Mock<IOptions<Settings>>();
        public void Dispose()
        {
            
        }

        public ProcessBarChatMonitorTestCases()
        {
            OnInit();
        }

        private void OnInit()
        {
            Settings settings = new Settings() { PollingInterval = "15000" }; 
                                                                                            
            var mock = new Mock<IOptions<Settings>>();
            mock.Setup(ap => ap.Value).Returns(settings);

            SUT = new ProcessBarChatMonitor( _loggerMock.Object, mock.Object);

            
        }
    }

    [Trait("Process", "Barc charts")]
    public class ProcessBarcchartMonitorTestServices : IClassFixture<ProcessBarChatMonitorTestCases>
    {
        private readonly ProcessBarChatMonitorTestCases _fixture;

        public ProcessBarcchartMonitorTestServices(ProcessBarChatMonitorTestCases processBarChatMonitorTestCases)
        {
            _fixture = processBarChatMonitorTestCases;
        }
       
        [Fact(DisplayName = "Process Barcharts")]
        public  void ProcessBarchartsTest()
        {
            
            //ACT
         var result= _fixture.SUT.ProcessBarChartApplication();
            

            //ASSERT
              Assert.True(true == result);
        }
    }
    }
