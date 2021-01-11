using System;
using System.Web.UI;
using Common;
using Repository.DAL;
using Telerik.Reporting;

namespace MehranPack
{
    public partial class WorksheetPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckShamsiDateFunctions();

            int id;

            if (Page.Request.QueryString[0].ToSafeInt() == 0)
                id = new WorksheetRepository().GetMaxId();
            else
                id = Page.Request.QueryString[0].ToSafeInt();

            Report report = new ReportWorksheets();

            Telerik.Reporting.SqlDataSource sqlDataSource = new Telerik.Reporting.SqlDataSource();
            sqlDataSource.ConnectionString = "Tarin";
            sqlDataSource.SelectCommand =
            "select WID,[Date],PartNo,WaxNo,ColorName,OperatorId,OperatorName,ProcessId,ProcessName,[Desc] from (" +
            "SELECT distinct w.Id WID, dbo.shamsidate(w.Date) as [Date] ,w.PartNo,w.WaxNo,c.Name ColorName, u.FriendlyName OperatorName," +
            "    pro.Name ProcessName," +
            "    pro.Id ProcessId," +
            "    cat.Id CatId," +
            "    u.Id OperatorId, " +
            "    w.[Desc] as [Desc] " +
            "FROM worksheets w " +
            "join Colors c on c.Id = w.ColorId " +
            "join Users u on u.Id = w.OperatorId " +
            "join WorksheetDetails d on w.Id = d.WorksheetId " +
            "join Products p on p.Id = d.ProductId " +
            "join Categories cat on cat.Id = p.ProductCategoryId " +
            "join ProcessCategories pcat on pcat.CategoryId = cat.Id " +
            "join Processes pro on pro.Id = pcat.ProcessId" +
            ") s1 " +
            "where WId = @id " +
            "group by WID,[Date],PartNo,WaxNo,ColorName,OperatorId,OperatorName,ProcessId,ProcessName,[Desc] " +
            "order by ProcessId";

            sqlDataSource.Parameters.Add("@id", System.Data.DbType.Int32, id);
            report.DataSource = sqlDataSource;

            InstanceReportSource reportSource = new InstanceReportSource();
            reportSource.ReportDocument = report;

            ReportViewer1.ReportSource = reportSource;

