using System;
using System.Globalization;
using System.Windows.Data;
using static chargen.Character.CharacterProperties.CharacterSkill;

namespace chargen.Character.CharacterProperties
{
    
public class KnowledgeLevelToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is KnowledgeLevel currentLevel && parameter is string targetLevel)
        {
            return currentLevel.ToString() == targetLevel;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isChecked && isChecked && parameter is string targetLevel)
        {
            if (Enum.TryParse(typeof(KnowledgeLevel), targetLevel, out var result))
            {
                return result;
            }
        }
        return KnowledgeLevel.Untrained; // Default fallback
    }
}

}
