using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LibraryUI;
using Logic;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace oLibraryUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class LibraryPage : Page
    {
        /* ||Updates and things to do||
         *
         *
         * ||Future Updaets||
         || add Users Manager for the librarian ||
        - for Removing users , Change Settings, show information about the user(books buy, total income per user and more)

         * when moving to edit page - show the full information about the book you are changing for know what to change
         * change duplicated code in the serilazation Class ( send strings and types instead of duplicating code
         * add count for books and magazins

        // need to use SOLID princepels and SRP, for example: make ui massages class that store all the ui massages instead of making them in the ui code.
        // dont call click buttons from other methods, if need make a method that will be called like RefreshLibrary().
        //the database take me alot of waste of time but this is ok, i learend some and i got into this, for know i will stay without.
         */

        Tuple<LibraryManager, Item, Logic.MovePage?, Users> Container;
        LibraryManager LibraryCollection;
        Item movingItem;
        Users currectUser;
        MyCashBox LibraryCash;
        public LibraryPage()
        {
            this.InitializeComponent();
        }
        //Navigations
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Container = e.Parameter as Tuple<LibraryManager, Item, Logic.MovePage?, Users>;
            LibraryCollection = Container.Item1;
            LibraryCash = LibraryCollection.LibraryCashBox;
            currectUser = Container.Item4;

            RefreshLibrary();
        }
        private void NavigateAddPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LibraryCash.EnugthCashCheck(Costs.addItem))
                {
                    Container = new Tuple<LibraryManager, Item, Logic.MovePage?, Users>(LibraryCollection, null, Logic.MovePage.AddPage, currectUser);
                    Frame.Navigate(typeof(AddAndEditPage), Container);
                    return;
                }
            }
            catch (ArgumentException)
            {
                ErrorMassages.NoCashInLibraryError();
            }

        }
        private void NavigateEditPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListViewSelectedNullCheck() || !LibraryCash.EnugthCashCheck(Costs.editItem))
                {
                    Container = new Tuple<Logic.LibraryManager, Item, Logic.MovePage?, Users>(LibraryCollection, movingItem, Logic.MovePage.EditPage, currectUser);
                    Frame.Navigate(typeof(AddAndEditPage), Container);
                }
            }
            catch (ArgumentException)
            {
                ErrorMassages.NoCashInLibraryError();
            }
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage), Container);
        }

        //Librarian only
        private void AddDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyListView.Items.ToList().ForEach(item => LibraryCollection.AddDiscount((Item)item, discountText.Text));
                RefreshLibrary();
            }
            catch (ArgumentException invalidDiscount)
            {
                discountErrortext.Text = invalidDiscount.Message;

            }
        }
        private void RemoveDiscountButton_Click(object sender, RoutedEventArgs e)
        {
            MyListView.Items.ToList().ForEach(item => LibraryCollection.RemoveDiscount((Item)item));
            RefreshLibrary();
        }
        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewSelectedNullCheck())
            {
                LibraryCollection.RemoveItem(movingItem);
                RefreshLibrary();
            }
        }

        //all
        private void RentItemButton_Click(object sender, RoutedEventArgs e) // need to make
        {
            if (ListViewSelectedNullCheck())
            {
                LibraryCollection.RentItem(movingItem, currectUser);
                RefreshLibrary();
                if (currectUser is Librarian)
                {
                    return;
                }
                UIMassages.GetReceipMsg(movingItem, currectUser);
            }
        }
        private void ReturnItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RentListViewSelectedNullCheck())
                {
                    LibraryCollection.ReturnItem(rentListView.SelectedItem as Item, currectUser);
                    RefreshLibrary();
                }
            }
            catch (ArgumentException)
            {

                ErrorMassages.CantReturnError();
            }


        }
        private void SerchText_TextChanged(object sender, TextChangedEventArgs e) // this method calls after the text changing
        {
            MyListView.Items.Clear();
            string name = SerchNameText.Text.ToLower();
            string genre = SerchGenreText.Text.ToLower();
            string author = SerchAuthorText.Text.ToLower();
            string publisher = SerchPublisherText.Text.ToLower();
            string type = SerchTypeText.Text.ToLower();

            LibraryCollection.Serch(name, genre, author, publisher, type).ForEach(item => MyListView.Items.Add(item));
        }

        //UI Stuff
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyListView.SelectedItem != null)
            {
                movingItem = MyListView.SelectedItem as Logic.Item;
            }
        }
        //private void rentListView_SelectionChanged(object sender, SelectionChangedEventArgs e) // EMPTY
        //{
        //}

        //Both?

        //Message Dialog exeption Throws
        public bool RentListViewSelectedNullCheck()
        {
            if (rentListView.SelectedItem == null)
            {
                ErrorMassages.ItemNotSelectedError("Rent");
                return false;
            }
            return true;
        }
        public bool ListViewSelectedNullCheck()
        {
            if (MyListView.SelectedItem == null)
            {
                ErrorMassages.ItemNotSelectedError();
                return false;
            }
            return true;
        }

        private void RefreshLibrary()
        {
            MyListView.Items.Clear();
            rentListView.Items.Clear();

            if (currectUser is Librarian)
            {
                RentItemButton.Content = UIMassages.LibrarianRentButtonText();
                ReturnButton.Content = UIMassages.LibrarianReturnButtonText();
            }
            else
            {
                LibrarianStackPanel.Visibility = Visibility.Collapsed;

                if (currectUser is null)
                {
                    RentItemButton.Visibility = Visibility.Collapsed;
                    ReturnButton.Visibility = Visibility.Collapsed;
                    rentGrid.Visibility = Visibility.Collapsed;
                }
            }
            welcomeText.Text = $"Welcome Back {currectUser}";

            foreach (var item in LibraryCollection.GetAllItems())
            {
                if (item.Renter is null)
                {
                    MyListView.Items.Add(item);
                }
                else
                {
                    rentListView.Items.Add(item);
                    LibraryCollection.LateOnReturnCheck(item);
                }
            }

            cashBoxText.Text = LibraryCash.ToString();
            LibraryItemCount.Text = MyListView.Items.Count().ToString();
            RentItemCount.Text = rentListView.Items.Count().ToString();

        }
    }

}
