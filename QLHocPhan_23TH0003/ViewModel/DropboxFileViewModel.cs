namespace QLHocPhan_23TH0003.ViewModel
{
    using System;

    public class DropboxUploadResponse
    {
        public string name { get; set; }
        public string path_display { get; set; }
        public string path_lower { get; set; }
        public DateTime client_modified { get; set; }
        public DateTime server_modified { get; set; }
        public string rev { get; set; }
        public long size { get; set; }
        public string id { get; set; }
    }

    public class DropboxFileViewModel
    {
        public string Name { get; set; }              
        public string PathDisplay { get; set; }       
        public string DownloadUrl { get; set; }       
        public string IconUrl { get; set; }           
        public string FileType { get; set; }          
        public string SizeFormatted { get; set; }     
        public DateTime ModifiedTime { get; set; }    
    }

}
