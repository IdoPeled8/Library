# Library Management System

# Description:
The Library Management System is a Universal Windows Platform (UWP) application that serves as a digital library for managing books and magazines. It provides functionalities for librarians to add, edit, and manage items in the library, as well as for users to browse and rent items.

# Features:

Item Management: Librarians can add books and magazines to the library collection. Each item has attributes like name, publisher, genre, price, and more.

Item Editing: Librarians can edit item details such as name, publisher, genre, price, and publication date. Changes are validated and saved.

Item Renting: Users can rent items from the library. The system automatically assigns return dates and tracks rented items.

Librarian Authentication: Librarians can log in using their credentials to access management functionalities.

Search: Users can search for items based on various criteria such as name, genre, author, and publisher.

Discounts: Librarians can apply and remove discounts on items.

Serialization: The system uses JSON serialization to store and retrieve item data from files.

# Project Structure:

ItemCollection: Manages the collection of items, including adding, editing, and searching.

Item: Represents an individual item, with attributes such as name, publisher, and genre.

Book and Magazine: Subclasses of Item with additional attributes like author and publication date.

JsonFileSerialize: Handles JSON serialization and deserialization of item data.
The UI is designed using XAML, featuring pages for login, item management, and search.

# How to Run:

Clone the repository to your local machine.

Open the solution in Visual Studio.

Build and deploy the UWP application to your target device or emulator.

# Contributions:
Contributions to this project are welcome! Feel free to fork the repository, make changes, and submit pull requests.

# License:
This project is licensed under the MIT License.

# Contact:
For questions or inquiries, you can reach out to me at idop9081@gmail.com.

