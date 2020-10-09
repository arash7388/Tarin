namespace MehranPack
{
    partial class OrderReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderReport));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.reportHeaderSection = new Telerik.Reporting.ReportHeaderSection();
            this.CompanyInfoPanel = new Telerik.Reporting.Panel();
            this.pageHeaderSection = new Telerik.Reporting.PageHeaderSection();
            this.companyLogoPictureBox = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.70866191387176514D);
            this.groupFooterSection.Name = "groupFooterSection";
            this.groupFooterSection.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.groupFooterSection.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.23610194027423859D);
            this.groupHeaderSection.Name = "groupHeaderSection";
            this.groupHeaderSection.PrintOnEveryPage = true;
            this.groupHeaderSection.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0.19685038924217224D);
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.708582878112793D);
            this.detailSection1.Name = "detailSection1";
            this.detailSection1.Style.BorderColor.Default = System.Drawing.Color.Silver;
            this.detailSection1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detailSection1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            this.detailSection1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            // 
            // reportHeaderSection
            // 
            this.reportHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20746493339538574D);
            this.reportHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.CompanyInfoPanel});
            this.reportHeaderSection.Name = "reportHeaderSection";
            this.reportHeaderSection.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0.19685038924217224D);
            // 
            // CompanyInfoPanel
            // 
            this.CompanyInfoPanel.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3853931427001953D), Telerik.Reporting.Drawing.Unit.Inch(0.010377724654972553D));
            this.CompanyInfoPanel.Name = "CompanyInfoPanel";
            this.CompanyInfoPanel.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8742527961730957D), Telerik.Reporting.Drawing.Unit.Inch(0.19708719849586487D));
            // 
            // pageHeaderSection
            // 
            this.pageHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1025595664978027D);
            this.pageHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.companyLogoPictureBox,
            this.textBox1});
            this.pageHeaderSection.Name = "pageHeaderSection";
            // 
            // companyLogoPictureBox
            // 
            this.companyLogoPictureBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7257075309753418D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.companyLogoPictureBox.MimeType = "image/png";
            this.companyLogoPictureBox.Name = "companyLogoPictureBox";
            this.companyLogoPictureBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8742529153823853D), Telerik.Reporting.Drawing.Unit.Inch(1.1023622751235962D));
            this.companyLogoPictureBox.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.companyLogoPictureBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.companyLogoPictureBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.companyLogoPictureBox.Value = ((object)(resources.GetObject("companyLogoPictureBox.Value")));
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox1.Value = "textBox1";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "MehranPack (MehranPack)";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.Parameters.AddRange(new Telerik.Reporting.SqlDataSourceParameter[] {
            new Telerik.Reporting.SqlDataSourceParameter("@id", System.Data.DbType.Int32, null)});
            this.sqlDataSource1.SelectCommand = "GetOrderById";
            this.sqlDataSource1.SelectCommandType = Telerik.Reporting.SqlDataSourceCommandType.StoredProcedure;
            // 
            // OrderReport
            // 
            this.Culture = new System.Globalization.CultureInfo("en-US");
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection;
            group1.Name = "group";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection,
            this.groupFooterSection,
            this.pageHeaderSection,
            this.detailSection1,
            this.reportHeaderSection});
            this.Name = "Invoice1";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.5999999046325684D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detailSection1;
        private Telerik.Reporting.ReportHeaderSection reportHeaderSection;
        private Telerik.Reporting.Panel CompanyInfoPanel;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection;
        private Telerik.Reporting.PictureBox companyLogoPictureBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.SqlDataSource sqlDataSource1;

    }
}