using iTextSharp.text;
using iTextSharp.text.pdf; 
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace NormalAd
{
    public partial class ReportForm : Form
    {
        private string connStr = "server=localhost;database=ad;uid=root;pwd=2002;";

        /// <summary>Initializes a new instance of the <see cref="ReportForm" /> class.</summary>
        public ReportForm()
        {
            InitializeComponent();
            
        }

        /// <summary>Handles the Load event of the ReportForm control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ReportForm_Load(object sender, EventArgs e)
        {
            InitializeReportTypeComboBox();
            cmbReportType.SelectedIndex = 0;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
           
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
          
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
        }

        private void dataGridViewReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>Handles the 1 event of the btnGenerateReport_Click control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        
            private void btnGenerateReport_Click_1(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedIndex <= 0) // index 0 is --SELECT--
            {
                MessageBox.Show("Please select a valid report type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reportType = cmbReportType.SelectedItem.ToString();
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;

            string query = "";

            switch (reportType)
            {
                case "Service History":
                    query = @"
                        SELECT RID, CustomerID, PickupLocation, DeliveryLocation, Item_Type, Quantity, Status, ServiceDate 
                        FROM service_request 
                        WHERE ServiceDate BETWEEN @From AND @To";
                    break;

                case "Customer Records":
                    query = @"
                        SELECT *
                        FROM customer_registration 
                        WHERE RegisteredDate BETWEEN @From AND @To";
                    break;

                case "Employee Records":
                    query = @"
                        SELECT EmpID, Name, JobRole, Contact, HiredDate 
                        FROM emp_registration 
                        WHERE HiredDate BETWEEN @From AND @To";
                    break;

                case "Vehicle Records":
                    query = @"
                        SELECT VID, Vehicle_Number, Vehicle_Type, CCapacity, Status, RegisteredDate 
                        FROM vehicle_registration 
                        WHERE RegisteredDate BETWEEN @From AND @To";
                    break;

                case "Assigning Records":
                    query = @"
                        SELECT AssignmentID, RID, VID, DriverName, AssistantName, ContainerID, AssignedDate, Status 
                        FROM load_assignment 
                        WHERE AssignedDate BETWEEN @From AND @To";
                    break;
            }

            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@From", fromDate);
                        cmd.Parameters.AddWithValue("@To", toDate);

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("No data found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            dataGridViewReport.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportPdf_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Save report as PDF"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 20f, 20f, 20f, 20f);
                PdfWriter.GetInstance(pdfDoc, new FileStream(sfd.FileName, FileMode.Create));
                pdfDoc.Open();

                // Company name
                Paragraph company = new Paragraph("e-Shift", FontFactory.GetFont("Arial", 18, BaseColor.BLUE))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                pdfDoc.Add(company);

                // Report type as title
                string reportType = cmbReportType.SelectedItem?.ToString() ?? "Report";
                Paragraph title = new Paragraph($"{reportType} Report", FontFactory.GetFont("Arial", 16))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                pdfDoc.Add(title);

                // Generated on date
                Paragraph generatedOn = new Paragraph($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", FontFactory.GetFont("Arial", 10))
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                pdfDoc.Add(generatedOn);

                pdfDoc.Add(new Paragraph(" ")); // empty line

                PdfPTable pdfTable = new PdfPTable(dataGridViewReport.Columns.Count)
                {
                    WidthPercentage = 100
                };

                // Add headers
                foreach (DataGridViewColumn column in dataGridViewReport.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont("Arial", 10)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY
                    };
                    pdfTable.AddCell(cell);
                }

                // Add rows
                foreach (DataGridViewRow row in dataGridViewReport.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        pdfTable.AddCell(cell.Value?.ToString() ?? "");
                    }
                }

                pdfDoc.Add(pdfTable);
                pdfDoc.Close();

                MessageBox.Show("PDF report saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportExcel_Click_1(object sender, EventArgs e)
        {

            if (dataGridViewReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Save report as Excel"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                Microsoft.Office.Interop.Excel._Worksheet worksheet = excelApp.ActiveSheet;

                int rowIndex = 1;

                // Add company name
                worksheet.Cells[rowIndex, 1] = "e-Shift";
                worksheet.Range[worksheet.Cells[rowIndex, 1], worksheet.Cells[rowIndex, dataGridViewReport.Columns.Count]].Merge();
                worksheet.Cells[rowIndex, 1].Font.Size = 16;
                worksheet.Cells[rowIndex, 1].Font.Bold = true;
                worksheet.Cells[rowIndex, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                rowIndex++;

                // Report type
                string reportType = cmbReportType.SelectedItem?.ToString() ?? "Report";
                worksheet.Cells[rowIndex, 1] = $"{reportType} Report";
                worksheet.Range[worksheet.Cells[rowIndex, 1], worksheet.Cells[rowIndex, dataGridViewReport.Columns.Count]].Merge();
                worksheet.Cells[rowIndex, 1].Font.Size = 14;
                worksheet.Cells[rowIndex, 1].Font.Bold = true;
                worksheet.Cells[rowIndex, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                rowIndex++;

                // Generated on
                worksheet.Cells[rowIndex, 1] = $"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                worksheet.Range[worksheet.Cells[rowIndex, 1], worksheet.Cells[rowIndex, dataGridViewReport.Columns.Count]].Merge();
                worksheet.Cells[rowIndex, 1].Font.Italic = true;
                worksheet.Cells[rowIndex, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

                rowIndex += 2;

                // Table headers
                for (int i = 0; i < dataGridViewReport.Columns.Count; i++)
                {
                    worksheet.Cells[rowIndex, i + 1] = dataGridViewReport.Columns[i].HeaderText;
                    worksheet.Cells[rowIndex, i + 1].Font.Bold = true;
                    worksheet.Cells[rowIndex, i + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                }

                rowIndex++;

                // Table rows
                for (int i = 0; i < dataGridViewReport.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridViewReport.Columns.Count; j++)
                    {
                        worksheet.Cells[rowIndex + i, j + 1] = dataGridViewReport.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }

                // Auto-fit columns
                worksheet.Columns.AutoFit();

                // Save
                excelApp.ActiveWorkbook.SaveAs(sfd.FileName);
                excelApp.ActiveWorkbook.Close();
                excelApp.Quit();

                MessageBox.Show("Excel report saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InitializeReportTypeComboBox()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.AddRange(new string[]
            {
                "--SELECT--",
                "Service History",
                "Customer Records",
                "Employee Records",
                "Vehicle Records",
                "Assigning Records"
            });
        }


        private void dataGridViewReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
