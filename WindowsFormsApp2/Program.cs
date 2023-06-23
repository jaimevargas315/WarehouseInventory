using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Inventory inventory = new Inventory();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());

        }
    }
   
     public class Inventory
    {

        
        public BindingList<Part> AllParts = new BindingList<Part>();
        public BindingList<Product> Products = new BindingList<Product>();



        public void addProduct(Product product)
        {
            Products.Add(product);
        }

        public bool removeProduct(int x)
        {
            foreach (Product product in Products)
                if (product.ProductID == x)
                {
                    Products.Remove(product);
                    return true;
                }
                return false;
        }

        public Product lookupProduct(int x)
        {
            Product found = null;

                foreach (Product product in Products)
                    if (product.ProductID ==  x)
                    found =product;
 
            return found;
        }

        public void updateProduct(int index,ref Product product)
        {

            Products[index] = product;
        }

        public void addPart(Part part)
        {
            AllParts.Add(part);
        }

        public void deletePart(ref Part part)
        {
           AllParts.Remove(part);
        }

        public Part lookupPart(int x)
        {
            Part found = null;

            foreach (Part part in AllParts)
                if (part.PartID == x)
                    found = part;

            return found;

        }

        public void updatePart(int index, Part part)
        {
            AllParts[index] = part;
        }


    }

    public abstract class Part
    {
        public static int nrOfInstances = 0;

        public int _PartID;
        public int PartID { get { return _PartID; } set { _PartID = value;  } }
        public string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }
        public decimal _Price;
        public decimal Price { get { return _Price; } set { _Price = value; } }
        public int _InStock;
        public int InStock { get { return _InStock; } set { _InStock = value; } }
        public int _Min;
        public int Min { get { return _Min; } set { _Min = value; } }
        public int _Max;
        public int Max { get { return _Max; } set { _Max = value; } }
        public int _MachineID;
        public int MachineID { get{ return _MachineID; } set{ _MachineID = value; } }
        public string _CompanyName;
        public string CompanyName { get { return _CompanyName; } set { _CompanyName = value; } }

        public override string ToString()
        {
            return "Part ID:" + PartID + ", Name: " + Name + ", Price: " + Price + 
                    ", In Stock:" + InStock + ", Min:" + Min + ", Max:" + Max;
        }

    }
    
    public class Inhouse : Part
    {
        public Inhouse(string name, decimal price, int instock, int min, int max,int machineid)
        {
            PartID = nrOfInstances ++; Name = name; Price = price; InStock = instock; Min = min; Max = max; MachineID = machineid;
        }


        public override string ToString()
        {
            return "Part ID:" + PartID + ", Name: " + Name + ", Price: " + Price + ", In Stock:" + InStock +
                    ", Min:" + Min + ", Max:" + Max + ", Machine ID:" + MachineID;
        }

    }

    public class Outsourced : Part
    {
        public Outsourced(string name, decimal price, int instock, int min, int max, string companyname)
        {
            PartID = nrOfInstances++; Name = name; Price = price; InStock = instock; Min = min; Max = max; CompanyName = companyname;
        }
      

        public override string ToString()
        {
            return "Part ID:" + PartID + ", Name: " + Name + ", Price: " + Price + ", In Stock:" + InStock +
                    ", Min:" + Min + ", Max:" + Max + ", Company Name" + CompanyName;
        }
    }
   
    public class Product
    {
        public BindingList<Part> AssociatedParts = new BindingList<Part>();
        public static int nrOfInstances = 0;

        public int _ProductID;
        public int ProductID { get{ return _ProductID; } set{ _ProductID = value; } }
        public string _Name;
        public string Name { get{ return _Name; } set{ _Name = value; } }
        public decimal _Price;
        public decimal Price { get{ return _Price; }set{ _Price = value; } }
        public int _InStock;
        public int InStock { get{ return _InStock; } set{ _InStock = value; } }
        public int _Min;
        public int Min { get{ return _Min; } set{ _Min = value; } }
        public int _Max;
        public int Max { get{ return _Max; } set{ _Max = value; } }

        public Product(string name, decimal price, int instock, int min, int max)
        {
            ProductID = nrOfInstances++;
            Name = name;
            Price = price;
            InStock = instock;
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return "Product ID:" + ProductID + ", Name: " + Name + ", Price: " + Price + ", In Stock:" + InStock +
                    ", Min:" + Min + ", Max:" + Max;
        }

        public void addAssociatedPart(Part part)
        {
           this.AssociatedParts.Add(part);
        }

        public bool removeAssociatedPart(int x)
        {
            if (AssociatedParts[x] != null)
            {
                AssociatedParts.RemoveAt(x);
                return true;
            }
            return false;
        }
        
        public Part lookupAssociatedPart(int x)
        {
            Part found = null;

            foreach (Part part in AssociatedParts)
                if (part.PartID == x)
                    found = part;

            return found;
        }

    }
}