            var table1 = report.Items.Find("table1", true)[0];
            ((table1 as Telerik.Reporting.Table).DataSource as Telerik.Reporting.SqlDataSource).Parameters[0].Value = id;
            ReportViewer1.RefreshReport();
        }

        private void CheckShamsiDateFunctions()
        {
            var cmd = @"CREATE or alter FUNCTION [dbo].[Shamsidate] (@Date1 DATETIME) 
                returns VARCHAR(10) 
                AS 
                  BEGIN 
                      DECLARE @ResultVar VARCHAR(10) 
                      DECLARE @Year INT 
                      DECLARE @Month INT 
                      DECLARE @Day INT 
                      DECLARE @PersianYear INT 
                      DECLARE @PersianMonth INT 
                      DECLARE @PersianDay INT 
                      DECLARE @StartMonthGregorianDateInPersianCalendar INT=10 
                      DECLARE @StartDayGregorianDateInPersianCalendar INT=11 
                      DECLARE @Date NVARCHAR(10) 

                      SET @Date = CONVERT(NVARCHAR(10), CONVERT(DATE, @Date1)) 
                      SET @Year=CONVERT(INT, Substring(@Date, 1, 4)) 
                      SET @Month=CONVERT(INT, Substring(@Date, 6, 2)) 
                      SET @Day=CONVERT(INT, Substring(@Date, 9, 2)) 

                      DECLARE @GregorianDayIndex INT=0 

                      IF( dbo.Isleapyear(@Year) = 1 ) 
                        SET @StartDayGregorianDateInPersianCalendar=11 
                      ELSE IF( dbo.Isleapyear(@Year - 1) = 1 ) 
                        SET @StartDayGregorianDateInPersianCalendar=12 
                      ELSE 
                        SET @StartDayGregorianDateInPersianCalendar=11 

                      DECLARE @m_index INT=1 

                      WHILE @m_index <= @Month - 1 
                        BEGIN 
                            SET @GregorianDayIndex= 
                            @GregorianDayIndex 
                            + dbo.Numberofdaysinmonthgregorian(@Year, @m_index) 
                            SET @m_index=@m_index + 1 
                        END 

                      SET @GregorianDayIndex=@GregorianDayIndex + @Day 

                      IF( @GregorianDayIndex >= 80 ) 
                        BEGIN 
                            SET @PersianYear=@Year - 621 
                        END 
                      ELSE 
                        BEGIN 
                            SET @PersianYear=@Year - 622 
                        END 

                      DECLARE @mdays INT 
                      DECLARE @m INT 
                      DECLARE @index INT=@GregorianDayIndex 

                      SET @m_index=0 

                      WHILE 1 = 1 
                        BEGIN 
                            IF( @m_index <= 2 ) 
                              SET @m=@StartMonthGregorianDateInPersianCalendar 
                                     + @m_index 
                            ELSE 
                              SET @m=@m_index - 2 

                            SET @mdays = dbo.Numberofdayinmonthpersian(@Year, @m) 

                            IF( @m = @StartMonthGregorianDateInPersianCalendar ) 
                              SET @mdays=@mdays - @StartDayGregorianDateInPersianCalendar + 1 

                            IF( @index <= @mdays ) 
                              BEGIN 
                                  SET @PersianMonth=@m 

                                  IF( @m = @StartMonthGregorianDateInPersianCalendar ) 
                                    SET @PersianDay=@index 
                                                    + @StartDayGregorianDateInPersianCalendar 
                                                    - 1 
                                  ELSE 
                                    SET @PersianDay=@index 

                                  BREAK 
                              END 
                            ELSE 
                              BEGIN 
                                  SET @index=@index - @mdays 
                                  SET @m_index=@m_index + 1 
                              END 
                        END 

                      SET @ResultVar= CONVERT(VARCHAR(4), @PersianYear) + '/' 
                                      + RIGHT('0'+CONVERT(VARCHAR(2), @PersianMonth), 2) 
                                      + '/' 
                                      + RIGHT('0'+CONVERT(VARCHAR(2), @PersianDay), 2) 

                      RETURN @ResultVar 
                  END 

                go 

                CREATE or alter FUNCTION [dbo].[Isleapyear] (@Year INT) 
                returns BIT 
                AS 
                  BEGIN 
                      DECLARE @ResultVar BIT 

                      IF @Year % 400 = 0 
                        BEGIN 
                            SET @ResultVar=1 
                        END 
                      ELSE IF @Year % 100 = 0 
                        BEGIN 
                            SET @ResultVar=0 
                        END 
                      ELSE IF @Year % 4 = 0 
                        BEGIN 
                            SET @ResultVar=1 
                        END 
                      ELSE 
                        BEGIN 
                            SET @ResultVar=0 
                        END 

                      RETURN @ResultVar 
                  END 

                go 

                CREATE or alter FUNCTION [dbo].[Numberofdaysinmonthgregorian] (@Year  INT, 
                                                                      @Month INT) 
                returns INT 
                AS 
                  BEGIN 
                      DECLARE @ResultVar INT 

                      IF( @Month <> 2 ) 
                        BEGIN 
                            SET @ResultVar=30 + ( ( @Month + Floor(@Month/8) ) % 2 ) 
                        END 
                      ELSE 
                        BEGIN 
                            IF( dbo.Isleapyear(@Year) = 1 ) 
                              BEGIN 
                                  SET @ResultVar=29 
                              END 
                            ELSE 
                              BEGIN 
                                  SET @ResultVar=28 
                              END 
                        END 

                      RETURN @ResultVar 
                  END 
                go 

                CREATE or alter FUNCTION [dbo].[Numberofdayinmonthpersian] (@Year  INT, 
                                                                   @Month INT) 
                returns INT 
                AS 
                  BEGIN 
                      DECLARE @ResultVar INT 

                      IF( @Month <= 6 ) 
                        SET @ResultVar=31 
                      ELSE IF( @Month = 12 ) 
                        IF( dbo.Isleapyear(@Year - 1) = 1 ) 
                          SET @ResultVar=30 
                        ELSE 
                          SET @ResultVar=29 
                      ELSE 
                        SET @ResultVar=30 

                      RETURN @ResultVar 
                  END ";

            var uow = new UnitOfWork();

            uow.ExecCommand(cmd);
        }
    }
}