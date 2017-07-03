using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate1
{
    public class Header
    {
        private int temperature;
        public string type = "RealFile 001";
        public string area = "China Xian";

        public delegate void BoiledEventHandler(object sender, BoiledEventArgs e);
        public event BoiledEventHandler Boiled;
        

        public class BoiledEventArgs: EventArgs
        {
            public int temperature { get; private set; }
            public BoiledEventArgs(int temperature)
            {
                this.temperature = temperature;
            }
        }
        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            if (Boiled != null)
                Boiled(this, e);
            
        }

        public void BoilWater()
        {
            for(int i = 0;i<=100;i++)
            {
                temperature = i;
                if(temperature>97)
                {
                    BoiledEventArgs e = new BoiledEventArgs(temperature);
                    OnBoiled(e);
                }

            }
        }
            
    }

    public class Alarm
    {
        public void MakeAlert(object sender, Header.BoiledEventArgs e)
        {
            Header heater = (Header)sender;
            Console.WriteLine("Alarm: {0} - {1}:", heater.area, heater.type);
            Console.WriteLine("Alarm: DIDIDI, the temperature of the water is {0} C ", e.temperature);
            Console.WriteLine();
        }
    }

  
    public class Display
    {
        public static void ShowMsg(object sender, Header.BoiledEventArgs e)
        {
            Header heater = (Header)sender;
            Console.WriteLine("Display: {0} - {1}:", heater.area, heater.type);
            Console.WriteLine("Display: DIDIDI, the temperature of the water is {0} C", e.temperature);
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main()
        {
            Header header = new Header();
            Alarm alarm = new Alarm();

            header.Boiled += alarm.MakeAlert;
            header.Boiled += (new Alarm()).MakeAlert;
            header.Boiled += new Header.BoiledEventHandler(alarm.MakeAlert);
            header.Boiled += Display.ShowMsg;

            header.BoilWater();
            Console.ReadKey();
        }

       
    }
}
