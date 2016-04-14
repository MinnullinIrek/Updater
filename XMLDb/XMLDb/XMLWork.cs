using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLDb
{
    //Для работы c xml
    public class XMLWork
    {
        public string version { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }

        
        //загрузить значения из файла
        public void SetFileName(string fileName)
        {
            Loger.Write("Загрузка данных из файла "+ fileName);
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileName);
            version= GetAttribute(xDoc,"File","FileVersion");
            name = GetValue(xDoc, "Name");
            date = DateTime.Parse(GetValue(xDoc, "DateTime"));
        }
        //получить из файла значение аттрибута attributeName элемента tag
        private string GetAttribute(XmlDocument xDoc, string tag, string attributeName )
        {
            XmlNodeList xmlList = xDoc.GetElementsByTagName(tag);
            if (xmlList.Count == 0)
                throw new Exception("Невеврен xml Файл.");
            XmlNode node = xmlList[0].Attributes.GetNamedItem(attributeName);
            return node.Value;
        }
        //получить из файла значение элемента nm
        private string GetValue(XmlDocument xDoc, string nm)
        {
            XmlNodeList xmlList = xDoc.GetElementsByTagName(nm);
            if (xmlList.Count == 0)
                throw new Exception("Невеврен xml Файл.");
            return xmlList[0].InnerText;
        }
        //сохранить xml файл
        public void SaveXmlFile(string pathToXml)
        {
            Loger.Write("Сохранение в xml файл" + pathToXml);
            XmlTextWriter textWritter = new XmlTextWriter(pathToXml, Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("File");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument document = new XmlDocument();
            document.Load(pathToXml);
            if (document.GetElementsByTagName("File").Count < 1)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка при записи xml файла");
            }
            XmlNode elementFile = document.GetElementsByTagName("File")[0];
            XmlAttribute attribute = document.CreateAttribute("FileVersion"); 
            attribute.Value = version;
            elementFile.Attributes.Append(attribute);

            XmlNode nameElement = document.CreateElement("Name");
            nameElement.InnerText = name; 
            elementFile.AppendChild(nameElement); 
            XmlNode dateElement = document.CreateElement("DateTime");
            dateElement.InnerText = date.ToString(); 
            elementFile.AppendChild(dateElement);
            document.Save(pathToXml);

        }

    }
}
