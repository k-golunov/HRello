using System.Drawing;
using Dal.TaskResult.Entities;
using Dal.TaskResult.Enums;
using Dal.Tasks.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Logic.Excel;

public static class ResultExcelGenerator
{
    private static readonly Dictionary<ColorEnum, Color> Colors = new()
    {
        { ColorEnum.Green, Color.FromArgb(1, 169, 242, 111) },
        { ColorEnum.Red, Color.FromArgb(1, 255, 249, 100) },
        { ColorEnum.Yellow, Color.FromArgb(1, 242, 127, 111) }
    };

    private static readonly int x = 2;
    private static readonly int y = 4;
    private static readonly double height = 80;
    private static readonly double width = 30;
    public static byte[] GenerateTasksReport(List<TaskResultDal> results, int year, List<int> quaters)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Итоги");
        SetDefaultSettingsForSheet(sheet);
        WriteCompanyName(sheet);
        WriteTitle(year, quaters, sheet);
        sheet.Columns[x - 1].Width = width;
        var groupedResults = results.GroupBy(x => x.Tasks.First().Block.Value).ToList();
        for (var i = y; i < groupedResults.Count + y; i++)
        {
            var blockCell = sheet.Cells[i, x - 1];
            blockCell.Value = groupedResults[i - y].Key;
            blockCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            sheet.Rows[i].Height = height;
            WriteTaskResultForBlock(groupedResults[i - y], i, sheet);
        }
        return package.GetAsByteArray();
    }

    private static void WriteTaskResultForBlock(IGrouping<string, TaskResultDal> block, int i, ExcelWorksheet sheet)
    {
        for (var j = x; j < block.Count() + x; j++)
        {
            var cell = sheet.Cells[i, j];
            cell.Value = block.ToList()[j - x].Result;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(Colors[block.ToList()[j - x].Color]);
            cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            sheet.Columns[j].Width = width;
        }
    }

    private static void SetDefaultSettingsForSheet(ExcelWorksheet sheet)
    {
        sheet.Cells.Style.WrapText = true;
        sheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
    }

    private static void WriteTitle(int year, List<int> quaters, ExcelWorksheet sheet)
    {
        var title = sheet.Cells[y - 1, 1, y - 1, 3];
        title.Merge = true;
        title.Value = $"Итоги за {year} год и {String.Join(", ", quaters)} квартал(ы)";
        title.Style.Font.Bold = true;
    }

    private static void WriteCompanyName(ExcelWorksheet sheet)
    {
        var A1 = sheet.Cells[1, 1, 1, 2];
        A1.Merge = true;
        A1.Value = "USSC x UDV";
        A1.Style.Font.Bold = true;
        A1.Style.Fill.PatternType = ExcelFillStyle.Solid;
        A1.Style.Fill.BackgroundColor.SetColor(Color.Coral);
        sheet.Rows[1].Height = 20;
    }
}