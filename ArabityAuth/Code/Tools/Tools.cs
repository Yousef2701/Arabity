namespace ArabityAuth
{
    public class Tools
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        
        public Tools(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        public string AddImages(IFormFile imagefile,string username)
        {

            if (imagefile == null)
            { return  "Defult User Image"+username; }
            string imageUrl = imagefile.FileName;
            string uploads = Path.Combine(Environment.WebRootPath, "Uploads");
            string path = Path.Combine(uploads, username.Substring(0,username.Length-4) + imageUrl);

            if (System.IO.File.Exists(imageUrl))
            {
                string temporary = Path.Combine(Environment.WebRootPath, "ImagePackups");
                File.Copy(path, temporary);
                string newFilePath=Path.Combine(path,username.Substring(0, username.Length - 4) + imageUrl);
                File.Move(temporary,newFilePath);
            }
            else
            { imagefile.CopyTo(new FileStream(path, FileMode.Create)); }
            
           
            return username.Substring(0, username.Length - 4) + imageUrl;
        }
        public string UpdateImages(IFormFile imagefile,string imagename)
        {
            string imageUrl = imagefile.FileName;
            string path = Path.Combine(Environment.WebRootPath, "Uploads");
            if (System.IO.File.Exists(imagename))
            {
              System.IO.File.Delete(imagename);
              imagefile.CopyTo(new FileStream(path, FileMode.Create));
            }
            
            
            return imageUrl;
        }
    }
}
