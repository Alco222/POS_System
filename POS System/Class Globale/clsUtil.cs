using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System.Class_Globale
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {

            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();

        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;

        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            // Full file name. Change your file name   
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenerateGUID() + extn;

        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it will update the sourceFileName with the new name.

            string DestinationFolder = @"C:\POS-System-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = destinationFile;
            return true;
        }

        public static void LoadDataPage2(DataTable sourceTable, DataGridView dataGridView,
                               int currentPage, int pageSize,
                               Label labelInfo, Button btnPrev, Button btnNext, ref int totalRows, ref int totalPages)
        {

            try
            {
                if (sourceTable.Rows.Count == 0)
                {
                    dataGridView.DataSource = null;
                    labelInfo.Text = "0 / 0";
                    return;
                }
                // 2) Total Rows
                totalRows = Convert.ToInt32(sourceTable.Rows[0]["TotalRows"]);
                totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                // 4) تحديث Label
                labelInfo.Text = $"{currentPage} \\ {totalPages}";

                // 5) تفعيل أو تعطيل الأزرار
                btnPrev.Enabled = (currentPage > 1);
                btnNext.Enabled = (currentPage < totalPages);
            }
            catch (Exception)
            {
                dataGridView.DataSource = sourceTable.Clone();
                labelInfo.Text = "0 \\ 0";
                btnPrev.Enabled = btnNext.Enabled = false;
            }

        }


    }
}
