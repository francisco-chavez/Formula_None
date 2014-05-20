using System;
using System.Globalization;
using System.Windows.Data;


namespace Unv.RaceTrackEditor.Converters
{
	public class BoolToContentConverter
		: IValueConverter
	{
		public object TrueContent	{ get; set; }
		public object FalseContent	{ get; set; }
		public object NullContent	{ get; set; }


		public BoolToContentConverter()
		{
			TrueContent		= "True";
			FalseContent	= "False";
			NullContent		= "Null";
		}


		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool? input = value as bool?;

			if (value == null)
				return NullContent;

			return input.Value ? TrueContent : FalseContent;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
