using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chargen.Character.CharacterProperties;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace chargen.Character
{
    public class CharacterPDFParser
    {
        public static void ExportCharacter(Character character)
        {
             string outputPdfPath = character.Name+".pdf";

            using (PdfWriter writer = new PdfWriter(outputPdfPath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    // Add title to the PDF
                    document.Add(new Paragraph(character.Name)
                        .SetFontSize(24)
                        .SimulateBold()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetMarginBottom(20));

                    
                    AddCharacterAttributes(document, outputPdfPath, character);

                    
                    

                    document.Close();
                }
            }
        }

        static void AddCharacterAttributes(Document document, string filePath, Character character)
        {
            // Add a section header for the file
            document.Add(new Paragraph($"--- {character.Metatype} ---")
                .SetFontSize(14)
                .SimulateBold()
                .SetMarginTop(10)
                .SetMarginBottom(5));

                Table table = new Table(UnitValue.CreatePercentArray(new float[] { 2, 5 }))
                        .UseAllAvailableWidth();

                    // Add header row
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Attribute").SimulateBold()));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Value").SimulateBold()));
                    

                    // Add rows of data
                    foreach(CharacterAttribute attribute in character.Attributess)
                        {
                        table.AddCell(new Cell().Add(new Paragraph(attribute.AttributeName))).SimulateBold();
                        table.AddCell(new Cell().Add(new Paragraph(attribute.Value.ToString())));
                    }

                    // Add the table to the document
                    document.Add(table);

        
        }
    
    }
}