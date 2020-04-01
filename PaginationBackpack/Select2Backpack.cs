using NetCoreBackPack.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreBackPack.PaginationBackpack
{
    public class Select2Backpack
    {
        public static Select2PagedResult AttendeesToSelect2Format(Dictionary<long, string> dictionary)
        {
            Select2PagedResult jsonAttendees = new Select2PagedResult();
            jsonAttendees.Results = new List<Select2Result>();

            foreach (var key in dictionary.Keys)
            {
                jsonAttendees.Results.Add(new Select2Result { id = key.ToString(), text = dictionary[key] });
            }

            jsonAttendees.Total = dictionary.Count();
            return jsonAttendees;
        }
    }
}