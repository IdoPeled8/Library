using LibraryUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUI.Pages
{
    public class ErrorMassages
    {

        public static string Invalid(string word)
        {
            return $"Invalid: {word}";
        }

        public static string NullOrEmptyMassage(string word)
        {
            return $"{word} Cannot be Null or Empty";
        }
    }
}

public class SimpleChecks
{
}
public class IntChecks
{
    public static int PriceCheck(string textbox)
    {
        bool priceCheck = int.TryParse((textbox), out int Price);
        if (!priceCheck)
            throw new ArgumentException(ErrorMassages.Invalid("Price"));

        return Price;
    }
    public static int DiscountPriceCheck(string textbox)
    {
        bool DiscountCheck = int.TryParse((textbox), out int DiscountPrice);
        if (!DiscountCheck)
            throw new ArgumentException(ErrorMassages.Invalid("Discount Price"));

        return DiscountPrice;

    }
    public static int YearCheck(string textbox)
    {
        bool publishYear = int.TryParse((textbox), out int PublishYear);
        if (publishYear == false || PublishYear < 1000 || PublishYear > DateTime.Now.Year)
            throw new ArgumentException(ErrorMassages.Invalid("Year"));

        return PublishYear;
    }
    public static int MonthCheck(string textbox)
    {
        bool publishMonth = int.TryParse((textbox), out int PublishMonth);
        if (publishMonth == false || PublishMonth < 1 || PublishMonth > 12)
            throw new ArgumentException(ErrorMassages.Invalid("Month"));

        return PublishMonth;
    }
    public static int DayCheck(string textbox)
    {

        bool publishDay = int.TryParse((textbox), out int PublishDay);

        if (publishDay == false || PublishDay < 1 || PublishDay > 31)
            throw new ArgumentException(ErrorMassages.Invalid("Day"));

        return PublishDay;
    }





}
