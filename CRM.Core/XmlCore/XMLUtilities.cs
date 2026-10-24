using System.Collections;
using System.IO;
using System.Xml;

namespace CRM.Core.XmlCore {
    public class XMLUtilities {
        public static XmlElement getFirstElement(XmlElement elemParent) {
            ArrayList subElements = XMLUtilities.getSubElements(elemParent);
            if (subElements.Count > 0) {
                return (XmlElement)subElements[0];
            }
            return null;
        }

        public static ArrayList getSubElements(XmlElement element) {
            ArrayList result;
            try {
                ArrayList arrayList = new ArrayList();
                if (element != null && element.HasChildNodes) {
                    for (XmlNode xmlNode = element.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling) {
                        if (xmlNode.NodeType == XmlNodeType.Element) {
                            XmlElement xmlElement = (XmlElement)xmlNode;
                            xmlElement.GetAttribute("code");
                            arrayList.Add(xmlElement);
                        }
                    }
                }
                arrayList.TrimToSize();
                result = arrayList;
            }
            catch {
                result = null;
            }
            return result;
        }

        public static XmlElement getElementByPath(XmlElement elemParent, string path) {
            XmlElement xmlElement = elemParent;
            try {
                string name = xmlElement.InnerXml;
                while (path.Length > 0) {
                    int num = path.IndexOf("\\");
                    if (num > 0) {
                        name = path.Substring(0, num);
                        path = path.Substring(num + 1);
                    }
                    else {
                        name = path;
                        path = "";
                    }
                    xmlElement = XMLUtilities.getFirstElementByNameOneLevel(xmlElement, name);
                    if (xmlElement == null) {
                        break;
                    }
                }
            }
            catch {
            }
            return xmlElement;
        }

        public static XmlElement getFirstElementByName(XmlElement element, string name) {
            try {
                if (element != null) {
                    XmlNodeList elementsByTagName = element.GetElementsByTagName(name);
                    if (elementsByTagName.Count > 0 && elementsByTagName.Item(0).NodeType == XmlNodeType.Element) {
                        return (XmlElement)elementsByTagName.Item(0);
                    }
                }
            }
            catch {
            }
            return null;
        }

        public static XmlElement getFirstElementByNameOneLevel(XmlElement elemParent, string name) {
            try {
                if (elemParent != null) {
                    foreach (XmlElement xmlElement in XMLUtilities.getSubElements(elemParent)) {
                        if (xmlElement.Name.Equals(name)) {
                            return xmlElement;
                        }
                    }
                }
            }
            catch {
            }
            return null;
        }

        public static XmlElement insertElement(XmlElement elemParent, string name) {
            XmlElement result;
            try {
                XmlDocument ownerDocument = elemParent.OwnerDocument;
                XmlElement newChild = ownerDocument.CreateElement(name);
                result = (XmlElement)elemParent.AppendChild(newChild);
            }
            catch {
                result = null;
            }
            return result;
        }

        public static string XmlDocumentToString(XmlDocument doc) {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = Formatting.Indented;
            doc.WriteContentTo(xmlTextWriter);
            xmlTextWriter.Flush();
            return stringWriter.ToString();
        }

        public static XmlElement addElement(XmlElement parent, string elementName) {
            if (parent == null || elementName == "") {
                return null;
            }
            XmlElement result;
            try {
                result = (XmlElement)parent.AppendChild(parent.OwnerDocument.CreateElement(elementName));
            }
            catch {
                result = null;
            }
            return result;
        }

        public static void clearChild(XmlElement parent) {
            try {
                if (parent != null) {
                    for (XmlNode firstChild = parent.FirstChild; firstChild != null; firstChild = parent.FirstChild) {
                        parent.RemoveChild(firstChild);
                    }
                }
            }
            catch {
            }
        }

        public static void addAttribute(XmlElement target, string attName, string attValue) {
            try {
                if (target != null && attName.Length >= 1 && attValue.Length >= 1) {
                    target.Attributes.Append(target.OwnerDocument.CreateAttribute(attName));
                    target.SetAttribute(attName, attValue);
                }
            }
            catch {
            }
        }

    }
}
