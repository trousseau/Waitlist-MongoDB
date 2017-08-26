using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaitlListMongoDB.Models
{
    public class ExportParams
    {
        public enum FileType
        {
            CSV,
            TXT,
            JSON
        }

        public enum ExportType
        {
            All_Records,
            Current_List,
            Partial_List
        }

        public FileType Filetype { get; set; }
        public ExportType RecordsToExport { get; set; }

        public ExportParams()
        {

        }

    }
}