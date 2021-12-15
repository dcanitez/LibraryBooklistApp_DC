using LibraryAutomation_DC.Classes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LibraryAutomation_DC
{
    public partial class MainPage : Form
    {
        private List<Book> bookList = new List<Book>();
        private Book selectedBook;

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            btnAdd.Visible = true;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }

        private void LoadComboBox()
        {
            List<string> listViewTypes = new List<string>
            {
                "Large Icon",
                "Small Icon",
                "List",
                "Tile",
                "Details"
            };
            cmbListViewType.DataSource = listViewTypes;
            cmbListViewType.SelectedIndex = -1;
            cmbListViewType.Text = "Choose View";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Book book = new Book
            {
                BookName = txtBookName.Text.Trim(),
                AuthorName = txtAuthorName.Text.Trim(),
                PageCount = (int)nudPageCount.Value,
                PublishedDate = dtpPublishedDate.Value,
                IsSale = chkIsSale.Checked
            };
            bookList.Add(book);
            LoadToListView();
            MessageBox.Show($"{book.BookName} Added!");
            ClearFields();
        }

        private void ClearFields()
        {
            txtBookName.Clear();
            txtAuthorName.Clear();
            nudPageCount.Value = default;
            dtpPublishedDate.Value = DateTime.Now;
        }

        private void LoadToListView()
        {
            lvBookList.Items.Clear();
            ListViewItem lvi;
            foreach (var item in bookList)
            {
                lvi = new ListViewItem(item.BookName);
                lvi.SubItems.Add(item.AuthorName);
                lvi.SubItems.Add(item.PageCount.ToString());
                lvi.SubItems.Add(item.PublishedDate.ToShortDateString());
                lvi.SubItems.Add(item.IsSale.ToString());
                lvi.Tag = item;
                lvBookList.Items.Add(lvi);
            }
        }

        private void cmbListViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedType = cmbListViewType.Text;
            switch (selectedType)
            {
                case "Large Icon":
                    lvBookList.View = View.LargeIcon;
                    break;

                case "Small Icon":
                    lvBookList.View = View.SmallIcon;
                    break;

                case "List":
                    lvBookList.View = View.List;
                    break;

                case "Details":
                    lvBookList.View = View.Details;
                    break;

                default:
                    break;
            }
        }

        private void lvBookList_DoubleClick(object sender, EventArgs e)
        {
            if (lvBookList.SelectedItems[0].Tag != null)
            {
                btnAdd.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                selectedBook = lvBookList.SelectedItems[0].Tag as Book;
                txtBookName.Text = selectedBook.BookName;
                txtAuthorName.Text = selectedBook.AuthorName;
                nudPageCount.Value = selectedBook.PageCount;
                dtpPublishedDate.Value = selectedBook.PublishedDate;
                chkIsSale.Checked = selectedBook.IsSale;
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            selectedBook.BookName = txtBookName.Text.Trim();
            selectedBook.AuthorName = txtAuthorName.Text.Trim();
            selectedBook.PageCount = (int)nudPageCount.Value;
            selectedBook.PublishedDate = dtpPublishedDate.Value;
            selectedBook.IsSale = chkIsSale.Checked;

            LoadToListView();
            MessageBox.Show($"{selectedBook.BookName} Changed!");
            ClearFields();
        }
       

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"{selectedBook.BookName} will be removed.\nAre you sure to remove this item from your library?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                bookList.Remove(selectedBook);
                LoadToListView();
            }
        }
    }
}
