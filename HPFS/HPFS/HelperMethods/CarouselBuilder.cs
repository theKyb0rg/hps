using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.UI.HtmlControls;

namespace HPFS.HelperMethods
{
    public class CarouselBuilder
    {
        public static HPSDB db = new HPSDB();

        public static void BuildCarousel(PlaceHolder plCarousel, int carouselID, string name)
        {
            // Get the images that are in the carousel
            var query = db.HPSFiles
                .Join(db.SlideShowImages, file => file.Id, slideShowImage => slideShowImage.FileId, (file, slideShowImage) => new { file, slideShowImage })
                .Where(type => type.file.FileContentType.Contains("image"))
                .Where(c => c.slideShowImage.SlideShow.Id == carouselID)
                .OrderBy(order => order.slideShowImage.SlideShowPosition)
                .Select(i => i);

            using (HtmlGenericControl carousel = new HtmlGenericControl("div"),
            carouselInner = new HtmlGenericControl("div"),
            ol = new HtmlGenericControl("ol"),
            left = new HtmlGenericControl("a"),
            arrowLeft = new HtmlGenericControl("span"),
            right = new HtmlGenericControl("a"),
            arrowRight = new HtmlGenericControl("span"))
            {
                // Create the div to store the carousel inner
                carousel.Attributes.Add("class", "carousel slide");
                carousel.Attributes.Add("data-ride", "carousel");
                carousel.ID = name;

                // Create the div to store the carousel items
                carouselInner.Attributes.Add("class", "carousel-inner");

                // Create the ordered list to hold the appropriate amount of controls for sliding through the images
                ol.Attributes.Add("class", "carousel-indicators");

                // For use with carousel controls
                int imageCount = 0;

                // Loop through all the images and add each to the carousel
                foreach (var image in query)
                {
                    using (HtmlGenericControl carouselItem = new HtmlGenericControl("div"),
                    carouselCaption = new HtmlGenericControl("div"),
                    carouselHeading = new HtmlGenericControl("h3"),
                    carouselText = new HtmlGenericControl("p"),
                    li = new HtmlGenericControl("li"))
                    {
                        using (System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image())
                        {
                            // Create the carousel item div
                            // Check if this is first image and add active class
                            string active = (imageCount == 0) ? "item active carousel-max-height" : "item carousel-max-height";
                            carouselItem.Attributes.Add("class", active);

                            // Get image from db and apply to image object
                            byte[] bytes = image.file.FileData;
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            img.ImageUrl = "data:image/jpg;base64," + base64String;
                            img.Attributes.Add("class", "img-responsive");

                            // Create the carousel caption div
                            carouselCaption.Attributes.Add("class", "carousel-caption");

                            // Create the carousel caption heading
                            carouselHeading.InnerHtml = image.slideShowImage.SlideShowHeading;

                            // Create the carousel caption text
                            carouselText.InnerHtml = image.slideShowImage.SlideShowText;

                            // Add heading and text to the caption
                            carouselCaption.Controls.Add(carouselHeading);
                            carouselCaption.Controls.Add(carouselText);

                            // Add the caption to the item
                            carouselItem.Controls.Add(img);
                            carouselItem.Controls.Add(carouselCaption);

                            // Add the item to the inner
                            carouselInner.Controls.Add(carouselItem);

                            // Create each list item to go into the order list
                            li.Attributes.Add("data-target", "#Body_" + name);

                            // Add active class on first item in carousel
                            if (imageCount == 0)
                            {
                                li.Attributes.Add("class", "active");
                            }

                            // Add the appropriate data number for carousel slide
                            li.Attributes.Add("data-slide-to", Convert.ToString(imageCount++));

                            // Add list item to ordered list
                            ol.Controls.Add(li);
                        }
                    }
                }
                // Create the left button for the carousel
                left.Attributes.Add("class", "left carousel-control");
                left.Attributes.Add("href", "#Body_" + name);
                left.Attributes.Add("data-slide", "prev");

                // Create the arrow for inside the button
                arrowLeft.Attributes.Add("class", "glyphicon glyphicon-chevron-left");

                // Add the arrrow to the left button
                left.Controls.Add(arrowLeft);

                // Create the right button for the carousel
                right.Attributes.Add("class", "right carousel-control");
                right.Attributes.Add("href", "#Body_" + name);
                right.Attributes.Add("data-slide", "next");

                // Create the arrow for inside the button
                arrowRight.Attributes.Add("class", "glyphicon glyphicon-chevron-right");

                // Add the arrrow to the left button
                right.Controls.Add(arrowRight);

                // Add ordered list, carousel inner and buttons to carousel
                carousel.Controls.Add(ol);
                carousel.Controls.Add(carouselInner);
                carousel.Controls.Add(left);
                carousel.Controls.Add(right);

                // Add the carousel to the placeholder
                plCarousel.Controls.Add(carousel);
            }
        }
    }
}
