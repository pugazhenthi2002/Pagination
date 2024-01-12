using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PageNavigation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pageCollection = new Button[8];
            pictureCollections = new PictureDetails[1];
            InitializePageSettings();
        }

        private int totalPage, currentPage, previewPage;
        private int stepWidth, stepHeight , pageBtnHeight, pageBtnWidth;
        private Button[] pageCollection;
        private string[] pictureTemplates = { "C:/Users/Pugaizhenthi/Downloads/Pictures/JellyFish.jpg", "C:/Users/Pugaizhenthi/Downloads/Pictures/Cub.jpg", "C:/Users/Pugaizhenthi/Downloads/Pictures/Bear.jpg", "C:/Users/Pugaizhenthi/Downloads/Pictures/Moose.jpg", "C:/Users/Pugaizhenthi/Downloads/Pictures/Parrot.jpg" };
        private PictureDetails[] pictureCollections;

        private void rightNavBtn_Click(object sender, EventArgs e)
        {
            if (currentPage <= totalPage-1)
                currentPage++;

            UpdatePageNumber();
        }

        private void leftNavBtn_Click(object sender, EventArgs e)
        {
            if(currentPage>=2)
                currentPage--;

            UpdatePageNumber();
        }

        private void createNavBtn_Click(object sender, EventArgs e)
         {
            if(isValidNumber(totalPageText.Text) && isValidNumber(goToPageText.Text) && isValidNumber(previewPageText.Text))
            {
                totalPage = Convert.ToInt32(totalPageText.Text);
                currentPage = Convert.ToInt32(goToPageText.Text);
                previewPage = Convert.ToInt32(previewPageText.Text);
                if(currentPage<=totalPage && currentPage>=1 && previewPage<=8 && previewPage>=1)
                {
                    InitializePageSettings();

                    totalPageText.Text = "";    goToPageText.Text = "";    previewPageText.Text = "";

                    for (int ctr = 1; ctr <= previewPage; ctr++)
                    {
                        Button B = new Button();
                        B.Name = Convert.ToString(ctr);

                        pageNoDisplayPanel.Controls.Add(B);
                        B.Location = new Point(stepWidth, stepHeight);
                        B.Size = new Size(pageBtnWidth, pageBtnHeight);
                        B.Click += PageDisplay;
                        stepWidth += pageBtnWidth + 10;
                        pageCollection[ctr - 1] = B;
                    }

                    UpdatePageNumber();
                }
                
            }
        }

        private void RemoveCollection()
        {
            
        }

        private void PageDisplay(object sender, EventArgs e)
        {
            Button B = sender as Button;

            if(B.Text == "..." && B.Name=="1")
            {
                currentPage = 1;
            }
            else if (B.Text == "..." && B.Name == Convert.ToString(previewPage))
            {
                currentPage = totalPage;
            }
            else
            {
                currentPage = Convert.ToInt32(B.Text);
            }
            UpdatePageNumber();
        }

        private void UpdatePageNumber()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            if (currentPage <= previewPage-1)
            {
                for (int ctr = 1; ctr <= previewPage; ctr++)
                {
                    if (ctr == previewPage)
                    {
                        pageCollection[ctr - 1].Text = "...";
                    }
                    else
                    {
                        pageCollection[ctr - 1].Text = Convert.ToString(ctr);
                    }

                    if (ctr == currentPage)
                    {
                        pageCollection[ctr-1].Focus();
                    }
                }
            }
            else if (totalPage - previewPage + 2 <= currentPage)
            {
                for (int ctr = 1; ctr <= previewPage; ctr++)
                {
                    if (ctr == 1)
                    {
                        pageCollection[ctr - 1].Text = "...";
                    }
                    else
                    {
                         pageCollection[ctr - 1].Text = Convert.ToString(totalPage + ctr - previewPage);
                    }


                    if (totalPage - previewPage + ctr == currentPage)
                    {
                        pageCollection[ctr - 1].Focus();
                    }
                }
            }
            else
            {
                int Iter = (previewPage / 2) - 1;   Iter = currentPage - Iter -1;
                for (int ctr = 1; ctr <= previewPage; ctr++, Iter++)
                {
                    if (ctr == 1 || ctr == previewPage)
                    {
                        pageCollection[ctr - 1].Text = "...";
                    }
                    else
                    {
                        pageCollection[ctr - 1].Text = Convert.ToString(Iter);
                    }

                    if ((previewPage/2)+1 == ctr)
                    {
                        pageCollection[ctr - 1].Focus();
                    }
                }
            }

            label4.Text = Convert.ToString(pictureCollections[currentPage - 1].PictureID);


            pictureBox1.Image = Image.FromFile(pictureCollections[currentPage - 1].Location);
        }

        private bool isValidNumber(string s)
        {
            if (s.Length == 0)
                return false;

            for(int ctr=0;ctr<s.Length;ctr++)
            {
                if(s[ctr]>'9' || s[ctr]<'0')
                {
                    return false;
                }
            }

            return true;
        }

        private void InitializePageSettings()
        {
            stepWidth = pageNoDisplayPanel.Width - ((previewPage * 50) - ((previewPage - 1) * 10)); stepHeight = 10; pageBtnHeight = 50; pageBtnWidth = 50;
            stepWidth /= 2;

            pictureCollections = new PictureDetails[totalPage];

            for (int ctr=0;ctr<totalPage;ctr++)
            {
                pictureCollections[ctr] = new PictureDetails { PictureID = ctr + 1, Location = pictureTemplates[ctr % 5] };
            }

            for (int ctr = 0; ctr < previewPage && ctr < pageCollection.Length; ctr++) 
            {
                if (pageCollection[ctr] != null)
                {
                    pageCollection[ctr].Dispose();
                }
            }

            pageCollection = new Button[previewPage];
        }
    }
}
