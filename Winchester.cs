using System;

namespace WinchesterDE { 
    [Serializable]
    public class Winchester {
        private string products;
        private string name;
        
        private int yearModel;
		private int postCode;
		private int telephoneCode;
		private int inthelistofCalibre;

		private double weight;
		private double length;

        // Getters şi Setters
        public string getProducts() {
            return products;
        }

        public void setProducts(string value) {
            if (value.Length > 3 && value.Length < 50) products = value;
        }

        public string getName() {
            return name;
        }

        public void setName(string value) {
            if (value.Length > 3 && value.Length < 50) name = value;
        }

        public int getYearModel() {
            return yearModel;
        }

        public void setYearModel(int value) {
            if (value > 0) yearModel = value;
        }

        public int getPostCode() {
            return postCode;
        }

        public void setPostCode(int value) {
            if (value > 0) postCode = value;
        }

        public int getTelephoneCode() {
            return telephoneCode;
        }

        public void setTelephoneCode(int value) {
            if (value >= 0 && value <= 100) telephoneCode = value;
        }

        public int getInthelistofCalibre() {
            return inthelistofCalibre;
        }

        public void setInthelistofCalibre(int value) {
            if (value > 0) inthelistofCalibre = value;
        }

        public double getWeight() {
            return weight;
        }

        public void setWeight(double value) {
            if (value > 0) weight = value;
        }

        public double getLength() {
            return length;
        }

        public void setLength(double value) {
            if (value > 0) length = value;
        }
    }
}
