using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace List2XML
{
    public partial class Form1 : Form
    {
        string _xml = @"<?xml version=""1.0"" ?>
        <CustomerQueryRs>
        <CustomerRet>
        <ListID>9999999bbbb</ListID>
        <Name>Zlaine Bailey</Name>
        <FullName>Zlaine Bailey</FullName>
        <Phone>33333333</Phone>
        </CustomerRet>
        <CustomerRet>
        <ListID>9AAAAAA</ListID>
        <Name>+ Brian Boyd</Name>
        <FullName>Brian Boyd</FullName>
        <Phone>6666666666</Phone>
        </CustomerRet>
        <CustomerRet>
        <ListID>92MMMMMMMMMMM</ListID>
        <Name>Moni Leahy</Name>
        <FullName>Moni Leahy</FullName>
        <Phone>1111111111111</Phone>
        </CustomerRet>
        </CustomerQueryRs>";


        public Form1()
        {
            InitializeComponent();
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {

        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(_xml);
            var CustomerList = DeserialzeXml(_xml);
            //dataGridView1.DataSource = CustomerList;
        }

        private object DeserialzeXml(string xml)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlSerializer xs = new XmlSerializer(typeof(CustomerList));
            CustomerList cl = (CustomerList)xs.Deserialize(new StringReader(doc.OuterXml));
            return cl.Customers;
        }
    }


    [XmlRoot("CustomerQueryRs")]
    public class CustomerList
    {

        [XmlElement("CustomerRet")]
        public List<Customer> Customers
        {
            get { return customers; }
            set { customers = value; }
        }

        private List<Customer> customers = null;

        public CustomerList()
        {
            customers = new List<Customer>();
        }
    }

    public class Customer
    {
        [XmlElement(ElementName = "ListID")]
        public string ListID { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "FullName")]
        public string FullName { get; set; }

        [XmlElement(ElementName = "Phone")]
        public string Phone { get; set; }
    }
}
