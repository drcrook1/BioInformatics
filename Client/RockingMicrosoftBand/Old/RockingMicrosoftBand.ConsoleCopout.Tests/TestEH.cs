using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RockingMicrosoftBand.ConsoleCopout.Tests
{
    [TestClass]
    public class TestEH
    {
        [TestMethod]
        public void ReadFileAndSend()
        {
            while (true)
            { 
                string input = @"C:\bandapp\band.csv";
                var sut = new DesktopCopout.Base.CopoutEventHubHelper("band2weueh");
                var messagesToSend = File.ReadAllText(input);//.ToList();
                sut.SendMessage(messagesToSend);
                
               
            }

        }
    }
}
