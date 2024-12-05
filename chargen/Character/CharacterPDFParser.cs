﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using chargen.Character.CharacterProperties;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using static chargen.Character.CharacterProperties.CharacterSkill;

namespace chargen.Character
{
    public class CharacterPDFParser
    {
        public static void ExportCharacter(CaAeCharacter character)
        {
            string outputPdfPath = character.Name + ".pdf";

            using (PdfWriter writer = new PdfWriter(outputPdfPath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"orbitron", "orbitron-medium.otf");
                    PdfFont ocrFont = PdfFontFactory.CreateFont(fontPath);
                   
                    document.SetFont(ocrFont);

                    // Add title to the PDF
                    document.Add(new Paragraph(character.Name)
                        .SetFontSize(24)
                        .SimulateBold()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetMarginBottom(20));

                    // Add basic character details
                    document.Add(new Paragraph($"Metatype: {character.Metatype}")
                        .SetFontSize(12)
                        .SetMarginBottom(5));
                    document.Add(new Paragraph($"Archetype: {character.Archetype}")
                        .SetFontSize(12)
                        .SetMarginBottom(10));

                    // Add attributes
                    AddCharacterAttributes(document, character);

                    // Add skills
                    AddCharacterSkills(document, character);

                    // Add career information
                    AddCharacterCareer(document, character);

                    // Add other sections (example: inventory, background)
                    AddCharacterExtras(document, character);

                    // Close the document
                    document.Close();
                }
            }
        }

        private static void AddCharacterAttributes(Document document, CaAeCharacter character)
        {
            document.Add(new Paragraph("Attributes")
                .SetFontSize(16)
                .SimulateBold()
                .SetMarginBottom(10));

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 2, 5 }))
                .UseAllAvailableWidth();

            // Add header row
            table.AddHeaderCell(new Cell().Add(new Paragraph("Attribute").SimulateBold()));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Value").SimulateBold()));

            // Add rows of data
            foreach (CharacterAttribute attribute in character.Attributess)
            {
                table.AddCell(new Cell().Add(new Paragraph(attribute.AttributeName)));
                table.AddCell(new Cell().Add(new Paragraph(attribute.Value.ToString())));
            }

            // Add the table to the document
            document.Add(table);
        }

        private static void AddCharacterSkills(Document document, CaAeCharacter character)
        {
            document.Add(new Paragraph("Skills")
         .SetFontSize(16)
         .SimulateBold()
         .SetMarginTop(15)
         .SetMarginBottom(10));

            // Define table with 4 columns: Skill Name, Trained, Advanced, Expert
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 4, 2, 2, 2 }))
                .UseAllAvailableWidth();

            // Add header row
            table.AddHeaderCell(new Cell().Add(new Paragraph("Skill").SimulateBold()));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Trained").SimulateBold()));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Advanced").SimulateBold()));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Expert").SimulateBold()));

            // Add rows for skills
            foreach (CharacterSkill skill in character.Skills)
            {         

                // Add the skill name
                table.AddCell(new Cell().Add(new Paragraph(skill.Name)));

                // Prepare cells for Trained, Advanced, and Expert
                Cell trainedCell = new Cell();
                Cell advancedCell = new Cell();
                Cell expertCell = new Cell();

                // Use a switch statement to set the background color
                switch (skill.CurrentLevel)
                {
                    case KnowledgeLevel.Trained:
                        trainedCell.SetBackgroundColor(new DeviceRgb(0, 236, 0));
                        break;
                    case KnowledgeLevel.Advanced:
                        advancedCell.SetBackgroundColor(new DeviceRgb(0, 236, 0));
                        break;
                    case KnowledgeLevel.Expert:
                        expertCell.SetBackgroundColor(new DeviceRgb(0, 236, 0));
                        break;
                    default:
                        // Leave cells with default background for untrained or unknown levels
                        break;
                }

                // Add the cells to the table
                table.AddCell(trainedCell);
                table.AddCell(advancedCell);
                table.AddCell(expertCell);                

            }

            // Add the table to the document
            document.Add(table);
        }

        private static void AddCharacterCareer(Document document, CaAeCharacter character)
        {
            //ToDo: Implement in Class
            /*
            document.Add(new Paragraph("Career")
                .SetFontSize(16)
                .SimulateBold()
                .SetMarginTop(15)
                .SetMarginBottom(10));

            document.Add(new Paragraph($"Current Career: {character.CurrentCareer.Name}")
                .SetFontSize(12)
                .SetMarginBottom(5));
            document.Add(new Paragraph($"Terms Completed: {character.CareerTerms}")
                .SetFontSize(12)
                .SetMarginBottom(5));
            document.Add(new Paragraph($"Total Age: {character.Age}")
                .SetFontSize(12)
                .SetMarginBottom(10));
            */
        }

        private static void AddCharacterExtras(Document document, CaAeCharacter character)
        {
            document.Add(new Paragraph("Additional Information")
                .SetFontSize(16)
                .SimulateBold()
                .SetMarginTop(15)
                .SetMarginBottom(10));
            //ToDo: Implement in Class
            /*
            document.Add(new Paragraph("Background:")
                .SetFontSize(12)
                .SimulateBold());
            document.Add(new Paragraph(character.Background)
                .SetFontSize(12)
                .SetMarginBottom(10));

            document.Add(new Paragraph("Inventory:")
                .SetFontSize(12)
                .SimulateBold());
            foreach (string item in character.Inventory)
            {
                document.Add(new Paragraph($"- {item}")
                    .SetFontSize(12));
            }
            */
        }


    }
}