using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Создание массивов и безопасное считывание данных из TextBox
            double[] f = new double[5];  // Депозиты
            double[] f1 = new double[5]; // Кредиты

            // Метод double.TryParse защищает от ошибок при пустых полях или вводе точки вместо запятой
            double.TryParse(textBox1.Text.Replace('.', ','), out f[0]);
            double.TryParse(textBox2.Text.Replace('.', ','), out f[1]);
            double.TryParse(textBox3.Text.Replace('.', ','), out f[2]);
            double.TryParse(textBox4.Text.Replace('.', ','), out f[3]);
            double.TryParse(textBox5.Text.Replace('.', ','), out f[4]);

            double.TryParse(textBox6.Text.Replace('.', ','), out f1[0]);
            double.TryParse(textBox7.Text.Replace('.', ','), out f1[1]);
            double.TryParse(textBox8.Text.Replace('.', ','), out f1[2]);
            double.TryParse(textBox9.Text.Replace('.', ','), out f1[3]);
            double.TryParse(textBox10.Text.Replace('.', ','), out f1[4]);

            // 2. Очистка прошлых данных гистограммы и настройка типов
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            chart1.Series[0].YValueType = ChartValueType.Double;
            chart1.Series[1].YValueType = ChartValueType.Double;

            // 3. Массив с именами филиалов и построение гистограммы
            string[] branches = { "Филиал 1", "Филиал 2", "Филиал 3", "Филиал 4", "Филиал 5" };

            for (int i = 0; i < 5; i++)
            {
                int index0 = chart1.Series[0].Points.AddXY(branches[i], f[i]);
                int index1 = chart1.Series[1].Points.AddXY(branches[i], f1[i]);

                chart1.Series[0].Points[index0].Label = f[i].ToString("0");
                chart1.Series[1].Points[index1].Label = f1[i].ToString("0");
            }

            // 4. Заполнение DataGridView
            dataGridView1.RowCount = 2;
            dataGridView1.ColumnCount = 5;

            dataGridView1.Rows[0].HeaderCell.Value = "Депозиты";
            dataGridView1.Rows[1].HeaderCell.Value = "Кредиты";
            dataGridView1.RowHeadersWidth = 100;

            for (int i = 0; i < 5; i++)
            {
                dataGridView1.Columns[i].Width = 70;
                dataGridView1.Columns[i].HeaderText = branches[i];
                dataGridView1.Rows[0].Cells[i].Value = f[i];
                dataGridView1.Rows[1].Cells[i].Value = f1[i];
            }

            // 5. ВЫПОЛНЕНИЕ САМОСТОЯТЕЛЬНОГО ЗАДАНИЯ
            double maxDiff = Math.Abs(f[0] - f1[0]);
            double minDiff = Math.Abs(f[0] - f1[0]);
            int maxBranchIndex = 0;
            int minBranchIndex = 0;
            double totalSum = 0;

            for (int i = 0; i < 5; i++)
            {
                // Разница прибылей текущего филиала по модулю
                double currentDiff = Math.Abs(f[i] - f1[i]);

                if (currentDiff > maxDiff)
                {
                    maxDiff = currentDiff;
                    maxBranchIndex = i;
                }

                if (currentDiff < minDiff)
                {
                    minDiff = currentDiff;
                    minBranchIndex = i;
                }

                // Суммируем общую прибыль филиала (депозиты + кредиты)
                totalSum += (f[i] + f1[i]);
            }

            // Вычисляем среднее арифметическое
            double averageProfit = totalSum / 5;

            // Выводим результат в Label (убедитесь, что labelResult добавлен на форму)
            labelResult.Text = $"Макс. разница: {branches[maxBranchIndex]} ({maxDiff})\n" +
                               $"Мин. разница: {branches[minBranchIndex]} ({minDiff})\n" +
                               $"Средняя прибыль филиала: {averageProfit:F2}";
        }
    }
}
