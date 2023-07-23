using Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace oLibraryUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class AddAndEditPage : Page
    {
        Tuple<Logic.LibraryManager, Item, Logic.MovePage?, Users> Container;
        Logic.LibraryManager Collection;
        public AddAndEditPage()
        {
            this.InitializeComponent();
        }

        //Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Container = e.Parameter as Tuple<Logic.LibraryManager, Item, Logic.MovePage?, Users>;
            Collection = Container.Item1;
            if (Container.Item3 == Logic.MovePage.EditPage)
            {
                AddItemButton.Visibility = Visibility.Collapsed;
                titleText.Text = "Welcome To Edit Page";
            }
            else
            {
                EditItemButton.Visibility = Visibility.Collapsed;
                titleText.Text = "Welcome To Add Page";
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }


        //Clicks
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            MakeChange(MovePage.AddPage);
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MakeChange(MovePage.EditPage);
        }

        //Methods
        void MakeChange(MovePage Make)
        {
            ExeptionText.Text = string.Empty;
            try
            {
                if (Make == MovePage.AddPage)
                {
                    Collection.AddItem(NameText.Text, CompanyText.Text, GenreText.Text, PriceText.Text, DiscountText.Text, publishDateYear.Text, publishDateMonth.Text, publishDateDay.Text, AuthorText.Text);
                    UIMassages.AddItemMsg("Item");
                }
                else if (Make == MovePage.EditPage)
                {
                    Container.Item1.EditItem(Container.Item2, NameText.Text, CompanyText.Text, GenreText.Text, PriceText.Text, DiscountText.Text, AuthorText.Text, publishDateYear.Text, publishDateMonth.Text, publishDateDay.Text);
                    UIMassages.EditedItemMsg();
                }
                NavigateBack();
            }
            catch (ArgumentException invalidInt)
            {
                ExeptionText.Text = invalidInt.Message;
                return;
            }
            catch (Exception InvalidParameter)
            {
                ExeptionText.Text = InvalidParameter.Message;
            }

        }

        void NavigateBack()
        {
            Frame.Navigate(typeof(LibraryPage), (Container));
        }
    }
}

