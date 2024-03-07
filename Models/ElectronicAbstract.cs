

using MudBlazor;
using static MudBlazor.Icons;

namespace OOAP1.Models
{
    public interface IElectronic
    {
        public string Name { get; }
        public string Number { get; }
        public int Price { get; }

    }

    public abstract class ElectronicBase : IElectronic
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public  int Price { get; set; }

        public ElectronicBase(string name, string number, int price)
        {
            Name = name;
            Number = number;
            Price = price;
        }
        public ElectronicBase() { }
    }

    public class Motherboard: ElectronicBase
    {    public string SocketType { get; set; }
        public int NumProcc { get; set; }
        public string RamType { get; set; }

        public double Frequency { get; set; }

        public Motherboard(string name, string number, int price, string socketType, int numProc, string ramType, double frequency) : base(name, number, price)
        {
            SocketType = socketType;
            NumProcc = numProc;
            RamType = ramType;
            Frequency = frequency;
        }
        public Motherboard() {
            
            Random rand = new Random();
            this.Number = UniqNum.GetFormNumber();
            this.Name = "Motherboard " + this.Number;
            this.Price = rand.Next();
            this.Frequency = (double)rand.NextInt64(2000, 3000);
            this.NumProcc = rand.Next(1,2);
            this.RamType = $"DDR{rand.Next(1, 5)}";
            this.SocketType = ((Socket)rand.Next(1, 5)).ToString();
        }

    }

    public class CPU : ElectronicBase
    {
        public string SocketType { get; set;}

        public int NumOfCores { get; set; }

        public double ClockFrequency { get; set; }

        public string TechProcc { get; set;  }
        public CPU(string name, string number, int price, string socketType, double clockFrequency, string techProcc, int numOfCores) : base(name, number, price)
        {
            SocketType = socketType;
            ClockFrequency = clockFrequency;
            TechProcc = techProcc;
            NumOfCores = numOfCores;
        }
        public CPU()
        {
            
            Random rand = new Random();
            this.Number = UniqNum.GetFormNumber();
            this.Name = "CPU " + this.Number;
            this.Price = rand.Next();
            this.ClockFrequency = (double)rand.NextInt64(1, 3);
            this.NumOfCores = (int)Math.Pow(2, rand.Next(1, 4));
            this.TechProcc = $"{rand.Next(1, 12)} NM";
            this.SocketType = ((Socket)rand.Next(1, 5)).ToString();
        }
    }
    public class HDD : ElectronicBase
    {
        public string Capacity { get; set; }

        public int Speed { get; set; }

        public string InterfaceType { get; set; }

        public HDD(string name, string number, int price, string capacity, int speed, string interfaceType):base(name, number, price)
        {
            Capacity = capacity;
            Speed = speed;
            InterfaceType = interfaceType;
        }

        public HDD()
        {
            
            Random rand = new Random();
            this.Number = UniqNum.GetFormNumber();
            this.Name = "HDD " + this.Number;
            this.Price = rand.Next();
            this.Capacity = $"{Math.Pow(2, rand.Next(1, 10))}GB";
            this.Speed = rand.Next();
            this.InterfaceType = ((Interface_type)rand.Next(1, 4)).ToString();
        }
    }
    
    public interface IElectronicFactory
    {

        ElectronicBase Create(string type);
    }
    public class ElectronicFactoryImplementation : IElectronicFactory
    {
     
        public ElectronicBase Create(string type)
        {
          
            switch (type)
            {
                case "HDD":
                    return new HDD();
                case "Motherboard":
                    return new Motherboard();
                case "CPU":
                    return new CPU();
                default:
                    throw new ArgumentException($"Wrong user type {type}");
            }
        }
    }
    enum Interface_type{
        SMD = 1,
        SASI,
        SATA,
        SAS
    }
    enum Socket
    {
        Socket754 = 1,
        SocketG2,
        SocketG3,
        Socket563,
        SocketFT3

    }
    public static class UniqNum
    {
        public static string GetFormNumber()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            var FormNumber = BitConverter.ToUInt32(buffer, 0) ^ BitConverter.ToUInt32(buffer, 4) ^ BitConverter.ToUInt32(buffer, 8) ^ BitConverter.ToUInt32(buffer, 12);
            return FormNumber.ToString("X");

        }
    }
}
