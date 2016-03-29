using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiplexerGUI
{
    public partial class MeasurementGrid : Form
    {
        public DataTable data;

        public MeasurementGrid()
        {
            InitializeComponent();
            this.data = null;
        }

        public DataTable PrepareDataContainer(int electrodes)
        {
            if (this.dataGridView1.DataSource != null)
                this.dataGridView1.DataSource = null;
            this.data = new DataTable();
            for (int i = 0; i < electrodes; i++)
                this.data.Columns.Add((i + 1).ToString(), typeof(double));

            object[] r = new object[electrodes];
            for (int i = 0; i < electrodes; i++)
                r[i] = (double)0;
            for (int i = 0; i < electrodes; i++)
                this.data.Rows.Add(r);

            this.dataGridView1.DataSource = this.data;
            this.dataGridView1.Columns[0].Visible = false;
            return this.data;
        }
        private void dgGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            StringFormat centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            //e.Value = 2e-4;
            int pow = 0;
            double cap = (double)e.Value;
            if (cap == 0)
            {
                e.Value = "";
                return;
            }

            if (cap > 100)
                e.Value = "ovld";
            else
            {
                string[] s = { "", "m", "u", "n", "p", "f", "a" };

                while ((int)cap == 0 && cap != 0)
                {
                    pow++;
                    cap = cap * Math.Pow(10, 3);
                }
                e.Value = cap.ToString("N2") + s[pow] + "F";
            }

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.Width = 55;
        }

        private void MeasurementGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
