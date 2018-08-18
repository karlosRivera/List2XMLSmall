using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace List2XML
{
    public partial class OrderAndOrderDetails : Form
    {
        //string xml = "<Root> <Orders> <OrderID>10248</OrderID> <CustomerID>VINET</CustomerID> <EmployeeID>5</EmployeeID> <OrderDate>1996-07-04T00:00:00</OrderDate> <RequiredDate>1996-08-01T00:00:00</RequiredDate> <ShippedDate>1996-07-16T00:00:00</ShippedDate> <ShipVia>3</ShipVia> <Freight>32.3800</Freight> <ShipName>Vins et alcools Chevalier</ShipName> <ShipAddress>59 rue de l'Abbaye</ShipAddress> <ShipCity>Reims</ShipCity> <ShipPostalCode>51100</ShipPostalCode> <ShipCountry>France</ShipCountry> <OrderDetails> <OrderID>10248</OrderID> <ProductID>11</ProductID> <UnitPrice>14.0000</UnitPrice> <Quantity>12</Quantity> <Discount>0.0000000e+000</Discount> </OrderDetails> <OrderDetails> <OrderID>10248</OrderID> <ProductID>42</ProductID> <UnitPrice>9.8000</UnitPrice> <Quantity>10</Quantity> <Discount>0.0000000e+000</Discount> </OrderDetails> <OrderDetails> <OrderID>10248</OrderID> <ProductID>72</ProductID> <UnitPrice>34.8000</UnitPrice> <Quantity>5</Quantity> <Discount>0.0000000e+000</Discount> </OrderDetails> </Orders> <Orders> <OrderID>10249</OrderID> <CustomerID>TOMSP</CustomerID> <EmployeeID>6</EmployeeID> <OrderDate>1996-07-05T00:00:00</OrderDate> <RequiredDate>1996-08-16T00:00:00</RequiredDate> <ShippedDate>1996-07-10T00:00:00</ShippedDate> <ShipVia>1</ShipVia> <Freight>11.6100</Freight> <ShipName>Toms Spezialitäten</ShipName> <ShipAddress>Luisenstr. 48</ShipAddress> <ShipCity>Münster</ShipCity> <ShipPostalCode>44087</ShipPostalCode> <ShipCountry>Germany</ShipCountry> <OrderDetails> <OrderID>10249</OrderID> <ProductID>14</ProductID> <UnitPrice>18.6000</UnitPrice> <Quantity>9</Quantity> <Discount>0.0000000e+000</Discount> </OrderDetails> <OrderDetails> <OrderID>10249</OrderID> <ProductID>51</ProductID> <UnitPrice>42.4000</UnitPrice> <Quantity>40</Quantity> <Discount>0.0000000e+000</Discount> </OrderDetails> </Orders> <Orders> <OrderID>10250</OrderID> <CustomerID>HANAR</CustomerID> <EmployeeID>4</EmployeeID> <OrderDate>1996-07-08T00:00:00</OrderDate> <RequiredDate>1996-08-05T00:00:00</RequiredDate> <ShippedDate>1996-07-12T00:00:00</ShippedDate> <ShipVia>2</ShipVia> <Freight>65.8300</Freight> <ShipName>Hanari Carnes</ShipName> <ShipAddress>Rua do Paço, 67</ShipAddress> <ShipCity>Rio de Janeiro</ShipCity> <ShipRegion>RJ</ShipRegion> <ShipPostalCode>05454-876</ShipPostalCode> <ShipCountry>Brazil</ShipCountry> <OrderDetails> <OrderID>10250</OrderID> <ProductID>41</ProductID> <UnitPrice>7.7000</UnitPrice> <Quantity>10</Quantity> <Discount>0.0000000e+000</Discount> </OrderDetails> <OrderDetails> <OrderID>10250</OrderID> <ProductID>51</ProductID> <UnitPrice>42.4000</UnitPrice> <Quantity>35</Quantity> <Discount>1.5000001e-001</Discount> </OrderDetails> <OrderDetails> <OrderID>10250</OrderID> <ProductID>65</ProductID> <UnitPrice>16.8000</UnitPrice> <Quantity>15</Quantity> <Discount>1.5000001e-001</Discount> </OrderDetails> </Orders> </Root>";
        string xml = @"<Root><Orders><OrderID>10248</OrderID><CustomerID>VINET</CustomerID>
            <OrderDetails><OrderID>10248</OrderID><ProductID>11</ProductID><UnitPrice>14.0000</UnitPrice><Quantity>12</Quantity></OrderDetails>
            </Orders></Root>";

        public OrderAndOrderDetails()
        {
            InitializeComponent();
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            var serializer = new XmlSerializer(typeof(Root));
            Root result=new Root();
            result.Orders = new List<Orders>();
            result.Orders.Add(
                new Orders()
                {
                    OrderID = "101",
                    CustomerID = "001", 
                    OrderDetails = new List<OrderDetails>()
                    { 
                        new OrderDetails
                        { 
                            OrderID="101",
                            ProductID="1",
                            UnitPrice=20.25,
                            Quantity=5
                        }
                    } 
                });

            result.Orders.Add(
                new Orders()
                {
                    OrderID = "102",
                    CustomerID = "002",
                    OrderDetails = new List<OrderDetails>()
                    { 
                        new OrderDetails
                        { 
                            OrderID="102",
                            ProductID="2",
                            UnitPrice=11,
                            Quantity=5
                        }
                    }
                });

            var stringwriter = new System.IO.StringWriter();
            var serializerToString = new XmlSerializer(typeof(Root));
            serializer.Serialize(stringwriter, result);
            var stringResult = stringwriter.ToString();
        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            var serializer = new XmlSerializer(typeof(Root));
            Root result;

            using (TextReader reader = new StringReader(xml))
            {
                result = (Root)serializer.Deserialize(reader);
            }
        }
    }

    [XmlRoot(ElementName = "OrderDetails")]
    public class OrderDetails
    {
        [XmlElement(ElementName = "OrderID")]
        public string OrderID { get; set; }
        [XmlElement(ElementName = "ProductID")]
        public string ProductID { get; set; }
        [XmlElement(ElementName = "UnitPrice")]
        public double UnitPrice { get; set; }
        [XmlElement(ElementName = "Quantity")]
        public int Quantity { get; set; }
        //[XmlElement(ElementName = "Discount")]
        //public string Discount { get; set; }
    }

    [XmlRoot(ElementName = "Orders")]
    public class Orders
    {
        [XmlElement(ElementName = "OrderID")]
        public string OrderID { get; set; }
        [XmlElement(ElementName = "CustomerID")]
        public string CustomerID { get; set; }
        //[XmlElement(ElementName = "EmployeeID")]
        //public string EmployeeID { get; set; }
        //[XmlElement(ElementName = "OrderDate")]
        //public string OrderDate { get; set; }
        //[XmlElement(ElementName = "RequiredDate")]
        //public string RequiredDate { get; set; }
        //[XmlElement(ElementName = "ShippedDate")]
        //public string ShippedDate { get; set; }
        //[XmlElement(ElementName = "ShipVia")]
        //public string ShipVia { get; set; }
        //[XmlElement(ElementName = "Freight")]
        //public string Freight { get; set; }
        //[XmlElement(ElementName = "ShipName")]
        //public string ShipName { get; set; }
        //[XmlElement(ElementName = "ShipAddress")]
        //public string ShipAddress { get; set; }
        //[XmlElement(ElementName = "ShipCity")]
        //public string ShipCity { get; set; }
        //[XmlElement(ElementName = "ShipPostalCode")]
        //public string ShipPostalCode { get; set; }
        //[XmlElement(ElementName = "ShipCountry")]
        //public string ShipCountry { get; set; }
        //[XmlElement(ElementName = "ShipRegion")]
        //public string ShipRegion { get; set; }

        [XmlElement(ElementName = "OrderDetails")]
        public List<OrderDetails> OrderDetails { get; set; }
    }

    [XmlRoot(ElementName = "Root")]
    public class Root
    {
        [XmlElement(ElementName = "Orders")]
        public List<Orders> Orders { get; set; }
    }
}
