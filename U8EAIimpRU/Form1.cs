using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace U8EAIimpRU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ufinterface sender=\"001\" receiver=\"u8\" roottag=\"department\" docid=\" \" proc=\"Query\" codeexchanged=\"n \">")
                .Append("<department importfile=\" \" exportfile=\" \" code=\"011\" bincrementout=\"n \">")
                .Append("<field display=\"部门编码\" name=\"cDepCode\" operation=\" =\" value=\"1 \" logic=\" \" /> ")
                .Append("</department>")
                .Append("</ufinterface>");
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(sb.ToString());
            MSXML2.XMLHTTPClass xmlHttp = new MSXML2.XMLHTTPClass();
            xmlHttp.open("POST", "http://192.168.1.15/U8EAI/import.asp", false, null, null);
            xmlHttp.send("");
            String responseXml = xmlHttp.responseText;
            string s;
            parseU8XmlRs(responseXml, out s);
            MessageBox.Show(s);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xmlHttp);       //COM释放
        }
        /// <summary>
        /// 查询部门导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ufinterface sender=\"001\" receiver=\"u8\" roottag=\"department\" docid=\" \" proc=\"Query \" codeexchanged=\"n \">")
                .Append("<department>")
                .Append("</department>")
                .Append("</ufinterface>");
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(sb.ToString());
            MSXML2.XMLHTTPClass xmlHttp = new MSXML2.XMLHTTPClass();
            xmlHttp.open("POST", "http://U8TESTSERVER/U8EAI/import.asp", false, null, null);
            xmlHttp.send(dom.OuterXml);
            String responseXml = xmlHttp.responseText;
            MessageBox.Show(responseXml);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xmlHttp);       //COM释放
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            StringBuilder sb = new StringBuilder();
            sb.Append("<ufinterface sender=\"001\" receiver=\"u8\" roottag=\"storein\" docid=\" \" proc=\"Query \" codeexchanged=\"n \" paginate=\"0\">")
                .Append("<storein importfile=\"\" exportfile=\"\" code=\"0000000192\" bincrementout=\"n\">")
                .Append("</storein>")
                .Append("</ufinterface>");
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(sb.ToString());
            MSXML2.XMLHTTPClass xmlHttp = new MSXML2.XMLHTTPClass();
            xmlHttp.open("POST", "http://U8TESTSERVER/U8EAI/import.asp", false, null, null);
            xmlHttp.send(dom.OuterXml);
            String responseXml = xmlHttp.responseText;
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xmlHttp);       //COM释放
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //U8XML u8XML = new U8XML();
            //u8XML.createXml();
            //u8XML.headerAdd();
            //u8XML.bodyAdd();
            //u8XML.saveXml("");
            //u8XML.getNodeAttrs("u8xml_ruku.xml", "ufinterface");
            //u8XML.GetXml("u8xml_ruku.xml");
            //u8XML.GetXmlData("u8xml_ruku.xml", "ufinterface/storein/body/entry");
            //u8XML.getNodesVal("u8xml_ruku.xml", "ufinterface/storein/body/entry");
            u8MobanXml moban = new u8MobanXml("入库单_2018.08.30_10.29.09.xml");
            Dictionary<string, string> d1 = new Dictionary<string, string>(moban.entryMoban);//克隆
            moban.rootAttr["sender"] = "002";
            moban.rootAttr["roottag"] = moban.mobanType;
            moban.rootAttr["proc"] = "add";
            moban.entryList.Add(d1);
            moban.combineU8Xml().Save("入库单mod.xml");
        }
        public bool parseU8XmlRs(string xml, out string result)
        {
            bool rs = false;
            string key = "";
            string dsc = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement xroot = doc.DocumentElement;
            if (xroot.Attributes["roottag"].Value == "return")
            {
                XmlElement ele = (XmlElement)xroot.SelectSingleNode("item");
                key = ele.GetAttribute("key");
                dsc = ele.GetAttribute("dsc");
                rs = ele.GetAttribute("succeed").Equals("0");
            }
            result = key + "|" + dsc;
            return rs;
        }
    }
}
