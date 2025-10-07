using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using ReportGeneration_Kataev.Pages;
using Exel = Microsoft.Office.Interop.Excel;

namespace ReportGeneration_Kataev.Classes.Common
{
    public class Report
    {
        public static void Group(int IdGroup, Main Main)
        {
            SaveFileDialog SFD = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Filter = "Excel (*.xlsx)| *.xlsx"
            };
            SFD.ShowDialog();
            if (SFD.FileName != "")
            {
                GroupContext Group = Main.AllGroups.Find(x => x.Id == IdGroup);
                var ExcelApp = new Excel.Application();
                try
                {
                    ExcelApp.Visible = false;
                    Excel.WorlBook WorkBook = ExcellApp.WorkBooks.Add(Type.Missing);
                    Excel.WorkSheet WorkSheet = WorkBook.ActiveSheet;
                    (Worksheet.Cells[1, 1] as Excel.Range).Value = $"Отчёт о группе {Group.Name}";
                    Worksheet.Range[WorkSheet.Cells[1, 1], Worksheet.Cells[1, 5]].Merge();
                    Styles(Worksheet.Cells[1, 1], 18);
                    (Worksheet.Cells[3, 1] as Excel.Range).Value = $"Список группы:";
                    Worksheet.Range[Worksheet.Cells[3, 1], Worksheet.Cells[3, 5]].Merge();
                    Styles(Worksheet.Cells[3, 1], 12, Excel.XLHA1ign.xLHA1ignLeft);

                    (Worksheet.Cells[4, 1] as Excel.Range).Value = $"ФИО";
                    Styles(Worksheet.Cells[4, 1], 12, XLHA1ign.xLHA1ignCenter, true);
                    (Worksheet.Cells[4, 1] as Excel.Range).ColumnWidth = 35.0f;

                    (Worksheet.Cells[4, 2] as Excel.Range).Value = $"Кол-во не сданных практических";
                    Styles(Worksheet.Cells[4, 2], 12, XLHA1ign.xLHA1ignCenter, true);

                    (Worksheet.Cells[4, 3] as Excel.Range).Value = $"Кол-во не сданных теоретических";
                    Styles(Worksheet.Cells[4, 3], 12, XLHA1ign.xLHA1ignCenter, true);

                    (Worksheet.Cells[4, 4] as Excel.Range).Value = $"Отсутствовал на паре";
                    Styles(Worksheet.Cells[4, 4], 12, XLHA1ign.xLHA1ignCenter, true);

                    (Worksheet.Cells[4, 5] as Excel.Range).Value = $"Опоздал";
                    Styles(Worksheet.Cells[4, 5], 12, XLHA1ign.xLHA1ignCenter, true);
                    int Height = 5;
                    List<StudentContext> Students = Main.AllStudents.FindAll(x => x.IdGroup == IdGroup);

                    foreach (StudentContext Student in Students)
                    {
                        List<DisciplineContext> StudentDisciplines = Main.AllDisciplines.FindAll(
                            x => x.IdGroup == Student.IdGroup);

                        int PracticeCount = 0;
                        int TheoryCount = 0;
                        int AbsenteeismCount = 0;
                        int LateCount = 0;

                        foreach (DisciplineContext StudentDiscipline in StudentDisciplines)
                        {
                            List<WorkContext> StudentWorks = Main.AllWorks.FindAll(x => x.IdDiscipline == StudentDiscipline.IdGroup);

                            foreach (WorkContext StudentWork in StudentWorks)
                            {
                                EvaluationContext Evaluation = Main.AllEvaluation.Find(x =>
                                    x.IdWork == StudentWork.Id &&
                                    x.IdStudent == Student.Id);

                                if ((Evaluation != null && (Evaluation.Value.Trim() == "" || Evaluation.Value.Trim() == "2"))
                                    || Evaluation == null)
                                {
                                    if (StudentWork.IdType == 1)
                                        PracticeCount++;
                                    else if (StudentWork.IdType == 2)
                                        TheoryCount++;
                                }

                                if (Evaluation != null && Evaluation.Lateness.Trim() != "")
                                {
                                    if (Convert.ToInt32(Evaluation.Lateness) == 90)
                                        AbsenteeismCount++;
                                    else
                                        LateCount++;
                                }
                            }
                        }
                        (Worksheet.Cells[Height, 1] as Excel.Range).Value = $"{Student.Lastname} {Student.Firstname}";
                        Styles(Worksheet.Cells[Height, 1], 12, XliAlign.xliAlignLeft, true);

                        (Worksheet.Cells[Height, 2] as Excel.Range).Value = PracticeCount.ToString();
                        Styles(Worksheet.Cells[Height, 2], 12, XliAlign.xliAlignCenter, true);

                        (Worksheet.Cells[Height, 3] as Excel.Range).Value = TheoryCount.ToString();
                        Styles(Worksheet.Cells[Height, 3], 12, XliAlign.xliAlignCenter, true);

                        (Worksheet.Cells[Height, 4] as Excel.Range).Value = AbsenteeismCount.ToString();
                        Styles(Worksheet.Cells[Height, 4], 12, XliAlign.xliAlignCenter, true);

                        (Worksheet.Cells[Height, 5] as Excel.Range).Value = LateCount.ToString();
                        Styles(Worksheet.Cells[Height, 5], 12, XliAlign.xliAlignCenter, true);

                        Height++;
                    }

                    Workbook.SaveAs($FD.FileName);
                    Workbook.Close();
                }
                catch (Exception exp) { };

                ExcelApp.Quit();
            }
        }
        public static void Styles(Excel.Range Cell,
            int FontSize,
            Excel.XlHA1ign Position = Excel.XlHA1ign.xlHA1ignCenter,
            bool Border = false) {
            {
                Cell.Font.Name = "Bahnschnift Light Condensed";
                Cell.Font.Size = FontSize;
                Cell.HorizontalAlignment = Position;
                Cell.VerticalAlignment = Excel.XlHA1ign.xlHA1ignCenter;

                if (Border)
                {
                    Excel.Borders border = Cell.Borders;
                    border.LineStyle = Excel.XllineStyle.xlDouble;
                    border.Weight = XlBorderWeight.xlThin;
                    Cell.WrapText = true;
                }
            }
        }
    }
}


