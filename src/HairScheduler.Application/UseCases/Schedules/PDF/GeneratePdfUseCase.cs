
using HairScheduler.Application.UseCases.Schedules.PDF.Colors;
using HairScheduler.Application.UseCases.Schedules.PDF.Fonts;
using HairScheduler.Domain.ExtesionsTypeAndCategory;
using HairScheduler.Domain.GeneratorPdf;
using HairScheduler.Domain.Repositories.Interfaces;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Globalization;


namespace HairScheduler.Application.UseCases.Schedules.PDF;

public class GeneratePdfUseCase : IGeneratePdfUseCase
{
    private readonly IReadOnlyRepository _repository;
    private const int HEIGHT_ROW_TABLE = 25;
    private readonly string Symbol = "R$";
    private const double PriceKid = 30.00;
    private const double PriceAdult = 40.00;


    public GeneratePdfUseCase(IReadOnlyRepository repository)
    {
        _repository = repository;
        GlobalFontSettings.FontResolver = new FontsResolver();

    }

    public async Task<byte[]> Execute(DateTime day, string nickname)
    {
        var informations = await _repository.FilterByDay(day, nickname);
        if (informations.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(day);
        var page = CreatePage(document);
        createHeader(page);
       

        foreach (var information in informations)
        {

            var table = CreateTable(page);
            addWhiteSpace(table);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_TABLE;

            addNameTitle(row.Cells[0]);
            addName(row.Cells[1], information.Name);
            addWhiteSpace(table);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_TABLE;

            addNickTitle(row.Cells[0]);
            addNick(row.Cells[1], information.Nickname);
            addWhiteSpace(table);


            row = table.AddRow();
            row.Height = HEIGHT_ROW_TABLE;

            addHaircutCategoryTitle(row.Cells[0]);
            addHaircutCategory(row.Cells[1], information.haircutCategory.CategoryStringToString());
            addWhiteSpace(table);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_TABLE;

            addDateTitle(row.Cells[0]);
            addDate(row.Cells[1], information.Date.ToString());
            addWhiteSpace(table);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_TABLE;

            addPaymentTitle(row.Cells[0]);
            addPaymentType(row.Cells[1], information.paymentType.paymentTypeToString());
            addWhiteSpace(table);

            if (string.IsNullOrWhiteSpace(information.HaircutDescription) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_TABLE;

                addDescriptionTitle(descriptionRow.Cells[0]);
                addHaircutDescription(descriptionRow.Cells[1], information.HaircutDescription);

            }

            addWhiteSpace(table);
            CultureInfo culturaBrasileira = new CultureInfo("pt-BR");


            if (information.haircutCategory == 0)
            {
                row = table.AddRow();
                row.Height = HEIGHT_ROW_TABLE;
                addAmountTile(row.Cells[0]);
                addPriceAmount(row.Cells[1], $"{Symbol} {PriceKid.ToString("F2", culturaBrasileira)}");
            }
            else
            {
                row = table.AddRow();
                row.Height = HEIGHT_ROW_TABLE;
                addAmountTile(row.Cells[0]);
                addPriceAmount(row.Cells[1], $"{Symbol} {PriceAdult.ToString("F2", culturaBrasileira)}");
            }

        }

        return RenderDocument(document);

    }


    private Document CreateDocument(DateTime day)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceGeneratorPdfMessages.HAIRCUT_INFORMATION} {day:Y}";
        document.Info.Author = "BarberBoss";

        var style = document.Styles["normal"];
        style!.Font.Name = FontHelper.DEFAULT_FONT;

        return document;

    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }


    private void createHeader(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("100").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("300").Format.Alignment = ParagraphAlignment.Center;
        
        var row = table.AddRow();
        /* var assembly = Assembly.GetExecutingAssembly();
         var directoryName = Path.GetDirectoryName(assembly.Location);
         var pathFile = Path.Combine(directoryName, "Logo", "Logo.png");
         row.Cells[0].AddImage(pathFile);*/
        row.Cells[0].AddImage("D:\\Projetos\\C# projects\\HairScheduler\\src\\HairScheduler.Application\\Logo\\Logo.png");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.WORKSANS_BLACK, Size = 30 };
        row.Cells[1].AddParagraph("Barber Shop");
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

    }

    private Table CreateTable(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("150").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("250").Format.Alignment = ParagraphAlignment.Left;
        return table;
    }

    private void addWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }
    private void addNameTitle(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.GREEN_LIGHT };
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.AddParagraph($"{ResourceGeneratorPdfMessages.NAME} :");
    }

    private void addName(Cell cell, string name)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14 };
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
        cell.AddParagraph(name);
    }

    private void addNickTitle(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.GREEN_LIGHT };
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.AddParagraph($"{ResourceGeneratorPdfMessages.NICKNAME} :");
    }

    private void addNick(Cell cell, string nickname)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14 };
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
        cell.AddParagraph(nickname);
    }

    private void addHaircutCategoryTitle(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.GREEN_LIGHT };
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.AddParagraph($"{ResourceGeneratorPdfMessages.HAIRCUT_CATEGORY} :");
    }

    private void addHaircutCategory(Cell cell, string category)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14 };
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
        cell.AddParagraph(category);
    }

    private void addDateTitle(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.GREEN_LIGHT };
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.AddParagraph($"{ResourceGeneratorPdfMessages.DATE} :");
    }

    private void addDate(Cell cell, string date)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14 };
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
        cell.AddParagraph(date);
    }

    private void addPaymentTitle(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.GREEN_LIGHT };
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.AddParagraph($"{ResourceGeneratorPdfMessages.PAYMENT_TYPE} :");
    }

    private void addPaymentType(Cell cell, string paymentType)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14 };
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
        cell.AddParagraph(paymentType);
    }


    private void addDescriptionTitle(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.GREEN_LIGHT };
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.AddParagraph($"{ResourceGeneratorPdfMessages.HAIRCUT_DESCRIPTION} :");
    }


    private void addHaircutDescription(Cell cell, string description)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14 };
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
        cell.AddParagraph(description);
    }

    private void addAmountTile(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.GREEN_LIGHT };
        cell.Shading.Color = ColorHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.AddParagraph($"{ResourceGeneratorPdfMessages.AMOUNT} :");
    }

    private void addPriceAmount(Cell cell, string amount)
    {
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14 };
        cell.Shading.Color = ColorHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
        cell.AddParagraph(amount);
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };
        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }

}
