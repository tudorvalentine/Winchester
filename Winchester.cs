using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace WinchesterDE
{
    [Serializable]
    public class Winchester
    {
        private string products;
        private string name;

        private int yearModel;
        private int postCode;
        private int telephoneCode;
        private int inthelistofCalibre;

        private double weight;
        private double length;

        public Winchester(string products, string name, int yearModel, int postCode, int telephoneCode, int inthelistofCalibre, double weight, double length)
        {
            // Assigning parameter values to corresponding fields using 'this' keyword
            this.products = products;
            this.name = name;
            this.yearModel = yearModel;
            this.postCode = postCode;
            this.telephoneCode = telephoneCode;
            this.inthelistofCalibre = inthelistofCalibre;
            this.weight = weight;
            this.length = length;
        }
        // Getters şi Setters
        public string getProducts()
        {
            return products;
        }

        public void setProducts(string value)
        {
            if (value.Length > 3 && value.Length < 50) products = value;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string value)
        {
            if (value.Length > 3 && value.Length < 50) name = value;
        }

        public int getYearModel()
        {
            return yearModel;
        }

        public void setYearModel(int value)
        {
            if (value > 0) yearModel = value;
        }

        public int getPostCode()
        {
            return postCode;
        }

        public void setPostCode(int value)
        {
            if (value > 0) postCode = value;
        }

        public int getTelephoneCode()
        {
            return telephoneCode;
        }

        public void setTelephoneCode(int value)
        {
            if (value >= 0 && value <= 100) telephoneCode = value;
        }

        public int getInthelistofCalibre()
        {
            return inthelistofCalibre;
        }

        public void setInthelistofCalibre(int value)
        {
            if (value > 0) inthelistofCalibre = value;
        }

        public double getWeight()
        {
            return weight;
        }

        public void setWeight(double value)
        {
            if (value > 0) weight = value;
        }

        public double getLength()
        {
            return length;
        }

        public void setLength(double value)
        {
            if (value > 0) length = value;
        }
        public void SaveToFile(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
        }

        public static Winchester LoadFromFile(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Winchester)formatter.Deserialize(stream);
            }
        }
    }
}
