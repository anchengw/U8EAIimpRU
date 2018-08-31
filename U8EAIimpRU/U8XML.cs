using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace U8EAIimpRU
{
    public class U8XML
    {
        private string xmlpre = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        private string u8xmlpre = "<ufinterface sender=\"{sender}\" receiver=\"u8\" roottag=\"{roottag}\" docid=\"\" proc=\"{proc}\" codeexchanged=\"N\" exportneedexch=\"N\" paginate=\"0\" display=\"入库单\" family=\"库存管理\" timestamp=\"\">";
        XmlDocument xmldoc;
        XmlNode header, body;
        public Dictionary<string, string> rootAttr = new Dictionary<string, string>(); //XML根节点属性字典
        public Dictionary<string, string> headerDict = new Dictionary<string, string>(); //XML根节点属性字典
        Dictionary<string, string> entryDict = new Dictionary<string, string>(); //XML根节点属性字典
        public List<Dictionary<string, string>> entryList = new List<Dictionary<string, string>>();//多记录
        public void createXml()
        {
            xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);
            XmlElement xmlelem = xmldoc.CreateElement("", "ufinterface", "");
            xmlelem.SetAttribute("sender", "001");
            xmlelem.SetAttribute("receiver", "u8");
            xmlelem.SetAttribute("roottag", "storein");
            xmlelem.SetAttribute("docid", "");
            xmlelem.SetAttribute("proc", "add");
            xmlelem.SetAttribute("codeexchanged", "N");
            xmlelem.SetAttribute("exportneedexch", "N");
            xmlelem.SetAttribute("display", "入库单");
            xmlelem.SetAttribute("family", "库存管理");
            xmlelem.SetAttribute("timestamp", "");
            xmldoc.AppendChild(xmlelem);

            XmlNode root = xmldoc.SelectSingleNode("ufinterface");//查找<Employees>
            XmlElement xe1 = xmldoc.CreateElement("storein");//创建一个<Node>节点               
            header = xmldoc.CreateElement("header");
            body = xmldoc.CreateElement("body");
            xe1.AppendChild(header);
            xe1.AppendChild(body);
            root.AppendChild(xe1);           
        }
        public void headerAdd()
        {
            header.AppendChild(eleAdd("id", "1000004493"));
            header.AppendChild(eleAdd("receiveflag", "1"));
            header.AppendChild(eleAdd("vouchtype", "10"));
            header.AppendChild(eleAdd("businesstype", "成品入库"));
            header.AppendChild(eleAdd("source", "库存"));
            header.AppendChild(eleAdd("businesscode", ""));
            header.AppendChild(eleAdd("warehousecode", "2"));

            header.AppendChild(eleAdd("date", DateTime.Now.ToString()));
            header.AppendChild(eleAdd("code", "0000000192"));                 //单据号
            header.AppendChild(eleAdd("sourcecodels", ""));
            header.AppendChild(eleAdd("receivecode", "12"));
            header.AppendChild(eleAdd("departmentcode", "2"));
            header.AppendChild(eleAdd("personcode", ""));
            header.AppendChild(eleAdd("purchasetypecode", ""));
            header.AppendChild(eleAdd("saletypecode", ""));
            header.AppendChild(eleAdd("customercode", ""));

            header.AppendChild(eleAdd("vendorcode", ""));
            header.AppendChild(eleAdd("ordercode", ""));
            header.AppendChild(eleAdd("quantity", ""));
            header.AppendChild(eleAdd("arrivecode", ""));
            header.AppendChild(eleAdd("billcode", ""));
            header.AppendChild(eleAdd("consignmentcode", ""));
            header.AppendChild(eleAdd("arrivedate", ""));
            header.AppendChild(eleAdd("checkcode", ""));
            header.AppendChild(eleAdd("checkdate", ""));
            header.AppendChild(eleAdd("checkperson", ""));
            header.AppendChild(eleAdd("templatenumber", "131093"));
            header.AppendChild(eleAdd("serial", ""));
            header.AppendChild(eleAdd("handler", "范磊"));
            header.AppendChild(eleAdd("memory", ""));
            header.AppendChild(eleAdd("maker", "范磊"));
            header.AppendChild(eleAdd("chandler", "范磊"));
            header.AppendChild(eleAdd("define1", ""));
            header.AppendChild(eleAdd("define2", ""));
            header.AppendChild(eleAdd("define3", ""));
            header.AppendChild(eleAdd("define4", ""));
            header.AppendChild(eleAdd("define5", ""));
            header.AppendChild(eleAdd("define6", ""));
            header.AppendChild(eleAdd("define7", ""));
            header.AppendChild(eleAdd("define8", ""));
            header.AppendChild(eleAdd("define9", ""));
            header.AppendChild(eleAdd("define10", ""));
            header.AppendChild(eleAdd("define11", ""));
            header.AppendChild(eleAdd("define12", ""));
            header.AppendChild(eleAdd("define13", ""));
            header.AppendChild(eleAdd("define14", ""));
            header.AppendChild(eleAdd("define15", ""));
            header.AppendChild(eleAdd("define16", ""));
            header.AppendChild(eleAdd("auditdate", DateTime.Now.ToString()));
            header.AppendChild(eleAdd("taxrate", ""));
            header.AppendChild(eleAdd("exchname", ""));

            header.AppendChild(eleAdd("exchrate", ""));
            header.AppendChild(eleAdd("discounttaxtype", ""));
            header.AppendChild(eleAdd("contact", ""));
            header.AppendChild(eleAdd("phone", ""));
            header.AppendChild(eleAdd("mobile", ""));
            header.AppendChild(eleAdd("address", ""));
            header.AppendChild(eleAdd("conphone", ""));
            header.AppendChild(eleAdd("conmobile", ""));
            header.AppendChild(eleAdd("deliverunit", ""));
            header.AppendChild(eleAdd("contactname", ""));
            header.AppendChild(eleAdd("officephone", ""));
            header.AppendChild(eleAdd("mobilephone", ""));
            header.AppendChild(eleAdd("psnophone", ""));
            header.AppendChild(eleAdd("psnmobilephone", ""));
            header.AppendChild(eleAdd("shipaddress", ""));
            header.AppendChild(eleAdd("addcode", ""));
            header.AppendChild(eleAdd("bomfirst", ""));
            header.AppendChild(eleAdd("bpufirst", ""));
            header.AppendChild(eleAdd("cvenpuomprotocol", ""));
            header.AppendChild(eleAdd("dcreditstart", ""));
            header.AppendChild(eleAdd("icreditperiod", ""));
            header.AppendChild(eleAdd("dgatheringdate", ""));
            header.AppendChild(eleAdd("bcredit", ""));
        }
        public void bodyAdd()
        {
            XmlNode entry = xmldoc.CreateElement("entry");
            entry.AppendChild(eleAdd("id", "1000004493"));
            entry.AppendChild(eleAdd("autoid", "1000022000"));
            entry.AppendChild(eleAdd("barcode", ""));
            entry.AppendChild(eleAdd("inventorycode", "MTVMC"));
            entry.AppendChild(eleAdd("invname", "PVC密度板橱柜吊柜门"));
            entry.AppendChild(eleAdd("free1", "YD01YG0-CG1"));
            entry.AppendChild(eleAdd("free2", "SF20088-03PCT"));
            entry.AppendChild(eleAdd("free3", ""));
            entry.AppendChild(eleAdd("free4", ""));
            entry.AppendChild(eleAdd("free5", ""));
            entry.AppendChild(eleAdd("free6", ""));
            entry.AppendChild(eleAdd("free7", ""));
            entry.AppendChild(eleAdd("free8", ""));
            entry.AppendChild(eleAdd("free9", ""));
            entry.AppendChild(eleAdd("free10", ""));
            entry.AppendChild(eleAdd("shouldquantity", ""));
            entry.AppendChild(eleAdd("shouldnumber", ""));
            entry.AppendChild(eleAdd("quantity", "1"));
            entry.AppendChild(eleAdd("cmassunitname", "套"));
            entry.AppendChild(eleAdd("assitantunit", ""));
            entry.AppendChild(eleAdd("assitantunitname", ""));
            entry.AppendChild(eleAdd("irate", ""));
            entry.AppendChild(eleAdd("number", ""));
            entry.AppendChild(eleAdd("price", "888"));
            entry.AppendChild(eleAdd("cost", "888"));
            entry.AppendChild(eleAdd("plancost", "planprice"));
            entry.AppendChild(eleAdd("serial", ""));
            entry.AppendChild(eleAdd("makedate", ""));
            entry.AppendChild(eleAdd("validdate", ""));
            entry.AppendChild(eleAdd("transitionid", ""));
            entry.AppendChild(eleAdd("subbillcode", ""));
            entry.AppendChild(eleAdd("subpurchaseid", ""));
            entry.AppendChild(eleAdd("position", ""));
            entry.AppendChild(eleAdd("itemclasscode", ""));
            entry.AppendChild(eleAdd("itemclassname", ""));
            entry.AppendChild(eleAdd("itemcode", ""));
            entry.AppendChild(eleAdd("itemname", ""));
            entry.AppendChild(eleAdd("define22", ""));
            entry.AppendChild(eleAdd("define23", ""));
            entry.AppendChild(eleAdd("define24", ""));
            entry.AppendChild(eleAdd("define25", ""));
            entry.AppendChild(eleAdd("define26", ""));
            entry.AppendChild(eleAdd("define27", ""));
            entry.AppendChild(eleAdd("define28", ""));
            entry.AppendChild(eleAdd("define29", ""));
            entry.AppendChild(eleAdd("define30", "YD01YG0001"));
            entry.AppendChild(eleAdd("define31", ""));
            entry.AppendChild(eleAdd("define32", ""));
            entry.AppendChild(eleAdd("define33", ""));
            entry.AppendChild(eleAdd("define34", ""));
            entry.AppendChild(eleAdd("define35", ""));
            entry.AppendChild(eleAdd("define36", ""));
            entry.AppendChild(eleAdd("define37", ""));
            entry.AppendChild(eleAdd("subconsignmentid", ""));
            entry.AppendChild(eleAdd("delegateconsignmentid", ""));
            entry.AppendChild(eleAdd("subproducingid", ""));
            entry.AppendChild(eleAdd("subcheckid", ""));
            entry.AppendChild(eleAdd("cRejectCode", ""));
            entry.AppendChild(eleAdd("iRejectIds", ""));
            entry.AppendChild(eleAdd("cCheckPersonCode", ""));
            entry.AppendChild(eleAdd("dCheckDate", ""));
            entry.AppendChild(eleAdd("cCheckCode", ""));
            entry.AppendChild(eleAdd("iMassDate", ""));
            entry.AppendChild(eleAdd("ioritaxcost", ""));
            entry.AppendChild(eleAdd("ioricost", ""));
            entry.AppendChild(eleAdd("iorimoney", ""));
            entry.AppendChild(eleAdd("ioritaxprice", ""));
            entry.AppendChild(eleAdd("iorisum", ""));
            entry.AppendChild(eleAdd("taxrate", ""));
            entry.AppendChild(eleAdd("taxprice", ""));
            entry.AppendChild(eleAdd("isum", ""));
            entry.AppendChild(eleAdd("massunit", ""));
            entry.AppendChild(eleAdd("vmivencode", ""));
            entry.AppendChild(eleAdd("materialfee", ""));
            entry.AppendChild(eleAdd("processcost", ""));
            entry.AppendChild(eleAdd("processfee", ""));
            entry.AppendChild(eleAdd("dmsdate", ""));
            entry.AppendChild(eleAdd("batchproperty1", ""));
            entry.AppendChild(eleAdd("batchproperty2", ""));
            entry.AppendChild(eleAdd("batchproperty3", ""));
            entry.AppendChild(eleAdd("batchproperty4", ""));
            entry.AppendChild(eleAdd("batchproperty5", ""));
            entry.AppendChild(eleAdd("batchproperty6", ""));
            entry.AppendChild(eleAdd("batchproperty7", ""));
            entry.AppendChild(eleAdd("batchproperty8", ""));
            entry.AppendChild(eleAdd("batchproperty9", ""));
            entry.AppendChild(eleAdd("batchproperty10", ""));
            entry.AppendChild(eleAdd("iexpiratdatecalcu", ""));
            entry.AppendChild(eleAdd("dexpirationdate", ""));
            entry.AppendChild(eleAdd("cexpirationdate", ""));
            entry.AppendChild(eleAdd("cbmemo", ""));
            body.AppendChild(entry);
        }
        /// <summary>
        /// 读取XML节点的所有属性并保存到字典中
        /// </summary>
        /// <param name="xmlfile">XML文件</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>KEY为属性名 VALUE为属性值</returns>
        public Dictionary<string, string> getNodeAttrs(string xmlfile,string nodeName)
        {
            //XmlTextReader xml = new XmlTextReader(xmlfile);
            if (!File.Exists(xmlfile))
                return null;
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfile);
            XmlNode xnode = xml.SelectSingleNode(nodeName);
            if (xnode == null)
                return null;
            Dictionary<string, string> Attrs = new Dictionary<string, string>();
            for (int i = 0; i<xnode.Attributes.Count;i++)
            {
                string attrName = xnode.Attributes[i].Name;
                Attrs[attrName] = xnode.Attributes[i].Value;//Dictionary[key]=value 这个方法，更安全，没有时添加，有时则修改替换,不必写个if判断
            }
            return Attrs;
        }
        /// <summary>
        /// 取节点路径下的所有子节点
        /// </summary>
        /// <param name="xmlfile">XML文件</param>
        /// <param name="nodePath">节点路径"ufinterface/storein/body/entry"</param>
        /// <returns></returns>
        public XmlNodeList getChileNodes(string xmlfile, string nodePath)
        {
            if (!File.Exists(xmlfile))
                return null;
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfile);
            XmlNodeList nodeList = xml.SelectNodes(nodePath);
            return nodeList;
        }
        /// <summary>
        /// 取节点路径下的所有子节点
        /// </summary>
        /// <param name="xmlfile">XML文件</param>
        /// <param name="nodePath">节点路径"ufinterface/storein/body/entry"</param>
        /// <returns></returns>
        public Dictionary<string, string> getNodesVal(string xmlfile, string nodePath)
        {
            if (!File.Exists(xmlfile))
                return null;
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfile);
            XmlNodeList nodeList = xml.SelectSingleNode(nodePath).ChildNodes;
            if (nodeList == null || nodeList.Count <= 0)
                return null;
            Dictionary<string, string> nodesDict = new Dictionary<string, string>();
            foreach (XmlNode node in nodeList)
            {
                nodesDict[node.Name] = node.InnerText;
            }
            return nodesDict;
        }
        /// <summary>
        /// 读取XML文件到DataSet
        /// </summary>
        /// <param name="XmlPath"></param>
        /// <returns></returns>
        public  DataSet GetXml(string XmlPath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(@XmlPath);
            return ds;
        }
        /// <summary>
        /// 把指定节点的数据填充到DataSet
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="XmlPathNode"></param>
        /// <returns></returns>
        public  DataSet GetXmlData(string xmlPath, string XmlPathNode)
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(xmlPath);
            DataSet ds = new DataSet();
            StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            ds.ReadXml(read);
            return ds;
        }
        private XmlElement eleAdd(string name,string val)
        {
            XmlElement xe1 = xmldoc.CreateElement(name);
            if (!string.IsNullOrEmpty(val))
               xe1.InnerText = val;
            //xe1.Value = val;
            return xe1;
        }
        public XmlDocument combineU8Xml(string mobanType)
        {
            XmlDocument dom = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = dom.CreateXmlDeclaration("1.0", "utf-8", null);
            dom.AppendChild(xmldecl);
            XmlElement xmlelem = dom.CreateElement("", "ufinterface", "");
            foreach (var attr in rootAttr)
            {
                xmlelem.SetAttribute(attr.Key,attr.Value);
            }
            XmlElement xe1 = dom.CreateElement(mobanType);//创建一个<Node>节点               
            header = dom.CreateElement("header");
            foreach (var node in headerDict)
            {
                header.AppendChild(eleAdd(node.Key, node.Value));
            }
            body = dom.CreateElement("body");
            for(int i =0; i< entryList.Count;i++)
            {
                Dictionary<string, string> dt = entryList[i];
                if (dt != null && dt.Count >0)
                {
                    XmlNode entry = dom.CreateElement("entry");
                    foreach (var node in dt)
                    {
                        entry.AppendChild(eleAdd(node.Key, node.Value));
                    }
                    body.AppendChild(entry);
                }
            }
            xe1.AppendChild(header);
            xe1.AppendChild(body);
            xmlelem.AppendChild(xe1);
            dom.AppendChild(xmlelem);
            return dom;
        }
        public void saveXml(string path)
        {
            //保存创建好的XML文档
            xmldoc.Save("u8xml_ruku.xml");
        }
    }
}
