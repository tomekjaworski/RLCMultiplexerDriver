using MultiplexerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace MultiplexerGUI
{
    public partial class MeasurementGrid : Form
    {
        public DataTable data;
        public MeasurementType MeasurementType { get; private set; }

        double rmin, rmax;

        public MeasurementGrid()
        {
            InitializeComponent();
            this.data = null;

        }

        public DataTable PrepareDataContainer(int electrodes, MeasurementType type)
        {
            if (this.dataGridView1.DataSource != null)
                this.dataGridView1.DataSource = null;
            this.data = new DataTable();
            for (int i = 0; i < electrodes; i++)
                this.data.Columns.Add((i + 1).ToString(), typeof(Complex));

            object[] r = new object[electrodes];
            for (int i = 0; i < electrodes; i++)
                r[i] = Complex.Zero;
            for (int i = 0; i < electrodes; i++)
                this.data.Rows.Add(r);

            this.MeasurementType = type;
            this.dataGridView1.DataSource = this.data;
            this.dataGridView1.Columns[0].Visible = false;

            this.data.RowChanged += Data_RowChanged;

            this.RecalculateColors();

            return this.data;
        }

        private void Data_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            this.RecalculateColors();
            this.dataGridView1.Invalidate();

        }

        private void RecalculateColors()
        {
            rmin = double.MaxValue;
            rmax = double.MinValue;

            for (int r = 0; r < data.Rows.Count; r++)
                for (int c = 0; c < data.Columns.Count; c++)
                {
                    Complex cp = (Complex)data.Rows[r][c];
                    if (cp == Complex.Zero)
                        continue;
          
                    rmin = Math.Min(rmin, cp.Real);
                    rmax = Math.Max(rmax, cp.Real);
                }
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

            Complex value = (Complex)e.Value;

            if (value == Complex.Zero)
            {
                // puste kratki
                e.Value = "";
                return;
            }

            // wyświetlaj pojemność
            if (MeasurementType == MeasurementType.Capacitance_Parallel || MeasurementType == MeasurementType.Capacitance_Serial)
            {
                if (value.Real > 100)
                    e.Value = "ovld";
                else
                    e.Value = AgilentHelper.CapacitanceToString(value, true).Replace(" ","");

                return;
            }

            if (MeasurementType == MeasurementType.Resistance_Reactance)
            {
                e.Value = AgilentHelper.ImpedanceToString(value, true).Replace(" ", "");
                return;
            }

            throw new Exception("MeasurementType");

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

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            Complex value = (Complex)e.Value;

            if (value.Real < rmin || value.Real > rmax)
            {
                e.CellStyle.ForeColor = Color.Green;
                return;
            }

            if (rmax == rmin)
            {
                e.CellStyle.BackColor = Color.Yellow;
                return;
            }

            double coef = 1.0-(value.Real - rmin) / (rmax - rmin);
            coef = 255.0 * coef;

            Color c = Color.FromArgb(255, (int)coef, (int)coef);
            e.CellStyle.BackColor = c;
        }
    }
}
