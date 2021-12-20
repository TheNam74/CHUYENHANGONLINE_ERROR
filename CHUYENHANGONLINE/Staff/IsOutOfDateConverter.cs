using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CHUYENHANGONLINE.Staff
{
    [ValueConversion(typeof(DateTime), typeof(int))]
    class IsOutOfDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return -1;//hợp đồng chưa đc duyệt
            }
            DateTime date = (DateTime)value;
            DateTime curDate = DateTime.Now;

            TimeSpan span = curDate.Subtract(date);

            if (span.TotalDays > 0)
            {
                return 1;//hợp đồng hết hạn
            }

            return 0;//hợp đồng còn hạn
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
