using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace U8EAIimpRU
{
    public class u8MobanXml
    {
        private XmlDocument xmldoc;
        private XmlNode header, body,root,moban, entry;
        private string _mobanType;
        private Dictionary<string, string> _entryMoban = null; //克隆Dictionary<string, string> d1 = new Dictionary<string, string>(moban.entryMoban);
        public Dictionary<string, string> rootAttr = null; //U8 XML ufinterface节点属性字典
        public Dictionary<string, string> headerDict = null; //header子节点字典
        public List<Dictionary<string, string>> entryList = null;//body下entry节点的子节点
        public Dictionary<string, string> entryMoban
        {
            get
            {
                return _entryMoban;
            }
        }
        public string mobanType
        {
            get
            {
                return _mobanType;
            }
        }
        public u8MobanXml(string xmlfile)
        {
            xmldoc = new XmlDocument();
            xmldoc.Load(xmlfile);
            root = xmldoc.DocumentElement;
            moban = root.FirstChild;
            header = moban.SelectSingleNode("header");
            body = moban.SelectSingleNode("body");
            entry = body.FirstChild;
            getRootAttr();
            getMobanType();
            getHeaderNodes();           
            getentryMoban();
            entryList = new List<Dictionary<string, string>>();
        }
        /// <summary>
        /// 取根节点属性
        /// </summary>
        /// <param name="nodeName"></param>
        private void getRootAttr()
        {
            rootAttr = new Dictionary<string, string>();
            for (int i = 0; i < root.Attributes.Count; i++)
            {
                string attrName = root.Attributes[i].Name;
                rootAttr[attrName] = root.Attributes[i].Value;//Dictionary[key]=value 这个方法，更安全，没有时添加，有时则修改替换,不必写个if判断
            }
        }
        /// <summary>
        /// 读取XML节点的所有属性并保存到字典中
        /// </summary>
        /// <param name="xmlfile">XML文件</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>KEY为属性名 VALUE为属性值</returns>
        private Dictionary<string, string> getNodeAttrs(string nodeName)
        {
            XmlNode xnode = xmldoc.SelectSingleNode(nodeName);
            if (xnode == null)
                return null;
            Dictionary<string, string> Attrs = new Dictionary<string, string>();
            for (int i = 0; i < xnode.Attributes.Count; i++)
            {
                string attrName = xnode.Attributes[i].Name;
                Attrs[attrName] = xnode.Attributes[i].Value;//Dictionary[key]=value 这个方法，更安全，没有时添加，有时则修改替换,不必写个if判断
            }
            return Attrs;
        }
        /// <summary>
        /// 取header子节点
        /// </summary>
        private void getHeaderNodes()
        {
            headerDict = getchilNodes(header);
        }
        /// <summary>
        /// 取节点路径下的所有子节点
        /// </summary>
        /// <param name="xmlfile">XML文件</param>
        /// <param name="nodePath">节点路径"ufinterface/storein/body/entry"</param>
        /// <returns></returns>
        private Dictionary<string, string> getchilNodes(XmlNode xnode)
        {
            XmlNodeList nodeList = xnode.ChildNodes;
            if (nodeList == null || nodeList.Count <=0)
                return null;
            Dictionary<string, string> nodesDict = new Dictionary<string, string>();
            foreach (XmlNode node in nodeList)
            {
                nodesDict[node.Name] = node.InnerText;
            }
            return nodesDict;
        }
        
        private void getMobanType()
        {
            XmlNode node = xmldoc.DocumentElement;
            _mobanType = node.FirstChild.Name;
        }
        private void getentryMoban()
        {
            _entryMoban = getchilNodes(entry);
        }
        /// <summary>
        /// 创建一个节点
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private XmlElement eleAdd(XmlDocument doc,string name, string val)
        {
            XmlElement xe1 = doc.CreateElement(name);
            if (!string.IsNullOrEmpty(val))
                xe1.InnerText = val;
            return xe1;
        }
        /// <summary>
        /// 修改后合并成XML
        /// </summary>
        /// <param name="mobanType"></param>
        /// <returns></returns>
        public XmlDocument combineU8Xml()
        {
            XmlDocument dom = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = dom.CreateXmlDeclaration("1.0", "utf-8", null);
            dom.AppendChild(xmldecl);
            XmlElement rootele = dom.CreateElement("", root.Name, "");
            foreach (var attr in rootAttr)
            {
                rootele.SetAttribute(attr.Key, attr.Value);
            }
            if (string.IsNullOrEmpty(mobanType))
                getMobanType();
            XmlElement xe1 = dom.CreateElement(mobanType);//创建一个<Node>节点               
            XmlElement headerele = dom.CreateElement(header.Name);
            foreach (var node in headerDict)
            {
                headerele.AppendChild(eleAdd(dom,node.Key, node.Value));
            }
            XmlElement bodyele = dom.CreateElement(body.Name);
            for (int i = 0; i < entryList.Count; i++)
            {
                Dictionary<string, string> dt = entryList[i];
                if (dt != null && dt.Count > 0)
                {
                    XmlElement entryele = dom.CreateElement(entry.Name);
                    foreach (var node in dt)
                    {
                        entryele.AppendChild(eleAdd(dom,node.Key, node.Value));
                    }
                    bodyele.AppendChild(entryele);
                }
            }
            xe1.AppendChild(headerele);
            xe1.AppendChild(bodyele);
            rootele.AppendChild(xe1);
            dom.AppendChild(rootele);
            return dom;
        }
        public bool parseU8XmlRs(string xml,out string result)
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
            result = key +"|" + dsc;
            return rs;
        }
    }
}
/* 只生成一张单据，多张可以循环建立
 * u8MobanXml moban = new u8MobanXml("入库单_2018.08.30_10.29.09.xml");
            Dictionary<string, string> d1 = new Dictionary<string, string>(moban.entryMoban);//克隆
            moban.rootAttr["sender"] = "002";
            moban.rootAttr["roottag"] = moban.mobanType;
            moban.rootAttr["proc"] = "add";
            moban.entryList.Add(d1);
            moban.combineU8Xml().Save("入库单mod.xml");
 */
