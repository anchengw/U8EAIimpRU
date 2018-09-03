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
        u8MobanXml moban;
        public Form1()
        {
            InitializeComponent();
            moban = new u8MobanXml("入库单_2018.08.30_10.29.09.xml");
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
            moban.parseU8XmlRs(responseXml, out s);
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
            
            Dictionary<string, string> d1 = moban.clearCloneDictVal(moban.entryMoban);//克隆
            //Dictionary<string, string> d1 = new Dictionary<string, string>(moban.entryMoban);//克隆
            moban.rootAttr["sender"] = "001";
            moban.rootAttr["roottag"] = moban.mobanType;
            moban.rootAttr["proc"] = "add";

            moban.headerDict["code"] = "0000000001";             //单据号 相同单据号时会自动编号，但不能为空
            moban.headerDict["date"] = DateTime.Now.ToString(); //单据日期 入库日期
            moban.headerDict["receiveflag"] = "1"; //收发标志 入库单-收 1，出库单-发 0
            moban.headerDict["vouchtype"] = "10"; //'单据类型 10 产成品入库 
            moban.headerDict["businesstype"] = "成品入库"; //业务类型
            moban.headerDict["source"] = "库存"; //单据来源 采购、销售、库存、存货
            moban.headerDict["warehousecode"] = "2"; //仓库编码
            moban.headerDict["receivecode"] = "12";//收发类别编码
            moban.headerDict["departmentcode"] = "";//部门编码
            moban.headerDict["personcode"] = "";//职员编码 业务员

            moban.headerDict["handler"] = "范磊";//经手人
            moban.headerDict["maker"] = "范磊";//制单人
            moban.headerDict["memory"] = "";//备注
            moban.headerDict["chandler"] = "";//审核人
            moban.headerDict["auditdate"] = "";//审核日期

            d1["inventorycode"] = "MTVMC";//存货编码
            d1["invname"] = "PVC密度板橱柜吊柜门";//存货名称
            d1["free1"] = "YD01YG0-CG1";//商品代码 －吉屋用
            d1["free2"] = "SF20088-03PCT";//表面色 －吉屋用
            d1["quantity"] = "1";//数量
            d1["cmassunitname"] = "套";//主记量单位名称
            d1["price"] = "999";//单价
            d1["cost"] = "999";//金额
            d1["makedate"] = "";//生产日期
            d1["define30"] = "YD01YG0001";//订单号 －吉屋用
            d1["iexpiratdatecalcu"] = "0";//有效期推算方式


            moban.entryList.Add(d1);
            XmlDocument dom = moban.combineU8Xml();
            dom.Save("入库单mod.xml");
            MSXML2.XMLHTTPClass xmlHttp = new MSXML2.XMLHTTPClass();
            try
            {
                xmlHttp.open("POST", "http://U8TESTSERVER/U8EAI/import.asp", false, null, null);
                xmlHttp.send(dom.OuterXml);
                String responseXml = xmlHttp.responseText;
                string s;
                bool suc = moban.parseU8XmlRs(responseXml, out s);
                if (suc)
                    MessageBox.Show("导入成功！" + s);
                else
                    MessageBox.Show("导入失败！" + s);
            }
            catch
            { }
            finally
            {
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xmlHttp);       //COM释放
            }
        }
        
    }
}
