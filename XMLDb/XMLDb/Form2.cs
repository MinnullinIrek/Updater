using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace XMLDb
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void xMLTableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.xMLTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database1DataSet.XMLTable". При необходимости она может быть перемещена или удалена.
            this.xMLTableTableAdapter.Fill(this.database1DataSet.XMLTable);
            
        }

        //открыть файл
        private void button1_Click(object sender, EventArgs e)
        {
            Loger.Write("Всталяем данные из файла в бд.");
            openFileDialog1.Filter = "XML files(*.xml)|*.xml";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res != DialogResult.OK)
            {
                return;
            }
            string fileName = openFileDialog1.FileName;
            string checkFileName = openFileDialog1.SafeFileName.Replace(".xml", "");

            if (!CheckFileName(checkFileName))
            {
                return;
            }
            Loger.Write("Создаем класс для работы с xml");
            XMLWork xml = new XMLWork();
            xml.SetFileName(fileName);
            AddToDb(xml);
        }
        //регулярное выражение
        string regString = @"^[а-я]{1,100}_[0-9]{1}_.{0,7}$|^[а-я]{1,3}_[0-9]{10}_.{0,7}$|^[а-я]{1,3}_[0-9]{14,20}_.{0,7}$";
        //проверка имени файла
        private bool CheckFileName(string fileName)
        {
            Loger.Write("проверка имени файла на правильность. имя - "+ fileName);
            Regex regex = new Regex(regString, RegexOptions.IgnoreCase);
            if (!regex.IsMatch(fileName))
            {
                Loger.Write("проверка имени файла на правильностьне прошла. имя - " + fileName);

                return false;
            }
            Loger.Write("проверка имени файла на правильность прошла. имя - " + fileName);

            return true;
            
        }
        //добавить в бд
        private void AddToDb(XMLWork xml)
        {
            Loger.Write("Добавление данных в бд." );

            DataRow workRow = database1DataSet.Tables[0].NewRow();
            workRow["Name"] = xml.name;
            workRow["version"] = xml.version;
            workRow["date"] = xml.date;
            database1DataSet.Tables[0].Rows.Add(workRow);

            this.xMLTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);
        }
        //сохранить
        private void button2_Click(object sender, EventArgs e)
        {
            Loger.Write("Создание xml файла из текущей строки." );

            if (nameTextBox.Text.Length < 1 ||versionTextBox.Text.Length < 1)
            {
                MessageBox.Show("Заполните поля");
                return;
            }
            if (!CheckFileName(nameTextBox.Text))
            {
                MessageBox.Show("Неверное имя файла");
            }

            saveFileDialog1.FileName = nameTextBox.Text;
            saveFileDialog1.Filter = "XML files(*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Ошибка при сохранении");
                return;
            }

            string path = saveFileDialog1.FileName;

            XMLWork xml = new XMLWork();
            xml.name = nameTextBox.Text;
            xml.version = versionTextBox.Text;
            xml.date = dateDateTimePicker.Value;
            Loger.Write(string.Format("Name [{0}], FileVersion[{1}], DateTime[{2}]", xml.name, xml.version, xml.date.ToString()));
            xml.SaveXmlFile(path);
            
        }
    }
}
