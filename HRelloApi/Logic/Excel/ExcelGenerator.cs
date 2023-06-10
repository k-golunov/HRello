using System.Drawing;
using System.Runtime.CompilerServices;
using Dal.Tasks.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Logic.Excel;

public class ExcelGenerator
{
    private readonly List<string> TaskHeadres = new List<string>
    {
        "Квартал/ период",
        "ФИО сотрудника",
        "Блок задачи",
        "Категория задачи",
        "Задача (краткое описание задачи)",
        "Какой результат хотим получить по итогу квартала (как оценим, что задача выполнена, желательно измеримый результат)",
        "Планируемый вес задачи, %",
        "Какой результат достигнут",
        "Фактический вес  по итогу квартала, %",
        "% выполнения - оценка сотрудника",
        "Комментарий  сотрудника",
        "% выполнения - оценка руководителя",
        "Комментарий  руководителя",
        "Комментарий Заказчика",
        "Сумма премии"
    };
    
    public byte[] GenerateTasksReport(List<TaskDal> tasks)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Бланк Цели");

        for (var i = 0; i < TaskHeadres.Count; i++)
        {
            sheet.Cells[1, i+1].Value = TaskHeadres[i];
        }
        
        sheet.Cells[1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 8].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
        sheet.Cells[1, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 9].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
        sheet.Cells[1, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 10].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
        sheet.Cells[1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 11].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
        sheet.Cells[1, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 12].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        sheet.Cells[1, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 13].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        sheet.Cells[1, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
        sheet.Cells[1, 14].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

        for (var i = 2; i < tasks.Count+2; i++)
        {
            sheet.Cells[i, 1].Value = tasks[i-2].Quarter;
            sheet.Cells[i, 2].Value = string.Join(" ", tasks[i-2].User.Surname, tasks[i-2].User.Name, tasks[i-2].User.Patronymic);
            sheet.Cells[i, 3].Value = tasks[i-2].Block.Value;
            sheet.Cells[i, 4].Value = tasks[i-2].Category.ToString();
            sheet.Cells[i, 5].Value = tasks[i-2].Name;
            sheet.Cells[i, 6].Value = tasks[i-2].WaitResult;
            sheet.Cells[i, 7].Style.Numberformat.Format = "###%";
            sheet.Cells[i, 7].Value = tasks[i-2].PlannedWeight * 0.01;
            sheet.Cells[i, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[i, 8].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
            sheet.Cells[i, 8].Value = tasks[i-2].UserTaskResultDal?.Result;
            sheet.Cells[i, 9].Style.Numberformat.Format = "###%";
            sheet.Cells[i, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[i, 9].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
            sheet.Cells[i, 9].Value = tasks[i-2].UserTaskResultDal?.FactResult * 0.01;
            sheet.Cells[i, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[i, 10].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
            sheet.Cells[i, 10].Style.Numberformat.Format = "###%";
            sheet.Cells[i, 10].Value = tasks[i-2].UserTaskResultDal?.FactWeight * 0.01;
            sheet.Cells[i, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[i, 11].Style.Fill.BackgroundColor.SetColor(Color.Aqua);
            sheet.Cells[i, 11].Value = tasks[i-2].UserTaskResultDal?.Description;
            sheet.Cells[i, 12].Style.Numberformat.Format = "###%";
            sheet.Cells[i, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[i, 12].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            sheet.Cells[i, 12].Value = tasks[i-2].BossTaskResultDal?.Result * 0.01;
            sheet.Cells[i, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[i, 13].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            sheet.Cells[i, 13].Value = tasks[i-2].BossTaskResultDal?.Comment;
            sheet.Cells[i, 14].Value = "";
            sheet.Cells[i, 15].Value = (tasks[i-2].UserTaskResultDal?.FactWeight * tasks[i-2].BossTaskResultDal?.Result) * 0.01;
        }

        return package.GetAsByteArray();
    }
}