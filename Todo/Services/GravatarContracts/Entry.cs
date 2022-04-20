using System.Collections.Generic;

namespace Todo.Services
{
    public class Entry
    {
        public string id { get; set; }
        public string hash { get; set; }
        public string requestHash { get; set; }
        public string profileUrl { get; set; }
        public string preferredUsername { get; set; }
        public string thumbnailUrl { get; set; }
        public List<Photo> photos { get; set; }
        public Name name { get; set; }
        public string displayName { get; set; }
        public string currentLocation { get; set; }
        public List<Url> urls { get; set; }
    }


}