using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MathModelApp
{
    public partial class EditFormulars : Form
    {
        public EditFormulars()
        {
            InitializeComponent();
        }

        private void ChangeSelect(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                OpenXmlForDataView("FormularP2");
                string[] array = { 
                                   "j",
                                   "Наименование j-го средства ближнего боя",
                                   "Вероятность поражения противника одним выстрелом в наст.",
                                   "Вероятность поражения противника одним выстрелом в обороне.",
                                   "Боевая скорострельность (выстр.в час) в наст.",
                                   "Боевая скорострельность (выстр.в час) в обороне",
                                   "Дальность эффективной стрельбы в наст.",
                                   "Дальность эффективной стрельбы в оборне"
                                 };
                EditForRusNameColums(array);
                
            }
            if (comboBox1.SelectedIndex == 1)
            {
                OpenXmlForDataView("FormularP3");
                string[] array = {
                                 "i",
                                 "Наименование типа средства поражения( ТЗ СВ РФ и ино-странных армий)",
                                 "Коэффициент соизмер. с ЕРБ",
                                 "Размер боекомпл."
                                 };
                EditForRusNameColums(array);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                OpenXmlForDataView("FormularP4");
                string[] array = {
                                 "i",
                                 "Наименование объектов(ТЗ СВ РФ и иностр. армий )",
                                 "Треб. кол. ЕРБ для подавления объектов, находящихся в обороне",
                                 "Треб. кол. ЕРБ для подавления объектов, находящихся в наступлении"
                                 };
                EditForRusNameColums(array);
            }
            if (comboBox1.SelectedIndex == 3)
            {
                OpenXmlForDataView("FormularP5");
                string[] array = {
                                 "i - Порядковый номер подразделения",
                                 "j - Порядковыйномер орудия",
                                 "Условное наимен. (тип) ОТФ РФ",
                                 "Штатное количество средств ближнего боя в ОТФ ВС РФ"
                                 };
                EditForRusNameColums(array);
            }
            if (comboBox1.SelectedIndex == 4)
            {
                OpenXmlForDataView("FormularP6");
                string[] array = {
                                 "i - Порядковый номер подразделения",
                                 "j - Порядковыйномер орудия",
                                 "Условное наимен. (тип) ОТФ РФ",
                                 "Штатное количество средств ближнего боя в ОТФ иностранных армий"
                                 };
                EditForRusNameColums(array);
            }
            if (comboBox1.SelectedIndex == 5)
            {
                OpenXmlForDataView("FormularP81");
                string[] array = {
                                 "i - Порядковый номер подразделения",
                                 "Наименование (тип) ОТФ ",
                                 "Укомплектованность"
                                 };
                EditForRusNameColums(array);
            }
            if (comboBox1.SelectedIndex == 6)
            {
                OpenXmlForDataView("FormularP82");
                string[] array = {
                                 "i - Порядковый номер подразделения",
                                 "Наименование (тип) ОТФ ",
                                 "Укомплектованность"
                                 };
                EditForRusNameColums(array);
            }
            if (comboBox1.SelectedIndex == 8)
            {
                OpenXmlForDataView("FormularP1");
                string[] array = {
                                 "Вероятность поражения противника одним выстрелом для танка обороняющ.",
                                 "Вероятность поражения противника одним выстрелом для танка Атакующих",
                                 "Расчетная скорострельность танка обороняющ.",
                                 "Расчетная скорострельность для танка Атакующих",
                                 "Дальность эффективной стрельбы для танка обороняющ.",
                                 "Дальность эффективной стрельбы для танка Атакующих",
                                 "Время нахождения в обороне",
                                 "Скорость движения атакующего танка",
                                 "Укомплектованность подразделения для танка Атакующих",
                                 "Укомплектованность подразделенияля танка обороняющ."
                                 
                                 };
                EditForRusNameColums(array);
            }         
        }

        string Path; //Путь к формуляру
        private void OpenXmlForDataView (string XmlName)
        {
            Path = "xml/" + XmlName + ".xml";
            ResetDatagridview();
        }

        private void ResetDatagridview()
        {
            DataSetFormular.Reset();
            DataSetFormular.ReadXml(Path);
            dataGridView1.DataSource = DataSetFormular.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }



        private void EditForRusNameColums(string[] RusName)  //Русифицирует колонки из упорядоченного массива
        {
           for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {                
                dataGridView1.Columns[i].HeaderText = RusName[i];
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right) return;         

        }


        private void Endeditcells(object sender, DataGridViewCellEventArgs e) //сохраняю изменения после редактирования ячейки
        {
            DataSetFormular.WriteXml(Path);
            dataGridView1.DataSource = DataSetFormular.Tables[0];            
        }

        private void добавитьСтрокуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функция отключена");
        }

        private void удаитьСтрокуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }



    }
}
