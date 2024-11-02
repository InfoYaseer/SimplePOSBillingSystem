using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;


namespace POSBillingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        private float grandTotal = 0;
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtItemId.Focus();
            dgvItems.Columns[5].DefaultCellStyle.Format = "0.00";
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string itemID = txtItemId.Text;
            string itemName = txtItemName.Text;
            int quantity = int.TryParse(txtQty.Text, out int q) ? q : 0;
            float unitPrice = float.TryParse(txtUnitPrice.Text, out float up) ? up : 0;
            float discount = float.TryParse(txtDiscount.Text, out float dis) ? dis : 0;
            float itemTotal = (float)Math.Round(quantity * unitPrice * (1 - discount / 100), 2);
            dgvItems.Rows.Add(itemID, itemName, quantity, unitPrice, discount, itemTotal);
            grandTotal += itemTotal;
            lblGrandTotal.Text = $"Grand Total: {grandTotal.ToString("C",new CultureInfo("si-LK"))}";
            txtItemId.Clear();
            txtItemName.Clear();
            txtQty.Clear();
            txtUnitPrice.Clear();
            txtDiscount.Clear();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvItems.Rows.Clear();
            grandTotal = 0;
            lblGrandTotal.Text = $"Grand Total is:{grandTotal.ToString("C",new CultureInfo("si-LK"))}";


        }

        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 992 , 699);
            previewDialog.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float ypos = 10;
            int leftMargin = e.MarginBounds.Left;
            int rightMargin = e.MarginBounds.Right;
            Font headerFont = new Font("Montserrat", 12, FontStyle.Bold);
            Font regularFont = new Font("Montserrat", 10);
            e.Graphics.DrawString("Invoice", headerFont, Brushes.Black, leftMargin, ypos);
            ypos += 30;
            e.Graphics.DrawString("Item ID", regularFont, Brushes.Black, leftMargin, ypos);
            e.Graphics.DrawString("Name", regularFont, Brushes.Black, leftMargin + 100, ypos);
            e.Graphics.DrawString("Qty", regularFont, Brushes.Black, leftMargin + 250, ypos);
            e.Graphics.DrawString("Unit Price", regularFont, Brushes.Black, leftMargin + 300, ypos);
            e.Graphics.DrawString("Discount", regularFont, Brushes.Black, leftMargin + 400, ypos);
            e.Graphics.DrawString("Total", regularFont, Brushes.Black, leftMargin + 500, ypos);
            ypos += 25;
            e.Graphics.DrawLine(Pens.Black, leftMargin, ypos, rightMargin, ypos);
            ypos += 5;

            foreach(DataGridViewRow row in dgvItems.Rows)
            {
                if(row.Cells[0].Value !=null)
                {
                    e.Graphics.DrawString(row.Cells[0].Value.ToString(), regularFont, Brushes.Black, leftMargin, ypos);
                    e.Graphics.DrawString(row.Cells[1].Value.ToString(), regularFont, Brushes.Black, leftMargin+100, ypos);
                    e.Graphics.DrawString(row.Cells[2].Value.ToString(), regularFont, Brushes.Black, leftMargin+250, ypos);
                    e.Graphics.DrawString(row.Cells[3].Value.ToString(), regularFont, Brushes.Black, leftMargin+300, ypos);
                    e.Graphics.DrawString(row.Cells[4].Value.ToString(), regularFont, Brushes.Black, leftMargin+400, ypos);
                    e.Graphics.DrawString(row.Cells["TotalPrice"].Value.ToString(), regularFont, Brushes.Black, leftMargin+500, ypos);
                    ypos += 25;


                }
            }
            ypos += 10;
            e.Graphics.DrawLine(Pens.Black, leftMargin, ypos, rightMargin, ypos);
            ypos += 10;
            e.Graphics.DrawString("Grand Total:", headerFont, Brushes.Black, leftMargin + 350, ypos);
            e.Graphics.DrawString(grandTotal.ToString("C", new System.Globalization.CultureInfo("Si-LK")), headerFont, Brushes.Black, leftMargin + 500, ypos);
        }

        private void txtItemId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtItemName.Focus();
            }
        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtQty.Focus();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtUnitPrice.Focus();
            }
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtDiscount.Focus();
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAddItem.PerformClick();
                txtItemId.Focus();
            }
        }
    }
}
