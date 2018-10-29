using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace ExportDemo
{
    public static class ExcelHelper
    {
        public static void Export(string file = @"helloworld.xlsx")
        {
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();

                var sheet = workbook.CreateSheet("SheetA");

                var row = sheet.CreateRow(0);
                var cell = row.CreateCell(0);
                cell.SetCellValue("Hello world");

                workbook.Write(fs);
            }
        }
    }
}
