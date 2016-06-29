using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace SecKeyTest_Folomeev.Models
{
    class IOxml
    {
        public void SaveXML(ObservableCollection<ViewModels.RecordViewModel> RecordList)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                using (XmlWriter xw =  XmlWriter.Create(saveFileDialog.FileName + ".xml"))
                {
                    xw.WriteStartElement("DataTable");

                    for (int i = 0; i < RecordList.Count; i++)
                    {
                        xw.WriteStartElement("record");
                        xw.WriteAttributeString("id", RecordList[i].Id.ToString());
                        xw.WriteAttributeString("digitalKey", RecordList[i].DigitalKey.ToString());
                        xw.WriteAttributeString("stringKey", RecordList[i].StringKey.ToString());
                        xw.WriteAttributeString("randomNumber", RecordList[i].RandomDigit.ToString());
                        xw.WriteEndElement();
                    }

                    xw.WriteEndElement();
                }

            }                
            
        }


        public ObservableCollection<ViewModels.RecordViewModel> OpenXml()
        {

            ObservableCollection<ViewModels.RecordViewModel> tempList = new ObservableCollection<ViewModels.RecordViewModel>();

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(openFileDialog.FileName);

                foreach (XmlElement element in doc.DocumentElement)
                {
                    if (element.Name == "record")
                    {
                        string tempId = element.GetAttribute("id");
                        int tempDigitalKey = Convert.ToInt32(element.GetAttribute("digitalKey"));
                        string tempStringKey = element.GetAttribute("stringKey");
                        int tempRandomDigit = Convert.ToInt32(element.GetAttribute("randomNumber"));


                        ViewModels.RecordViewModel tempElement = new ViewModels.RecordViewModel(tempId, tempStringKey, tempDigitalKey, tempRandomDigit);
                        tempList.Add(tempElement);
                    }

                }
            }

            return tempList;
        }
    }
}
