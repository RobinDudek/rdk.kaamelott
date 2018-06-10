using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace rdk.kaamelott.Converters
{
    public class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageName = value as string;
            var realNameImage = "rdk.kaamelott.Resources.Images." + imageName;
            if (HasResource(realNameImage))
            {
                var img = ImageSource.FromResource(realNameImage);
                return img;
            }
            return ImageSource.FromResource("rdk.kaamelott.Resources.Images.profile_generic.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        bool HasResource(string name)
        {
            return this.GetType().GetTypeInfo().Assembly.GetManifestResourceNames().Where(x => x == name).Count() > 0;
        }
    }
}
